using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Accord.Statistics.Models.Regression;
using Accord.MachineLearning;

using Xunit;
using Moq;

using DriverTracker.Models;
using DriverTracker.Domain;

namespace DriverTracker.Tests
{
    public class TestPickupPrediction
    {

        private void SetupMocks(
            Mock<ILocationClustering> mockLocationClustering,
            Mock<ILegRepository> mockLegRepository,
            Mock<IGeocodingDbSync> mockGeocodingDbSync,
            Mock<ISupervisedLearning<LogisticRegression, double[], double>> mockLogisticRegressionAnalysis,
        double[][][] startLocationClusters, double[][][] endLocationClusters,
            DateTime[][] startDates, double[][] durations, double[][] fares, int[][] pickups)
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

            bool[] expectedNonNull = { false, true, false, false };
            Assert.Equal(expectedNonNull.Length, regressions.Count());
            for (int i = 0; i < expectedNonNull.Length; i++)
            {
                if (expectedNonNull[i])
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
                else
                {
                    Assert.Null(regressions.ElementAt(i));
                }
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
new double[][][] { new double[][] { new double[] { 38.5, -121.9 }, new double[] { 39.5, -121.8 } } }, new double[][][] { new double[][] { new double[] { 39.5, -121.8 }, new double[] { 39.6, -121.8 } } },
new DateTime[][] { new DateTime[] { DateTime.Now, DateTime.Now.AddHours(2) } }, new double[][] { new double[] { 15.0, 10.0 } },
new double[][] { new double[] { 15.0, 0.0 } }, new int[][] { new int[] { 1, 1 } });
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
            await CheckProbabilityFunction<int, double>(lr, (objInstance, startLoc, endLoc, delay, duration, pickups, interval) =>
                            objInstance.GetFareClassProbabilities(startLoc, endLoc, delay, duration, pickups, interval),
            1.3, 20.0, 1, 4880);
        }

        [Fact]
        public async Task CheckPickupProbabilities()
        {
            LogisticRegression[] lr = {
                new LogisticRegression
                {
                    Intercept = 0,
                    NumberOfInputs = 3,
                    NumberOfOutputs = 1,
                    NumberOfClasses = 2
                },
                new LogisticRegression
                {
                    Intercept = -1,
                    NumberOfInputs = 3,
                    NumberOfOutputs = 1,
                    NumberOfClasses = 2
                },
                new LogisticRegression
                {
                    Intercept = -2,
                    NumberOfInputs = 3,
                    NumberOfOutputs = 1,
                    NumberOfClasses = 2
                },
                new LogisticRegression
                {
                    Intercept = -3,
                    NumberOfInputs = 3,
                    NumberOfOutputs = 1,
                    NumberOfClasses = 2
                },
                new LogisticRegression
                {
                    Intercept = -4,
                    NumberOfInputs = 3,
                    NumberOfOutputs = 1,
                    NumberOfClasses = 2
                }
            };
            await CheckProbabilityFunction<double, double>(lr, (objInstance, startLoc, endLoc, delay, duration, fare, interval) =>
                            objInstance.GetPickupProbabilities(startLoc, endLoc, delay, duration, fare, interval),
            1.3, 20.0, 13.0, 4880);
        }

        private async Task CheckProbabilityFunction<T1, T2>(LogisticRegression[] lr, 
            Func<PickupPrediction, double[], double[], double, double, T1, T2, IEnumerable<double>> testFunc,
            double in1, double in2, T1 in3, T2 in4)
        {
            Console.WriteLine("CheckProbabilityFunction called");
            SetupSimpleMocks(out Mock<ILocationClustering> mockLocationClustering,
             out Mock<ILegRepository> mockLegRepository,
             out Mock<IGeocodingDbSync> mockGeocodingDbSync,
                 out Mock<ISupervisedLearning<LogisticRegression, double[], double>>
             mockLogisticRegressionAnalysis);

            double[] fareClassIntervals = { 8.9, 23.0, 30.5 };

            mockLogisticRegressionAnalysis.Setup(lra => lra.Learn(It.IsAny<double[][]>(), It.IsAny<double[]>(), It.IsAny<double[]>()))
                .Returns<double[][], double[], double[]>((input, output, weights) =>
                {
                    int fareClass = fareClassIntervals.Length - 1;
                    double avg = output.Average();

                    // determine fare class based on how many outputs are 1
                    for (int i = 0; i < fareClassIntervals.Length; i++)
                    {
                        if (avg < Convert.ToDouble(i + 1) / fareClassIntervals.Length)
                        {
                            fareClass = i;
                            break;
                        }
                    }
                    return lr[fareClass];
                });

            PickupPrediction instance = new PickupPrediction(mockLocationClustering.Object, mockLegRepository.Object, mockGeocodingDbSync.Object, mockLogisticRegressionAnalysis.Object)
            {
                FareClassIntervals = fareClassIntervals
            };

            await instance.LearnFromDates(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(2));

            IEnumerable<double> fareClassProbabilities = testFunc(instance, new double[] { 0, 0 }, new double[] { 0, 1 }, in1, in2, in3, in4);

            for (int i = 0; i <= lr.Length; i++)
            {
                double prob = i < lr.Length ? lr[i].Probability(new double[] { in1, in2 }) : 0;
                if (i == 0)
                    Assert.Equal(1 - prob, fareClassProbabilities.ElementAt(0));
                else
                    Assert.Equal(lr[i - 1].Probability(new double[] { in1, in2 }) * (1 - prob),
                    fareClassProbabilities.ElementAt(i));
            }
        }
    }
}
