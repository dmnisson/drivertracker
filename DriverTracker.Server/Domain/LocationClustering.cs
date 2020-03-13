using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using static System.Math;

using Accord.MachineLearning;

using DriverTracker.Models;
using DriverTracker.Repositories;

namespace DriverTracker.Domain
{
    public class LocationClustering : ILocationClustering
    {

        private readonly ILegRepository _legRepository;
        private readonly IGeocodingDbSync _geocodingDbSync;

        public LocationClustering(
            ILegRepository legRepository, 
            IGeocodingDbSync geocodingDbSync)
        {
            _legRepository = legRepository;
            _geocodingDbSync = geocodingDbSync;
        }

        public int NumberOfClusters => ClusterCollection == null ? 1 : ClusterCollection.Count;

        /// <summary>
        /// Train on number of clusters using gap statistic
        /// </summary>
        private async Task ComputeK(int maxK = 100, int B = 10, int driverID = 0, DateTime? startDate = null, DateTime? endDate = null)
        {

            double[] Wk = new double[maxK];
            double[][] Wref_kb = new double[maxK][];
            double[] Gap = new double[maxK];
            double[] sd = new double[maxK];

            KMeansClusterCollection[] clusterCollections = new KMeansClusterCollection[maxK];

            // obtain dataset
            IEnumerable<Leg> legs = driverID == 0 ? await _legRepository.ListAsync()
                : await _legRepository.ListForDriverAsync(driverID);
            if (startDate == null) startDate = DateTime.MinValue;
            if (endDate == null) endDate = DateTime.MaxValue;
            legs = legs.Where(leg => leg.StartTime.CompareTo(startDate) >= 0 && leg.StartTime.CompareTo(endDate) < 0);
            double[][] dataset = GetDataset(legs);

            // first cluster the dataset varying K
            for (int k = 1; k <= maxK; k++)
            {
                KMeans kMeans = new KMeans(k)
                {
                    // distance function for geographic coordinates
                    Distance = new GeographicDistance()
                };

                clusterCollections[k - 1] = kMeans.Learn(dataset);
                double[][][] clusterData = ClusterPoints(dataset, k, clusterCollections[k - 1]);

                // sum of pairwise distances
                Wk[k - 1] = ComputeWk(clusterData, clusterCollections[k - 1]);
            }

            // then generate the reference data sets
            double[] lowerBounds = new double[4];
            double[] boxDimensions = new double[4];
            for (int i = 0; i < 4; i++)
            {
                lowerBounds[i] = dataset.Select(l => l[i]).Min();
                boxDimensions[i] = dataset.Select(l => l[i]).Max() - lowerBounds[i];
            }
            CorrectLongitudeBounds(lowerBounds, boxDimensions, 1);
            CorrectLongitudeBounds(lowerBounds, boxDimensions, 3);

            Random random = new Random();

            for (int k = 1; k <= maxK; k++)
            {
                Wref_kb[k - 1] = new double[B];
                for (int c = 0; c < B; c++)
                {
                    double[][] refDataset = new double[dataset.Length][];
                    for (int i = 0; i < refDataset.Length; i++)
                    {
                        double[] dataPoint = new double[4];
                        for (int j = 0; j < 4; j++)
                        {
                            dataPoint[j] = random.NextDouble() * boxDimensions[j] + lowerBounds[j];
                            if ((j == 1 || j == 3) && dataPoint[j] > 180) dataPoint[j] -= 360;
                        }
                        refDataset[i] = dataPoint;
                    }

                    // cluster reference dataset
                    KMeans refKmeans = new KMeans(k);
                    refKmeans.Distance = new GeographicDistance();
                    KMeansClusterCollection refClusters = refKmeans.Learn(refDataset);

                    // points in each cluster
                    double[][][] refClusterData = ClusterPoints(refDataset, k, refClusters);

                    // compute pairwise distance sum for refDataset
                    Wref_kb[k - 1][c] = ComputeWk(refClusterData, refClusters);
                }

                // compute gap statistic
                double l_avg = Wref_kb[k - 1].Select(x => Log(x)).Average();
                Gap[k - 1] = l_avg - Log(Wk[k - 1]);
                sd[k - 1] = Sqrt(Wref_kb[k - 1].Select(x => (Log(x) - l_avg) * (Log(x) - l_avg)).Average());

                // decide optimal k
                if (k > 1 && Gap[k - 2] >= Gap[k - 1] - sd[k - 1])
                {
                    ClusterCollection = clusterCollections[k - 2];
                    NumberOfClustersLastChanged = DateTime.Now;
                    return;
                }
            }
        }

        private double[][] GetDataset(IEnumerable<Leg> legs)
        {
            return legs.Select(leg => _geocodingDbSync.GetLegCoordinatesAsync(leg.LegID).Result)
                .Where(coordinates => coordinates != null)
                .Select(coordinates =>
                {
                    return new double[] {
                    Convert.ToDouble(coordinates.StartLatitude),
                    Convert.ToDouble(coordinates.StartLongitude),
                    Convert.ToDouble(coordinates.DestLatitude),
                    Convert.ToDouble(coordinates.DestLongitude)
                };
            }).ToArray();
        }

