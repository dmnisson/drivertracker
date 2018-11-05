using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DriverTracker.Models;

namespace DriverTracker.Domain
{
    public class DriverStatistics
    {
        private readonly MvcDriverContext _context;

        private int numOfDrivers; // total drivers
        private int pickups; // number of pickups
        private decimal milesDriven; // total miles driven company-wide
        private double? averagePickupDelay; // average pickup delay in minutes
        private decimal totalFares; // total revenue from fares
        private decimal totalCosts; // total fuel costs
        private decimal netProfit;

        private Dictionary<int, DriverStatisticResults> driverStats;

        public DriverStatistics(MvcDriverContext context)
        {
            _context = context;
        }

        public async void ComputeCompanyStatistics() {
            numOfDrivers = await _context.Drivers.CountAsync();
            pickups = await _context.Legs.Select(leg => leg.NumOfPassengersPickedUp).SumAsync();
            milesDriven = await _context.Legs.Select(leg => leg.Distance).SumAsync();

            if (await _context.Legs.CountAsync() > 0)
            {
                averagePickupDelay = await _context.Legs.Select(leg => leg.StartTime.Subtract(leg.PickupRequestTime.GetValueOrDefault(leg.StartTime)).TotalMinutes).AverageAsync();
            }

            totalFares = await _context.Legs.Select(leg => leg.Fare * leg.NumOfPassengersAboard).SumAsync();
            totalCosts = await _context.Legs.Select(leg => leg.GetTotalFuelCost()).SumAsync();
            netProfit = totalFares - totalCosts;
        }

        public async void ComputeDriverStatistics(int id) {
            if (driverStats == null) {
                driverStats = new Dictionary<int, DriverStatisticResults>();
            }

            Driver driver = await _context.Drivers.Where(d => d.DriverID == id).FirstAsync();
            if (driver == null) {
                return;
            }

            DriverStatisticResults results = new DriverStatisticResults();
            results.DriverID = id;
            results.Pickups = await _context.Legs.Where(leg => leg.DriverID == id)
                                        .Select(leg => leg.NumOfPassengersPickedUp).SumAsync();
            results.MilesDriven = await _context.Legs.Where(leg => leg.DriverID == id)
                                                .Select(leg => leg.Distance).SumAsync();
            if (await _context.Legs.Where(leg => leg.DriverID == id).CountAsync() > 0)
            {
                results.AveragePickupDelay = await _context.Legs.Where(leg => leg.DriverID == id)
                                                      .Select(leg => leg.StartTime.Subtract(leg.PickupRequestTime.GetValueOrDefault(leg.StartTime)).TotalMinutes).AverageAsync();
            }

            results.TotalFares = await _context.Legs.Where(leg => leg.DriverID == id)
                .Select(leg => leg.Fare * leg.NumOfPassengersAboard).SumAsync();

            results.TotalCosts = await _context.Legs.Where(leg => leg.DriverID == id)
                .Select(leg => leg.GetTotalFuelCost()).SumAsync();

            driverStats[id] = results;
        }

        public int NumOfDrivers
        {
            get
            {
                return numOfDrivers;
            }
        }

        public int Pickups {
            get {
                return pickups;
            }
        }

        public int GetPickupsBy(int id)
        {
            return driverStats[id].Pickups;
        }

        public decimal MilesDriven {
            get {
                return milesDriven;
            }
        }

        public decimal GetMilesDrivenBy(int id)
        {
            return driverStats[id].MilesDriven;
        }

        public double? AveragePickupDelay {
            get {
                return averagePickupDelay;
            }
        }

        public double? GetAveragePickupDelayBy(int id)
        {
            return driverStats[id].AveragePickupDelay;
        }

        public decimal TotalFares {
            get {
                return totalFares;
            }
        }

        public decimal GetTotalFaresBy(int id)
        {
            return driverStats[id].TotalFares;
        }

        public decimal TotalCosts {
            get {
                return totalCosts;
            }
        }

        public decimal GetTotalCostsBy(int id)
        {
            return driverStats[id].TotalCosts;
        }

        public decimal NetProfit {
            get {
                return netProfit;
            }
        }
    }
}
