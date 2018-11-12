using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression;

using DriverTracker.Models;

namespace DriverTracker.Domain
{
    public class FarePrediction
    {
        private readonly LogisticRegressionAnalysis _logisticRegressionAnalysis;
        private LogisticRegression _logisticRegression;
        private readonly MvcDriverContext _context;
        private readonly int _DriverID;

        public FarePrediction(MvcDriverContext context, int DriverID) {
            _logisticRegressionAnalysis = new LogisticRegressionAnalysis();
            _context = context;
            _DriverID = DriverID;

            _logisticRegressionAnalysis.Inputs = new string[] { "delay", "legDuration", "fare" };
            _logisticRegressionAnalysis.Output = "multiplePickups";
        }

        /* Learn from legs with specified request times in a given date range */
        public async void LearnFromDates(DateTime from, DateTime to) {
            List<Leg> legs = await _context.Legs.Where(leg => leg.DriverID == _DriverID
                                                        && leg.StartTime.CompareTo(from) >= 0
                                                        && leg.StartTime.CompareTo(to) < 0
                                                        && leg.PickupRequestTime.HasValue)
                                                .ToListAsync();

            double[][] trainingInputs = legs.Select(leg =>
            {
                return new double[]
                {
                    leg.StartTime.Subtract(leg.PickupRequestTime.Value).TotalMinutes,
                    leg.ArrivalTime.Subtract(leg.StartTime).TotalMinutes,
                    decimal.ToDouble(leg.Fare)
                };
            }).ToArray();
            
            /*{
                // delays in minutes
                legs.Select(leg => leg.StartTime.Subtract(leg.PickupRequestTime.Value).TotalMinutes)
                    .ToArray(),

                // leg duration in minutes
                legs.Select(leg => leg.ArrivalTime.Subtract(leg.StartTime).TotalMinutes)
                    .ToArray(),

                // fare collected in local currency units
                legs.Select(leg => decimal.ToDouble(leg.Fare)).ToArray()
            };*/

            double[] trainingOutputs =
                legs.Select(leg => leg.NumOfPassengersPickedUp > 1 ? 1.0 : 0.0).ToArray();

            _logisticRegression = _logisticRegressionAnalysis.Learn(trainingInputs, trainingOutputs);
        }

        /* Get the regression result */
        public LogisticRegression GetRegressionModel() {
            return _logisticRegression;
        }
    }
}
