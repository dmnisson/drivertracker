using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Accord.Statistics.Models.Regression;
using Accord.Statistics.Distributions.Multivariate;
using Accord.MachineLearning;

using Xunit;
using Moq;

using DriverTracker.Models;
using DriverTracker.Domain;

namespace DriverTracker.Tests
{
    public class TestPickupPrediction
    {
        // Set up mocks given arrays of data points
        private void SetupMocks(
            Mock<ILocationClustering> mockLocationClustering,
            Mock<ILegRepository> mockLegRepository,
            Mock<IGeocodingDbSync> mockGeocodingDbSync,
            Mock<ISupervisedLearning<LogisticRegression, double[], double>> mockLogisticRegressionAnalysis,
        double[][][] startLocationClusters, double[][][] endLocationClusters,
            DateTime[][] startDates, double[][] pickupDelays, double[][] durations, double[][] fares, int[][] pickups)
        {
            // mock location clustering
            mockLocationClustering.SetupGet(lc => lc.NumberOfClusters).Returns(startLocationClusters.Length);
            mockLocationClustering.SetupGet(lc => lc.NumberOfClustersLastChanged).Returns(DateTime.Now);
            mockLocationClustering.SetupGet(lc => lc.ClusterCollection).Returns(new KMeansClusterCollection(startLocationClusters.Length, new GeographicDistance())
            {
                Centroids = startLocationClusters.Select((lc, i) => ConcatStartEnd(lc[0], endLocationClusters[i][0])).ToArray(),
                NumberOfClasses = startLocationClusters.Length
            });
            mockLocationClustering.Setup(lc => lc.RetrainAsync()).Returns(() =>
            {
                Console.WriteLine("RetrainAsync called");
                return Task.FromResult(0);
            });
            mockLocationClustering.Setup(lc => lc.RetrainAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .Returns<DateTime, DateTime?>((start, end) =>
                {
                    Console.WriteLine("RetrainAsync called with dates " + start + " to " + end);
                    return Task.FromResult(0);
                });
            mockLocationClustering.Setup(lc => lc.RenumberAsync(It.IsAny<int>()))
                .Returns<int>((max) =>
            {
                Console.WriteLine("RenumberAsync called with maxClusters = " + max);
                return Task.FromResult(0);
            });
            mockLocationClustering.Setup(lc => lc.RenumberAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>()))
                .Returns<DateTime, DateTime?, int>((start, end, max) =>
                {
                    Console.WriteLine("RetrainAsync called with dates " + start + " to " + end + " and maxClusters = " + max);
                    return Task.FromResult(0);
                });

            // mock leg repository
            int largestClusterSize = startLocationClusters.Select(a => a.Length).Max();
            if (pickupDelays != null)
            {
                mockLegRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(
                startLocationClusters.SelectMany((lc, i) => lc.Select((slc, j) => new Leg
                {
                    DriverID = 1,
                    LegID = i * largestClusterSize + j,
                    StartTime = startDates[i][j],
                    ArrivalTime = startDates[i][j].AddMinutes(durations[i][j]),
                    PickupRequestTime = startDates[i][j].AddMinutes(-pickupDelays[i][j]),
                    Fare = Convert.ToDecimal(fares[i][j]),
                    NumOfPassengersPickedUp = pickups[i][j]
                })));
            }
            else
            {
                mockLegRepository.Setup(repo => repo.ListAsync()).ReturnsAsync(
                startLocationClusters.SelectMany((lc, i) => lc.Select((slc, j) => new Leg
                {
                    DriverID = 1,
                    LegID = i * largestClusterSize + j,
                    StartTime = startDates[i][j],
                    ArrivalTime = startDates[i][j].AddMinutes(durations[i][j]),
                    Fare = Convert.ToDecimal(fares[i][j]),
                    NumOfPassengersPickedUp = pickups[i][j]
                })));
            }

            // mock geocoding sync
            mockGeocodingDbSync.Setup(ds => ds.GetLegCoordinatesAsync(It.IsAny<int>()))
                .Returns<int>(async (i) =>
                {
                    double[] startCoordinates = startLocationClusters[i / largestClusterSize][i % largestClusterSize];
                    double[] endCoordinates = endLocationClusters[i / largestClusterSize][i % largestClusterSize];
                    return new LegCoordinates
                    {
                        StartLatitude = Convert.ToDecimal(startCoordinates[0]),
                        StartLongitude = Convert.ToDecimal(startCoordinates[1]),
                        DestLatitude = Convert.ToDecimal(endCoordinates[0]),
                        DestLongitude = Convert.ToDecimal(endCoordinates[1])
                    };
                });

            // mock logistic regression analysis
            mockLogisticRegressionAnalysis.Setup(lra => lra.Learn(It.IsAny<double[][]>(), It.IsAny<double[]>(), It.IsAny<double[]>()))
            .Returns<double[][], double[], double[]>((input, output, weights)
                => new LogisticRegression
                {
                    Intercept = 0,
                    NumberOfClasses = 2,
                    NumberOfInputs = 3,
                    NumberOfOutputs = 1
                });
        }

