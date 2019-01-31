using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Accord.Statistics.Models.Regression;
using Accord.MachineLearning;
using Accord.Statistics.Distributions.Multivariate;
using Accord.Statistics.Distributions.DensityKernels;
using System.Linq;

using DriverTracker.Models;

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
        private double[][][] clusterFareClassFrequencies; // array[i][n-1][j] = frequency of minimum fare class j for cluster i and number of pickups at least n
        private IDensityKernel[][][] clusterFareClassInputDensityKernels; // array[i][n-1][j] = pdf of input variables for cluster i given minimum fare class j
        private double[][] clusterPickupFrequencies; // array[i][j] = frequency of at least j+1 pickups in cluster i
        private IDensityKernel[][] clusterPickupInputDensityKernels; // array[i][j] = pdf of input variables for cluster i given minimum of pickups > j

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
                    probs[i] = 1 - clusterFareClassRegressions[clusterIndex][pickups - 1][0]
                    .Probability(new double[] { pickupDelay, duration });
                else if (i < probs.Length - 1)
                    probs[i] = clusterFareClassRegressions[clusterIndex][pickups - 1][i - 1]
                        .Probability(new double[] { pickupDelay, duration })
                        * (1 - clusterFareClassRegressions[clusterIndex][pickups - 1][i]
                        .Probability(new double[] { pickupDelay, duration }));
                else
                    probs[i] = clusterFareClassRegressions[clusterIndex][pickups - 1][i - 1]
                        .Probability(new double[] { pickupDelay, duration });
            }

            // get unconditional probability
            return probs.Select((p, i) => p
                * clusterPickupInputDensityKernels[clusterIndex][pickups - 1].Function(new double[] { pickupDelay, duration })
                * clusterPickupFrequencies[clusterIndex][pickups - 1]
                * interval * interval * interval).ToArray();
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
                probs[n - 1] = clusterFareClassRegressions[clusterIndex][n - 1][fareClassIndex]
                    .Probability(new double[] { pickupDelay, duration });
            }

            // apply Bayes' theorem
            return probs.Select((p, i) => p
                * clusterPickupInputDensityKernels[clusterIndex][i].Function(new double[] { pickupDelay, duration })
                * clusterPickupFrequencies[clusterIndex][i]
                / clusterFareClassInputDensityKernels[clusterIndex][i][fareClassIndex].Function(new double[] { pickupDelay, duration })
                / clusterFareClassFrequencies[clusterIndex][i][fareClassIndex]
            ).ToArray();
        }

        public async Task LearnFromDates(DateTime from, DateTime to)
        {
            int maxPickups = await GetMaxNumberOfPickups();

            // train clustering algorithm
            await _locationClustering.RetrainAsync(from, to);

            // initialize storage arrays
            int pickupElementSize = await GetMaxNumberOfPickups() + 1;
            InitStorageArray(ref clusterFareClassRegressions, pickupElementSize - 1, NumberOfFareClassIntervals - 1);
            InitStorageArray(ref clusterFareClassFrequencies, pickupElementSize - 1, NumberOfFareClassIntervals);
            InitStorageArray(ref clusterFareClassInputDensityKernels, pickupElementSize - 1, NumberOfFareClassIntervals);
            InitStorageArray(ref clusterPickupFrequencies, pickupElementSize);
            InitStorageArray(ref clusterPickupInputDensityKernels, pickupElementSize);

            // for each cluster
            for (int i = 0; i < _locationClustering.NumberOfClusters; i++)
            {
                // obtain data set
                IEnumerable<Task<Pair<Leg, bool>>> decisionTasks = (await _legRepository.ListAsync()).
                Select(async (leg) =>
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
                            if ((j < FareClassIntervals.Count()
                                && Convert.ToDecimal(FareClassIntervals.ElementAt(j)) > leg.Fare))
                                return j;
                        }
                        return FareClassIntervals.Count();
                    }).ToArray();
                // Pickup numbers in this cluster
                int[] pickupNumbers = dataLegs.Select(leg => leg.NumOfPassengersPickedUp).ToArray();


                // for each possible number of pickups
                for (int n = 1; n <= maxPickups; n++)
                {
                    double[][] dataSubset = dataset.Where((dp, k) => pickupNumbers[k] >= n).ToArray();
                    int[] fareClassesSubset = fareClasses.Where((fc, k) => pickupNumbers[k] >= n).ToArray();
                    // for each fare class interval boundary
                    for (int j = 0; j < NumberOfFareClassIntervals; j++)
                    {
                        // train logistic regression
                        if (j > 0 && clusterFareClassRegressions[i][n-1][j-1] == null)
                        {
                            clusterFareClassRegressions[i][n-1][j-1] = _logisticRegressionAnalysis
                                .Learn(dataSubset, fareClassesSubset.Select(fc => fc >= j ? 1.0 : 0.0).ToArray());
                        }

                        // compute frequency of this fare class
                        clusterFareClassFrequencies[i][n-1][j] = fareClassesSubset.Count(fc => fc >= j) / to.Subtract(from).TotalMinutes;

                        // train empirical density functions
                        if (clusterFareClassFrequencies[i][n-1][j] > 0.0)
                        {
                            MultivariateEmpiricalDistribution fareClassInputDistribution
                            = new MultivariateEmpiricalDistribution(dataSubset.Where((dp, k)
                                => fareClassesSubset[k] >= j).ToArray());
                            clusterFareClassInputDensityKernels[i][n-1][j] = fareClassInputDistribution.Kernel;
                        }

                    }
                }

                // compute pickup frequencies
                for (int j = 0; j < pickupElementSize; j++)
                {
                    clusterPickupFrequencies[i][j] = Convert.ToDouble(dataLegs.Count(leg => leg.NumOfPassengersPickedUp >= j))
                        / to.Subtract(from).TotalMinutes;

                    MultivariateEmpiricalDistribution pickupInputDistribution
                        = new MultivariateEmpiricalDistribution(dataset.Where((dp, k) => pickupNumbers[k] >= j).ToArray());
                    clusterPickupInputDensityKernels[i][j] = pickupInputDistribution.Kernel;
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

                for (int j = 0; j < storageArray.Length; j++)
                {
                    storageArray[i][j] = new T[elementSize2];
                }
            }
        }
    }
}