        private static double[][][] ClusterPoints(double[][] dataset, int k, KMeansClusterCollection clusters)
        {
            // points in each cluster
            double[][][] clusterData = new double[k][][];
            for (int i = 0; i < dataset.Length; i++)
            {
                int decision = clusters.Decide(dataset[i]);

                if (clusterData[decision] == null)
                {
                    clusterData[decision] = new double[][] { };
                }
                Array.Resize(ref clusterData[decision], clusterData[decision].Length + 1);
                clusterData[decision][clusterData[decision].Length - 1] = dataset[i];
            }

            return clusterData;
        }

        // make westerly longitudes lower bounds so reference datasets will generate properly
        private static void CorrectLongitudeBounds(double[] lowerBounds, double[] boxDimensions, int i)
        {

            if (boxDimensions[i] > 180)
            {

                lowerBounds[i] += boxDimensions[i];
                boxDimensions[i] = 360 - boxDimensions[i];
            }
        }

        private double ComputeWk(double[][][] clusterData, KMeansClusterCollection clusters)
        {
            // sum of pairwise distances
            double Wk = 0;
            double[] D = new double[clusterData.Length];

            for (int r = 0; r < clusterData.Length; r++)
            {
                if (clusterData[r] != null)
                {
                    D[r] = 0;
                    for (int i = 0; i < clusterData[r].Length; i++)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            D[r] += clusters.Distance.Distance(clusterData[r][i], clusterData[r][j]);
                        }
                    }

                    if (clusterData[r].Length > 0) Wk += D[r] / (2 * clusterData[r].Length);
                }
            }

            return Wk;
        }

        private DateTime _RenumberedEarliestDate;
        private DateTime _RenumberedLatestDate;
        private DateTime _RetrainedEarliestDate;
        private DateTime _RetrainedLatestDate;
        private DateTime _LastRetrained;

        public DateTime NumberOfClustersLastChanged { get; private set; }

        public KMeansClusterCollection ClusterCollection { get; private set; }

        public DateTime LastRenumbered()
        {
            return NumberOfClustersLastChanged;
        }

        public DateTime LastRenumberedEarliestDate()
        {
            return _RenumberedEarliestDate;
        }

        public DateTime LastRenumberedLatestDate()
        {
            return _RenumberedLatestDate;
        }

        public DateTime LastRetrained()
        {
            return _LastRetrained;
        }

        public DateTime LastRetrainedEarliestDate()
        {
            return _RetrainedEarliestDate;
        }

        public DateTime LastRetrainedLatestDate()
        {
            return _RetrainedLatestDate;
        }

        public async Task RenumberAsync(int maxClusters = 100)
        {
            await _geocodingDbSync.UpdateAllAsync();
            await ComputeK(maxClusters);
            IEnumerable<DateTime> startTimes = (await _legRepository.ListAsync()).Select(leg => leg.StartTime);
            _RenumberedEarliestDate = startTimes.Min();
            _RenumberedLatestDate = startTimes.Max();
        }

        public async Task RenumberAsync(DateTime start, DateTime? end, int maxClusters = 100)
        {
            await _geocodingDbSync.UpdateAllAsync();
            await ComputeK(maxClusters, startDate: start, endDate: end);
            IEnumerable<DateTime> startTimes = (await _legRepository
            .ListAsync(leg => leg.StartTime.CompareTo(start) >= 0 && leg.StartTime.CompareTo(end) < 0))
                .Select(leg => leg.StartTime);
            _RenumberedEarliestDate = startTimes.Min();
            _RenumberedLatestDate = startTimes.Max();
        }

        public async Task RetrainAsync()
        {
            await RetrainAsync(null);
        }

        private async Task RetrainAsync(Expression<Func<Leg, bool>> predicate)
        {
            await _geocodingDbSync.UpdateAllAsync();
            KMeans kMeans = new KMeans(NumberOfClusters)
            {
                Distance = new GeographicDistance()
            };

            double[][] dataset = GetDataset(predicate == null ? 
                await _legRepository.ListAsync()
                : await _legRepository.ListAsync(predicate));

            ClusterCollection = await Task.Run(() => kMeans.Learn(dataset));

            IEnumerable<DateTime> startTimes = (await _legRepository.ListAsync(predicate)).Select(leg => leg.StartTime);
            _RetrainedEarliestDate = startTimes.Min();
            _RetrainedLatestDate = startTimes.Max();
            _LastRetrained = DateTime.Now;
        }

        public async Task RetrainAsync(DateTime start, DateTime? end)
        {
            Expression<Func<Leg, bool>> predicate = leg => leg.StartTime.CompareTo(start) >= 0 && leg.StartTime.CompareTo(end) < 0;
            await RetrainAsync(predicate);
        }
    }
}