        // Helper for the above-puts start and end locations together
        private static double[] ConcatStartEnd(double[] start, double[] end)
        {
            double[] point = new double[4];
            start.CopyTo(point, 0);
            end.CopyTo(point, 2);
            return point;
        }

        // Helper function to convert double array values to LegCoordinates entity
        private static LegCoordinates ToLegCoordinates(double[] start, double[] end)
        {
            return new LegCoordinates
            {
                StartLatitude = Convert.ToDecimal(start[0]),
                StartLongitude = Convert.ToDecimal(start[1]),
                DestLatitude = Convert.ToDecimal(end[0]),
                DestLongitude = Convert.ToDecimal(end[1])
            };
        }

        [Fact]
        public async Task CheckLogisticRegressions()
        {
            SetupSimpleMocks(out Mock<ILocationClustering> mockLocationClustering,
            out Mock<ILegRepository> mockLegRepository,
            out Mock<IGeocodingDbSync> mockGeocodingDbSync,
                out Mock<ISupervisedLearning<LogisticRegression, double[], double>>
            mockLogisticRegressionAnalysis);

            // train module
            PickupPrediction instance = new PickupPrediction(mockLocationClustering.Object,
            mockLegRepository.Object, mockGeocodingDbSync.Object, mockLogisticRegressionAnalysis.Object)
            {
                FareClassIntervals = new double[] { 8.9, 23.0, 30.5 }
            };
            await instance.LearnFromDates(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2));

            // test method
            IEnumerable<LogisticRegression> regressions = instance.GetLogisticRegressions(0, 1);

            Assert.Equal(3, regressions.Count());
            for (int i = 0; i < 3; i++)
            {
                LogisticRegression lrx = new LogisticRegression
                {
                    Intercept = 0,
                    NumberOfClasses = 2,
                    NumberOfInputs = 3,
                    NumberOfOutputs = 1,
                };
                AssertRegressionsEqual(lrx, regressions.ElementAt(i));
            }
        }

        // Assertion for non-null logistic regressions whose relevant properties are identical
        private static void AssertRegressionsEqual(LogisticRegression lrx, LogisticRegression lr)
        {
            Assert.NotNull(lr);
            Assert.Equal(lrx.Intercept, lr.Intercept);
            Assert.Equal(lrx.NumberOfClasses, lr.NumberOfClasses);
            Assert.Equal(lrx.NumberOfInputs, lr.NumberOfInputs);
            Assert.Equal(lrx.NumberOfOutputs, lr.NumberOfOutputs);
        }

        // setup a collection of mocks for a simple dataset
        private void SetupSimpleMocks(out Mock<ILocationClustering> mockLocationClustering, out Mock<ILegRepository> mockLegRepository, out Mock<IGeocodingDbSync> mockGeocodingDbSync, out Mock<ISupervisedLearning<LogisticRegression, double[], double>> mockLogisticRegressionAnalysis)
        {
            mockLocationClustering = new Mock<ILocationClustering>();
            mockLegRepository = new Mock<ILegRepository>();
            mockGeocodingDbSync = new Mock<IGeocodingDbSync>();
            mockLogisticRegressionAnalysis
= new Mock<ISupervisedLearning<LogisticRegression, double[], double>>();
            SetupMocks(mockLocationClustering, mockLegRepository, mockGeocodingDbSync, mockLogisticRegressionAnalysis,
            new double[][][] { new double[][] {
                new double[] { 38.5, -121.9 },
                 new double[] { 39.5, -121.8 },
                 new double[] { 39.6, -121.8 } } },
                new double[][][] { new double[][] {
            new double[] { 39.5, -121.8 },
            new double[] { 39.6, -121.8 },
            new double[] { 39.55, -121.87 } } },
            new DateTime[][] { new DateTime[] {
                DateTime.Now,
                DateTime.Now.AddHours(2),
                DateTime.Now.AddHours(3) } },
                new double[][] { new double[] { 0, 0, 1.5 } },
                new double[][] { new double[] { 15.0, 10.0, 5.7 } },
                new double[][] { new double[] { 15.0, 0.0, 4.7 } }, new int[][] { new int[] { 1, 1, 2 } });
        }

