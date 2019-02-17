using System;
using Xunit;

using DriverTracker.Models;
using DriverTracker.Domain;

namespace DriverTracker.Tests
{
    public class TestDriverStatistics
    {
        private DriverStatistics CreateInstance(Driver[] drivers, Leg[] legs)
        {
            var mockDriverRepository = new MockDriverRepository(drivers);
            var mockLegRepository = new MockLegRepository(legs);

            return new DriverStatistics(mockDriverRepository, mockLegRepository);
        }

        private DriverStatistics CreateEmptyInstance() => CreateInstance(new Driver[] { }, new Leg[] { });

        private DriverStatistics CreateInstance1() => CreateInstance(new Driver[] {
                new Driver {
                    DriverID = 1,
                    UserIDString = "1",
                    LicenseNumber = "123456789ABC",
                    Name = "John Doe"
                }
            }, new Leg[] { });

        private DriverStatistics CreateInstance2() => CreateInstance(new Driver[] {
                new Driver {
                    DriverID = 1,
                    UserIDString = "1",
                    LicenseNumber = "123456789ABC",
                    Name = "John Doe"
                },
                new Driver {
                    DriverID = 2,
                    UserIDString = "7",
                    LicenseNumber = "123456788ABC",
                    Name = "Joe Johnson"
                },
                new Driver {
                    DriverID = 3,
                    UserIDString = "19",
                    LicenseNumber = "AC12346134",
                    Name = "Alex Smith"
                },
                new Driver {
                    DriverID = 4,
                    UserIDString = "3",
                    LicenseNumber = "11235813ABCX",
                    Name = "Jane West"
                },
                new Driver {
                    DriverID = 5,
                    UserIDString = "8",
                    LicenseNumber = "112471324BCDE",
                    Name = "Lucy Anderson"
                }
            }, new Leg[] {
                new Leg
                {
                    LegID = 1,
                    DriverID = 1,
                    PickupRequestTime = new DateTime(2010, 7, 15, 13, 10, 00),
                    StartTime = new DateTime(2010, 7, 15, 13, 13, 56),
                },
                new Leg
                {
                    LegID = 2,
                    DriverID = 1,
                    PickupRequestTime = new DateTime(2010, 7, 15, 14, 30, 00),
                    StartTime = new DateTime(2010, 7, 15, 14, 35, 24),
                },
                new Leg
                {
                    LegID = 3,
                    DriverID = 1,
                    PickupRequestTime = new DateTime(2010, 7, 15, 18, 00, 00),
                    StartTime = new DateTime(2010, 7, 15, 18, 07, 01),
                }
            });

        [Fact]
        public void CheckNumberOfDrivers()
        {
            var driverStatistics = CreateEmptyInstance();

            driverStatistics.ComputeCompanyStatistics();

            Assert.Equal(0, driverStatistics.NumOfDrivers);
        }

        [Fact]
        public void CheckNumberOfDrivers1()
        {
            var driverStatistics = CreateInstance1();

            driverStatistics.ComputeCompanyStatistics();

            Assert.Equal(1, driverStatistics.NumOfDrivers);
        }

        [Fact]
        public void CheckNumberOfDrivers2()
        {
            var driverStatistics = CreateInstance2();

            driverStatistics.ComputeCompanyStatistics();

            Assert.Equal(5, driverStatistics.NumOfDrivers);
        }

        [Fact]
        public void CheckAveragePickupDelayEmpty()
        {
            var driverStatistics = CreateEmptyInstance();

            driverStatistics.ComputeCompanyStatistics();

            Assert.False(driverStatistics.AveragePickupDelay.HasValue);
        }

        [Fact]
        public void CheckAveragePickupDelay1()
        {
            var driverStatistics = CreateInstance1();

            driverStatistics.ComputeCompanyStatistics();

            Assert.False(driverStatistics.AveragePickupDelay.HasValue);
        }

        [Fact]
        public void CheckAveragePickupDelay2()
        {
            var driverStatistics = CreateInstance2();

            driverStatistics.ComputeCompanyStatistics();

            Assert.Equal(5.45, driverStatistics.AveragePickupDelay.Value, 2);
        }
    }
}
