using System;
namespace DriverTracker.Domain
{
    public struct DriverStatisticResults
    {
        public int DriverID;
        public int Pickups; // total pickups
        public decimal MilesDriven; // total miles driven
        public double? AveragePickupDelay; // average pickup delay in minutes
        public decimal TotalFares; // this driver's total fares
        public decimal TotalCosts; // this driver's total fuel costs
    }
}