        // setup a collection of mocks for a simple dataset with null pickup delays
        private void SetupSimpleMocks2(out Mock<ILocationClustering> mockLocationClustering, out Mock<ILegRepository> mockLegRepository, out Mock<IGeocodingDbSync> mockGeocodingDbSync, out Mock<ISupervisedLearning<LogisticRegression, double[], double>> mockLogisticRegressionAnalysis)
        {
            mockLocationClustering = new Mock<ILocationClustering>();
            mockLegRepository = new Mock<ILegRepository>();
            mockGeocodingDbSync = new Mock<IGeocodingDbSync>();
            mockLogisticRegressionAnalysis
= new Mock<ISupervisedLearning<LogisticRegression, double[], double>>();
            SetupMocks(mockLocationClustering, mockLegRepository, mockGeocodingDbSync, mockLogisticRegressionAnalysis,
            new double[][][] { new double[][] {
                new double[] { 38.5, -121.9 },
                 new double[] { 39.5, -121.8 },
                 new double[] { 39.6, -121.8 } } },
                new double[][][] { new double[][] {
            new double[] { 39.5, -121.8 },
            new double[] { 39.6, -121.8 },
            new double[] { 39.55, -121.87 } } },
            new DateTime[][] { new DateTime[] {
                DateTime.Now,
                DateTime.Now.AddHours(2),
                DateTime.Now.AddHours(3) } },
                null,
                new double[][] { new double[] { 15.0, 10.0, 5.7 } },
                new double[][] { new double[] { 15.0, 0.0, 4.7 } }, new int[][] { new int[] { 1, 1, 2 } });
        }

        [Fact]
        public async Task CheckFareClassProbabilities()
        {
            LogisticRegression[] lr = {
                new LogisticRegression
                {
                    Intercept = -8.9,
                    NumberOfInputs = 2,
                    NumberOfOutputs = 1,
                    NumberOfClasses = 2
                },
                new LogisticRegression
                {
                    Intercept = -23.0,
                    NumberOfInputs = 2,
                    NumberOfOutputs = 1,
                    NumberOfClasses = 2
                },
                new LogisticRegression
                {
                    Intercept = -30.5,
                    NumberOfInputs = 2,
                    NumberOfOutputs = 1,
                    NumberOfClasses = 2
                }
            };

            // check cases of null and non-null pickup delays
            await CheckProbabilityFunction(lr, (objInstance, startLoc, endLoc, delay, duration, pickups, interval) =>
                            objInstance.GetFareClassProbabilities(startLoc, endLoc, delay, duration, pickups, interval),
            1.3, 20.0, 1, 4880, false, leg => leg.NumOfPassengersPickedUp == 1);
            await CheckProbabilityFunction(lr, (objInstance, startLoc, endLoc, delay, duration, pickups, interval) =>
                            objInstance.GetFareClassProbabilities(startLoc, endLoc, delay, duration, pickups, interval),
            1.3, 20.0, 1, 4880, true, leg => leg.NumOfPassengersPickedUp == 1);
        }

        [Fact]
        public async Task CheckPickupProbabilities()
        {
            LogisticRegression[] lr = {
                new LogisticRegression
                {
                    Intercept = -8.9,
                    NumberOfInputs = 3,
                    NumberOfOutputs = 1,
                    NumberOfClasses = 2
                },
                new LogisticRegression
                {
                    Intercept = -23.0,
                    NumberOfInputs = 3,
                    NumberOfOutputs = 1,
                    NumberOfClasses = 2
                },
                new LogisticRegression
                {
                    Intercept = -30.5,
                    NumberOfInputs = 3,
                    NumberOfOutputs = 1,
                    NumberOfClasses = 2
                }
            };
            await CheckProbabilityFunctionGivenFare(lr, (objInstance, startLoc, endLoc, delay, duration, fare, interval) =>
                            objInstance.GetPickupProbabilities(startLoc, endLoc, delay, duration, fare, interval),
            1.3, 20.0, 13.0, 4880, false);
            await CheckProbabilityFunctionGivenFare(lr, (objInstance, startLoc, endLoc, delay, duration, fare, interval) =>
                            objInstance.GetPickupProbabilities(startLoc, endLoc, delay, duration, fare, interval),
            1.3, 20.0, 13.0, 4880, true);
        }

