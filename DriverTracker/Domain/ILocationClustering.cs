using System;
using System.Threading.Tasks;
using Accord.MachineLearning;

namespace DriverTracker.Domain
{
    public interface ILocationClustering
    {
        int NumberOfClusters { get; }
        DateTime NumberOfClustersLastChanged { get; }
        KMeansClusterCollection ClusterCollection { get; }

        // for reclustering using currently set optimal cluster number
        Task RetrainAsync();
        Task RetrainAsync(DateTime start, DateTime? end);
        DateTime LastRetrained();
        DateTime LastRetrainedEarliestDate();
        DateTime LastRetrainedLatestDate();

        // for determining optimal number of clusters
        Task RenumberAsync(int maxClusters = 100);
        Task RenumberAsync(DateTime start, DateTime? end, int maxClusters = 100);
        DateTime LastRenumbered();
        DateTime LastRenumberedEarliestDate();
        DateTime LastRenumberedLatestDate();
    }
}
