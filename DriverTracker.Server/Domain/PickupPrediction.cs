using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Accord.Statistics.Models.Regression;
using Accord.MachineLearning;
using Accord.Statistics.Distributions;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Distributions.DensityKernels;
using System.Linq;

using DriverTracker.Models;
using DriverTracker.Repositories;

namespace DriverTracker.Domain
{
    public class Pair<T1, T2>
    {
        public Pair(T1 first, T2 second)
        {
            First = first; Second = second;
        }

        public T1 First { get; private set; }
        public T2 Second { get; private set; }
    }


    public class PickupPrediction : IPickupPrediction
    {
        private readonly ILocationClustering _locationClustering;
        private readonly ILegRepository _legRepository;
        private readonly IGeocodingDbSync _geocodingDbSync;
        private readonly ISupervisedLearning<LogisticRegression, double[], double> _logisticRegressionAnalysis;

        private LogisticRegression[][][] clusterFareClassRegressions; // array[i][n-1][j] = logistic regression of fare class j for cluster i and minimum number of pickups n
        //private double[][][] clusterFareClassFrequencies; // array[i][n-1][j] = frequency of minimum fare class j for cluster i and number of pickups at least n
        private MultivariateEmpiricalDistribution[][][] clusterFareClassInputDensityKernels; // array[i][n-1][j] = pdf of input variables for cluster i given minimum fare class j
        private bool[][][] clusterFareClassDistributionsUnivariate;
        private double[][] clusterPickupFrequencies; // array[i][j] = frequency of at least j+1 pickups in cluster i
        private MultivariateEmpiricalDistribution[][] clusterPickupInputDensityKernels; // array[i][j] = pdf of input variables for cluster i given minimum of pickups > j
        private bool[][] clusterPickupInputDensityKernelsUnivariate;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:DriverTracker.Domain.PickupPrediction"/> class.
        /// </summary>
        /// <param name="locationClustering">Location clustering servive.</param>
        /// <param name="legRepository">Leg repository.</param>
        /// <param name="logisticRegressionAnalysis">Object that can perform logistic regression analysis.</param>
        public PickupPrediction(ILocationClustering locationClustering, ILegRepository legRepository,
            IGeocodingDbSync geocodingDbSync,
        ISupervisedLearning<LogisticRegression, double[], double> logisticRegressionAnalysis)
        {
            _locationClustering = locationClustering;
            _legRepository = legRepository;
            _geocodingDbSync = geocodingDbSync;
            _logisticRegressionAnalysis = logisticRegressionAnalysis;
        }

        public IEnumerable<double> FareClassIntervals { get; set; }

        public IEnumerable<LogisticRegression> GetLogisticRegressions(int clusterIndex, int numberOfPickups)
        {
            return clusterFareClassRegressions[clusterIndex][numberOfPickups - 1];
        }

        public double[] GetFareClassProbabilities(double[] startLocation, double[] destLocation, double pickupDelay, double duration, int pickups, double interval)
        {
            // cluster to which location belongs
            int clusterIndex = _locationClustering.ClusterCollection.Decide(new double[] {
                startLocation[0], startLocation[1], destLocation[0], destLocation[1] });

            double[] probs = new double[FareClassIntervals.Count() + 1];
            for (int i = 0; i < probs.Length; i++)
            {
                if (i == 0)
                    probs[i] = 1 - clusterFareClassRegressions[clusterIndex][pickups][0]
                    .Probability(new double[] { pickupDelay, duration });
                else if (i < probs.Length - 1)
                    probs[i] = clusterFareClassRegressions[clusterIndex][pickups][i - 1]
                        .Probability(new double[] { pickupDelay, duration })
                        * (1 - clusterFareClassRegressions[clusterIndex][pickups][i]
                        .Probability(new double[] { pickupDelay, duration }));
                else
                    probs[i] = clusterFareClassRegressions[clusterIndex][pickups][i - 1]
                        .Probability(new double[] { pickupDelay, duration });
            }

            // get unconditional probability
            if (clusterPickupInputDensityKernels[clusterIndex][pickups] == null) 
            { 
                // return zeroes for all fare class probabilities for this cluster
                return new double[probs.Length]; 
            }

            double probDist = 1 - clusterPickupInputDensityKernels[clusterIndex][pickups].DistributionFunction(
                clusterPickupInputDensityKernelsUnivariate[clusterIndex][pickups] ? new double[] { duration } : new double[] { pickupDelay, duration });
            return probs.Select((p, i) => p
                * probDist * clusterPickupFrequencies[clusterIndex][pickups] * interval).ToArray();
        }