        // check a function that gives a list of probabilities for classifications such as
        // fare class or number of pickups
        private async Task CheckProbabilityFunction<T3>(LogisticRegression[] lr,
            Func<PickupPrediction, double[], double[], double, double, T3, double, IEnumerable<double>> testFunc,
            double pickupDelay, double duration, T3 in3, double interval, bool nullPickupDelays, Func<Leg, bool> predicate = null
            )
        {
            Console.WriteLine("CheckProbabilityFunction called");
            Mock<ILocationClustering> mockLocationClustering;
            Mock<ILegRepository> mockLegRepository;
            Mock<IGeocodingDbSync> mockGeocodingDbSync;
            Mock<ISupervisedLearning<LogisticRegression, double[], double>> mockLogisticRegressionAnalysis;

            if (nullPickupDelays)
            {
                SetupSimpleMocks2(out mockLocationClustering, out mockLegRepository,
                out mockGeocodingDbSync, out mockLogisticRegressionAnalysis);
            }
            else
            {
                SetupSimpleMocks(out mockLocationClustering, out mockLegRepository,
                 out mockGeocodingDbSync, out mockLogisticRegressionAnalysis);
            }

            double[] fareClassIntervals = { 8.9, 23.0, 30.5 };

            IEnumerable<Leg> legs = await mockLegRepository.Object.ListAsync();
            if (predicate != null)
            {
                legs = legs.Where(predicate);
            }
            SetupMockLogisticRegressionAnalysis(lr, mockLogisticRegressionAnalysis, fareClassIntervals, legs, out LogisticRegression[] mockLr);

            PickupPrediction instance = new PickupPrediction(mockLocationClustering.Object, mockLegRepository.Object, mockGeocodingDbSync.Object, mockLogisticRegressionAnalysis.Object)
            {
                FareClassIntervals = fareClassIntervals
            };

            await instance.LearnFromDates(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2));

            IEnumerable<double> actualProbabilities = testFunc(instance, new double[] { 0, 0 }, new double[] { 0, 1 }, pickupDelay, duration, in3, interval);

            double pickupProb = ComputeDensityEstimation(pickupDelay, duration, interval, legs);

