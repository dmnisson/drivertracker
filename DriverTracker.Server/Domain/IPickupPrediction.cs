using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Accord.Statistics.Models.Regression;

namespace DriverTracker.Domain
{
    public interface IPickupPrediction
    {
        /// <summary>
        /// Learns from data ranging through the given dates
        /// </summary>
        /// <param name="from">Start date</param>
        /// <param name="to">End date</param>
        Task LearnFromDates(DateTime from, DateTime to);

        /// <summary>
        /// Gets or sets the intervals marking the boundaries of the fare classes.
        /// The first interval is [0, FareClassIntervals[0]], and the last interval
        /// is [FareClassIntervals.Last, +Inf).
        /// </summary>
        /// <value>The fare class intervals.</value>
        IEnumerable<double> FareClassIntervals { get; set; }

        /// <summary>
        /// Gets the logistic regressions corresponding to each fare class boundary
        /// <param name="clusterIndex"/>
        /// The index of the cluster for which to get the logistic regresssions
        /// <paramref name="numberOfPickups"/>
        /// The number of pickups.
        /// </summary>
        /// <value>The logistic regressions.</value>
        IEnumerable<LogisticRegression> GetLogisticRegressions(int clusterIndex, int numberOfPickups);

        /// <summary>
        /// Gets the predicted fare class probabilities given parameters of new leg.
        /// </summary>
        /// <returns>The fare class probabilities.</returns>
        /// <param name="startLocation">Start coordinates.</param>
        /// <param name="destLocation">Destination coordinates.</param>
        /// <param name="pickupDelay">Pickup delay in minutes.</param>
        /// <param name="duration">Duration in minutes.</param>
        /// <param name="pickups">Number of pickups.</param>
        /// <param name="interval">Interval in which to compute probability, in minutes.</param>
        double[] GetFareClassProbabilities(double[] startLocation, double[] destLocation, double pickupDelay, double duration, int pickups, double interval);

        /// <summary>
        /// Gets the predicted number-of-pickups probabilities given parameters of new leg.
        /// </summary>
        /// <returns>The pickup probabilities.</returns>
        /// <param name="startLocation">Start coordinates.</param>
        /// <param name="destLocation">Destination coordinates.</param>
        /// <param name="pickupDelay">Pickup delay in minutes.</param>
        /// <param name="duration">Duration in minutes.</param>
        /// <param name="fare">Fare.</param>
        /// <param name="interval">Interval in which to compute the probability, in minutes.</param>
        double[] GetPickupProbabilities(double[] startLocation, double[] destLocation, double pickupDelay, double duration, double fare, double interval);
    }
}