        public double[] GetPickupProbabilities(double[] startLocation, double[] destLocation, double pickupDelay, double duration, double fare, double interval)
        {
            // cluster to which location belongs
            int clusterIndex = _locationClustering.ClusterCollection.Decide(new double[] {
                startLocation[0], startLocation[1], destLocation[0], destLocation[1] });

            // fare class index
            int fareClassIndex = 0;
            while (fareClassIndex < FareClassIntervals.Count())
            {
                if (FareClassIntervals.ElementAt(fareClassIndex) > fare) break;
                fareClassIndex++;
            }

            double[] probs = new double[GetMaxNumberOfPickups().Result];

            for (int n = 1; n <= probs.Length; n++)
            {
                // first compute probability of fare class given pickups
                probs[n - 1] = fareClassIndex > 0 ? clusterFareClassRegressions[clusterIndex][n - 1][fareClassIndex - 1]
                    .Probability(new double[] { pickupDelay, duration }) : 1;
            }

            // apply Bayes' theorem
            return probs.Select((p, i) =>
            {
                double pickupProb = 0;

                if (clusterPickupInputDensityKernels[clusterIndex][i + 1] != null)
                {
                    double[] inputArray = clusterPickupInputDensityKernelsUnivariate[clusterIndex][i + 1] ? new double[] { duration } : new double[] { pickupDelay, duration };
                    pickupProb = (1 - clusterPickupInputDensityKernels[clusterIndex][i + 1].DistributionFunction(inputArray))
                        * clusterPickupFrequencies[clusterIndex][i + 1] * interval; // unconditional probability of at least n pickups
                }

                return p * pickupProb / ((p * pickupProb) + ((1 - p) * (1 - pickupProb)));
            }).ToArray();
        }

        public async Task LearnFromDates(DateTime from, DateTime to)
        {
            int maxPickups = await GetMaxNumberOfPickups();

            // train clustering algorithm
            await _locationClustering.RetrainAsync(from, to);

            // initialize storage arrays
            int pickupElementSize = await GetMaxNumberOfPickups() + 1;
            InitStorageArray(ref clusterFareClassRegressions, pickupElementSize - 1, NumberOfFareClassIntervals - 1);
            InitStorageArray(ref clusterFareClassInputDensityKernels, pickupElementSize - 1, NumberOfFareClassIntervals);
            InitStorageArray(ref clusterFareClassDistributionsUnivariate, pickupElementSize - 1, NumberOfFareClassIntervals);
            InitStorageArray(ref clusterPickupFrequencies, pickupElementSize);
            InitStorageArray(ref clusterPickupInputDensityKernels, pickupElementSize);
            InitStorageArray(ref clusterPickupInputDensityKernelsUnivariate, pickupElementSize);

            // for each cluster
            for (int i = 0; i < _locationClustering.NumberOfClusters; i++)
            {
                // obtain data set
                IEnumerable<Task<Pair<Leg, bool>>> decisionTasks = (await _legRepository.ListAsync())
                    .Where(leg => leg.StartTime.CompareTo(from) > 0 && leg.StartTime.CompareTo(to) < 0)
                .Select(async (leg) =>
                {
                    LegCoordinates coords = await _geocodingDbSync.GetLegCoordinatesAsync(leg.LegID);
                    double[] dp = (new decimal[] { coords.StartLatitude, coords.StartLongitude, coords.DestLatitude, coords.DestLongitude })
                    .Select(Convert.ToDouble).ToArray();
                    return new Pair<Leg, bool>(leg, _locationClustering.ClusterCollection.Decide(dp) == i);
                });
                Pair<Leg, bool>[] decisions = await Task.WhenAll(decisionTasks);

                // Data input values (pickup delay, travel time) in this cluster
                IEnumerable<Leg> dataLegs = decisions.Where(pair => pair.Second).Select(pair => pair.First);
                double[][] dataset = dataLegs
                .Select(leg => new double[]
                    {
                        leg.PickupRequestTime.HasValue
                        ? leg.StartTime.Subtract(leg.PickupRequestTime.Value).TotalMinutes
                        : 0,

                        leg.ArrivalTime.Subtract(leg.StartTime).TotalMinutes

                    }).ToArray();
                // Fare classes in this cluster
                int[] fareClasses = dataLegs
                    .Select(leg =>
                    {
                        for (int j = 0; j < FareClassIntervals.Count(); j++)
                        {
                            if (j < FareClassIntervals.Count()
                                && Convert.ToDecimal(FareClassIntervals.ElementAt(j)) > leg.Fare)
                                return j;
                        }
                        return FareClassIntervals.Count();
                    }).ToArray();
                // Pickup numbers in this cluster
                int[] pickupNumbers = dataLegs.Select(leg => leg.NumOfPassengersPickedUp).ToArray();


                // for each possible number of pickups
                for (int n = 1; n <= maxPickups; n++)
                {
                    double[][] dataSubset = dataset.Where((dp, k) => pickupNumbers[k] == n).ToArray();
                    int[] fareClassesSubset = fareClasses.Where((fc, k) => pickupNumbers[k] == n).ToArray();

                    if (dataSubset.Length == 0)
                    {
                        throw new ApplicationException("Insufficient data to make a reliable prediction");
                    }

                    // for each fare class interval boundary
                    for (int j = 0; j < NumberOfFareClassIntervals; j++)
                    {
                        // train logistic regression
                        if (j > 0 && clusterFareClassRegressions[i][n - 1][j - 1] == null)
                        {
                            clusterFareClassRegressions[i][n - 1][j - 1] = _logisticRegressionAnalysis
                                .Learn(dataSubset, fareClassesSubset.Select(fc => fc >= j ? 1.0 : 0.0).ToArray());
                        }

                        // train empirical density functions
                        if (fareClassesSubset.Count(fc => fc >= j) > 0.0)
                        {
                            double[][] dataSubsetSamples = dataSubset.Where((dp, k)
                                   => fareClassesSubset[k] >= j).ToArray();
                            MultivariateEmpiricalDistribution fareClassInputDistribution
                            = new MultivariateEmpiricalDistribution(dataSubsetSamples);

                            SetInputDistribution(fareClassInputDistribution, dataSubsetSamples,
                            out clusterFareClassInputDensityKernels[i][n - 1][j],
                            out clusterFareClassDistributionsUnivariate[i][n - 1][j]);
                        }

                    }
                }

                // compute pickup frequencies
                for (int l = 0; l < pickupElementSize; l++)
                {
                    clusterPickupFrequencies[i][l] = Convert.ToDouble(dataLegs.Count(leg => leg.NumOfPassengersPickedUp == l))
                        / to.Subtract(from).TotalMinutes;

                    if (pickupNumbers.Any(pn => pn == l))
                    {
                        double[][] samples = dataset.Where((dp, k) => pickupNumbers[k] == l).ToArray();
                        MultivariateEmpiricalDistribution pickupInputDistribution
                            = new MultivariateEmpiricalDistribution(samples);

                        SetInputDistribution(pickupInputDistribution, samples, 
                        out clusterPickupInputDensityKernels[i][l], 
                        out clusterPickupInputDensityKernelsUnivariate[i][l]);
                    }
                }
            }
        }