            CheckProbabilityResults(pickupDelay, duration, mockLr, actualProbabilities, pickupProb);
        }

        // Helper functions
        private static double ComputeDensityEstimation(double pickupDelay, double duration, double interval, IEnumerable<Leg> legs)
        {

            MultivariateEmpiricalDistribution dist = new MultivariateEmpiricalDistribution(legs
                            .Where(l => l.NumOfPassengersPickedUp > 0)
                        .Select(l => (new double[] {
                    GetPickupDelay(l),
                    l.ArrivalTime.Subtract(l.StartTime).TotalMinutes
                            })).ToArray());

            double probDist = 1 - dist.DistributionFunction(new double[] { pickupDelay, duration });

            // compute appropriate univariate distribution
            if (Math.Abs(dist.Variance[0]) < Double.Epsilon)
            {
                dist = new MultivariateEmpiricalDistribution(legs
                            .Where(l => l.NumOfPassengersPickedUp > 0)
                        .Select(l => (new double[] {
                    l.ArrivalTime.Subtract(l.StartTime).TotalMinutes
                            })).ToArray());
                probDist = 1 - dist.DistributionFunction(new double[] { duration });
            }

            double frequency = legs.Count(l => Math.Abs(l.StartTime.Subtract(DateTime.Now).TotalDays) < 2
                   && l.NumOfPassengersPickedUp > 0) / 5760.0;

            return probDist * frequency * interval;
        }

        // check a function that gives a list of probabilities for classifications such as
        // fare class or number of pickups
        private async Task CheckProbabilityFunctionGivenFare(LogisticRegression[] lr,
            Func<PickupPrediction, double[], double[], double, double, double, double, IEnumerable<double>> testFunc,
            double pickupDelay, double duration, double fare, double interval, bool nullPickupDelays
            )
        {
            Console.WriteLine("CheckProbabilityFunctionGivenFare called");
            Mock<ILocationClustering> mockLocationClustering;
            Mock<ILegRepository> mockLegRepository;
            Mock<IGeocodingDbSync> mockGeocodingDbSync;
            Mock<ISupervisedLearning<LogisticRegression, double[], double>> mockLogisticRegressionAnalysis;

            if (nullPickupDelays)
            {
                SetupSimpleMocks2(out mockLocationClustering, out mockLegRepository,
                out mockGeocodingDbSync, out mockLogisticRegressionAnalysis);
            }
            else
            {
                SetupSimpleMocks(out mockLocationClustering, out mockLegRepository,
                 out mockGeocodingDbSync, out mockLogisticRegressionAnalysis);
            }

            double[] fareClassIntervals = { 8.9, 23.0, 30.5 };

            IEnumerable<Leg> legs = await mockLegRepository.Object.ListAsync();
            SetupMockLogisticRegressionAnalysis(lr, mockLogisticRegressionAnalysis, fareClassIntervals, legs, out LogisticRegression[] mockLr);

            PickupPrediction instance = new PickupPrediction(mockLocationClustering.Object, mockLegRepository.Object, mockGeocodingDbSync.Object, mockLogisticRegressionAnalysis.Object)
            {
                FareClassIntervals = fareClassIntervals
            };

            await instance.LearnFromDates(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2));

            IEnumerable<double> actualProbabilities = testFunc(instance, new double[] { 0, 0 }, new double[] { 0, 1 }, pickupDelay, duration, fare, interval);

            double[] pickupProbs = new double[actualProbabilities.Count()]; 
            for (int i = 0; i < pickupProbs.Length; i++)
            {
                pickupProbs[i] = ComputeDensityEstimation(pickupDelay, duration, interval,
                 legs.Where(l => l.NumOfPassengersPickedUp == i + 1));
            }


            CheckProbabilityResultsGivenFare(pickupDelay, duration, mockLr, actualProbabilities, pickupProbs, fare, await instance.GetMaxNumberOfPickups(), fareClassIntervals, legs, mockLogisticRegressionAnalysis);
        }

        // Helper functions
        private static void SetupMockLogisticRegressionAnalysis(LogisticRegression[] lr, Mock<ISupervisedLearning<LogisticRegression, double[], double>> mockLogisticRegressionAnalysis, double[] fareClassIntervals, IEnumerable<Leg> legs, out LogisticRegression[] mockLr)
        {
            mockLogisticRegressionAnalysis.Setup(lra => lra.Learn(It.IsAny<double[][]>(), It.IsAny<double[]>(), It.IsAny<double[]>()))
                            .Returns<double[][], double[], double[]>((Func<double[][], double[], double[], LogisticRegression>)((input, output, weights) =>
                            {
                                // determine appropriate logistic regression based on comparing
                                // output array to 
                                for (int i = 0; i < fareClassIntervals.Length; i++)
                                {

                                    double[] expectedOutput = input.Select(dp =>
                                    {

                                        Leg leg = legs.FirstOrDefault(
                                            l => Math.Abs(dp[0] - GetPickupDelay(l)) < double.Epsilon
                                            && Math.Abs(dp[1] - l.ArrivalTime.Subtract(l.StartTime).TotalMinutes) < double.Epsilon);

                                        return Convert.ToDouble(leg == null ? 0 : leg.Fare) >= fareClassIntervals[i] ? 1.0 : 0.0;

                                    }).ToArray();

                                    int j = 0;
                                    for (; j < output.Length; j++)
                                    {
                                        if (Math.Abs(output[j] - expectedOutput[j]) > double.Epsilon)
                                        {
                                            break;
                                        }
                                    }

                                    if (j == output.Length)
                                        return lr[i];
                                }

                                return lr[lr.Length - 1];
                            }));
            mockLr = ComputeMockLogisticRegressionModels(mockLogisticRegressionAnalysis, fareClassIntervals, legs);
        }

        private static LogisticRegression[] ComputeMockLogisticRegressionModels(Mock<ISupervisedLearning<LogisticRegression, double[], double>> mockLogisticRegressionAnalysis,
        double[] fareClassIntervals, IEnumerable<Leg> legs,
            Func<Leg, bool> mockLrPredicate = null)
        {
            // set up logistic regression array from result of mock
            LogisticRegression[] mockLr = new LogisticRegression[fareClassIntervals.Length];
            IEnumerable<Leg> selectLegs;
            if (mockLrPredicate == null)
            {
                selectLegs = legs;
            }
            else
            {
                selectLegs = legs.Where(mockLrPredicate);
            }


            for (int i = 0; i < fareClassIntervals.Length; i++)
            {
                mockLr[i] = mockLogisticRegressionAnalysis.Object.Learn(
                    selectLegs.Select(l => new double[] { GetPickupDelay(l), l.ArrivalTime.Subtract(l.StartTime).TotalMinutes }).ToArray(),
                    selectLegs.Select(l => Convert.ToDouble(l.Fare) >= fareClassIntervals[i] ? 1.0 : 0.0).ToArray()
                );
            }

            return mockLr;
        }

        // Helper for when the desired result is not conditional on fare class
        private static void CheckProbabilityResults(double pickupDelay, double duration, LogisticRegression[] mockLr, IEnumerable<double> actualProbabilities,
            double pickupProb // probability of a leg occurring with at least 1 pickup
            )
        {
            for (int i = 0; i <= mockLr.Length; i++)
            {
                // conditional probability of class i+1 or greater given that a pickup occurred
                double condProb = i < mockLr.Length ? mockLr[i].Probability(new double[] { pickupDelay, duration }) : 0;

                // probability value to expect
                double expectedProb;
                if (i == 0)
                    expectedProb = (1 - condProb) * pickupProb;
                else
                    expectedProb = mockLr[i - 1].Probability(new double[] { pickupDelay, duration })
                     * (1 - condProb) * pickupProb;

                Assert.Equal(expectedProb, actualProbabilities.ElementAt(i), 6);
            }
        }

        // Helper for when the desired result is conditional on fare class
        private static void CheckProbabilityResultsGivenFare(double pickupDelay, double duration, LogisticRegression[] mockLr, IEnumerable<double> actualProbabilities,
            double[] pickupProbs, // probability of a leg occurring with at least 1 pickup
            double fare, int maxPickups, double[] fareClassIntervals, IEnumerable<Leg> legs,
            Mock<ISupervisedLearning<LogisticRegression, double[], double>> mockLra)
        {
            for (int n = 1; n <= maxPickups; n++)
            {
                // find fare class of given fare
                int fareClass = 0;
                for (; fareClass < fareClassIntervals.Length; fareClass++)
                {
                    if (fareClassIntervals[fareClass] > fare)
                    {
                        break;
                    }
                }

                double pickupProb = n == 0 ? pickupProbs.Sum() : pickupProbs[n - 1];

                // conditional probability of class fareClass or greater given that a pickup occurred with the given parameters
                double condProb = fareClass > 0 ? mockLr[fareClass - 1].Probability(new double[] { pickupDelay, duration }) : 1.0;

                // unconditional probability of fare class assuming fares are negotiated
                double uncondProb = condProb * pickupProb + (1 - condProb) * (1 - pickupProb); // since fares are negotiated

                //expected probability
                double expectedProb;
                mockLr = ComputeMockLogisticRegressionModels(mockLra, fareClassIntervals, legs, leg => leg.NumOfPassengersPickedUp == n);

                // probability of fare class given exactly n pickups
                double nPickupsCondProb = fareClass > 0 ? mockLr[fareClass - 1].Probability(new double[] { pickupDelay, duration }) : 1.0;

                double nPickupsUncondProb = nPickupsCondProb * pickupProb + (1 - nPickupsCondProb) * (1 - pickupProb);

                expectedProb = nPickupsCondProb * pickupProb / nPickupsUncondProb;

                Assert.Equal(expectedProb, actualProbabilities.ElementAt(n - 1), 6);
            }
        }

        // Helper for the above
        private static double GetPickupDelay(Leg l)
        {
            return l.PickupRequestTime.HasValue ? l.StartTime.Subtract(l.PickupRequestTime.Value).TotalMinutes : 0.0;
        }
    }
}
