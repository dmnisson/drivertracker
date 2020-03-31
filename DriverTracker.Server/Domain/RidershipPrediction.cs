using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Accord.Statistics.Analysis;
using Accord.Statistics.Models.Regression;

using DriverTracker.Models;
using DriverTracker.Repositories;

namespace DriverTracker.Domain
{
    public class RidershipPrediction
    {
        private readonly List<LogisticRegressionAnalysis> _logisticRegressionAnalyses;
        private readonly List<LogisticRegression> _logisticRegressions;
        private readonly ILegRepository _legRepository;
        private readonly int _DriverID;

        public RidershipPrediction(ILegRepository legRepository, int DriverID, int maxPickups = 4) {
            _logisticRegressionAnalyses = new List<LogisticRegressionAnalysis>();
            for (int i = 0; i < maxPickups; i++) {
                _logisticRegressionAnalyses.Add(new LogisticRegressionAnalysis());
            }
            _legRepository = legRepository;
            _DriverID = DriverID;

            _logisticRegressionAnalyses.ForEach(lra => lra.Inputs = new string[] { "delay", "duration", "fare" });
            for (int i = 0; i < _logisticRegressionAnalyses.Count; i++) {
                _logisticRegressionAnalyses[i].Output = "_" + (i + 1) + "PlusPickups";
            }


            _logisticRegressions = new List<LogisticRegression>();
        }

        /* Learn from legs with specified request times in a given date range */
        public async Task LearnFromDates(DateTime from, DateTime to) {
            IEnumerable<Leg> legs = await _legRepository.ListForDriverAsync(_DriverID, leg =>
                                                        leg.StartTime.CompareTo(from) >= 0
                                                        && leg.StartTime.CompareTo(to) < 0
                                                        && leg.PickupRequestTime.HasValue);

            double[][] trainingInputs = legs.Select(leg =>
            {
                return new double[]
                {
                    leg.StartTime.Subtract(leg.PickupRequestTime.Value).TotalMinutes,
                    leg.ArrivalTime.Subtract(leg.StartTime).TotalMinutes,
                    decimal.ToDouble(leg.Fare)
                };
            }).ToArray();

            _logisticRegressions.Clear();
            _logisticRegressions.AddRange(_logisticRegressionAnalyses.Select((lra, i) => {
                    double[] trainingOutputs =
                    legs.Select(leg => leg.NumOfPassengersPickedUp > i + 1 ? 1.0 : 0.0).ToArray();
                    return lra.Learn(trainingInputs, trainingOutputs);
            }));
        }

        /* Get the regression result */
        public IEnumerable<LogisticRegression> GetRegressionModels() {
            return _logisticRegressions;
        }

        /* Get a list of the probabilities of n or more pickups as an array of
         * doubles with indices corresponding to n - 2 */
        public double[] RidershipClassProbabilities(double delay, double duration, double fare) {
            double[] result = new double[_logisticRegressions.Count];
            for (int i = 0; i < _logisticRegressions.Count; i++) {
                result[i] = _logisticRegressions[i].Probabilities(new double[] { delay, duration, fare })[1];
                if (i > 0) result[i - 1] *= 1 - result[i];
            }
            return result;
        }
    }
}
