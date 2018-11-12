using System;

using Accord.Statistics.Models.Regression;

namespace DriverTracker.Domain
{
    public class LogisticFarePredictionResult
    {
        public int DriverID { get; set; }
        public DateTime FromDateTime { get; set; }
        public DateTime ToDateTime { get; set; }
        public LogisticRegression RegressionResult { get; set; }
    }
}