        public int NumberOfFareClassIntervals => FareClassIntervals.Count() + 1;

        public async Task<int> GetMaxNumberOfPickups()
        {
            return (await _legRepository.ListAsync()).Select(leg => leg.NumOfPassengersPickedUp).Max();
        }

        private void InitStorageArray<T>(ref T[][] storageArray, int elementSize)
        {
            if (storageArray == null || storageArray.Length != _locationClustering.NumberOfClusters)
            {
                storageArray = new T[_locationClustering.NumberOfClusters][];
            }

            for (int i = 0; i < storageArray.Length; i++)
            {
                storageArray[i] = new T[elementSize];
            }
        }

        private void InitStorageArray<T>(ref T[][][] storageArray, int elementSize1, int elementSize2)
        {
            if (storageArray == null || storageArray.Length != _locationClustering.NumberOfClusters)
            {
                storageArray = new T[_locationClustering.NumberOfClusters][][];
            }

            for (int i = 0; i < storageArray.Length; i++)
            {
                storageArray[i] = new T[elementSize1][];

                for (int j = 0; j < storageArray[i].Length; j++)
                {
                    storageArray[i][j] = new T[elementSize2];
                }
            }
        }

        // set an appropriate distribution for the given data subset
        private void SetInputDistribution(MultivariateEmpiricalDistribution inputDistribution, 
            double[][] dataSubsetSamples, out MultivariateEmpiricalDistribution distToSet, out bool distUnivariate)
        {
            if (Math.Abs(inputDistribution.Variance[0]) < double.Epsilon)
            {
                // if we have essentially a univariate distribution
                MultivariateEmpiricalDistribution univInputDistribution
                    = new MultivariateEmpiricalDistribution(dataSubsetSamples.Select(a => new double[] { a[1] }).ToArray());
                distToSet = univInputDistribution;
                distUnivariate = true;
            }
            else
            {
                distToSet = inputDistribution;
                distUnivariate = false;
            }
        }

    }
}
