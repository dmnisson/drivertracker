using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using DriverTracker.Models;
using System.Threading.Tasks;

namespace DriverTracker.Domain
{
    public class DriverStatistics
    {
        private readonly IDriverRepository _driverRepository;
        private readonly ILegRepository _legRepository;

        private int numOfDrivers; // total drivers
        private int pickups; // number of pickups
        private decimal milesDriven; // total miles driven company-wide
        private double? averagePickupDelay; // average pickup delay in minutes
        private decimal totalFares; // total revenue from fares
        private decimal totalCosts; // total fuel costs
        private decimal netProfit;

        private Dictionary<int, DriverStatisticResults> driverStats;

        public DriverStatistics(IDriverRepository driverRepository, ILegRepository legRepository)
        {
            _driverRepository = driverRepository;
            _legRepository = legRepository;
        }

        public async Task ComputeCompanyStatistics() {
            numOfDrivers = await _driverRepository.CountAsync();

            IEnumerable<Leg> legs = await _legRepository.ListAsync();

            pickups = legs.Select(leg => leg.NumOfPassengersPickedUp).Sum();
            milesDriven = legs.Select(leg => leg.Distance).Sum();

            if (await _legRepository.CountAsync() > 0)
                averagePickupDelay = legs.Select(leg =>
                leg.StartTime.Subtract(leg.PickupRequestTime.GetValueOrDefault(leg.StartTime)).TotalMinutes).Average();

            totalFares = legs.Select(leg => leg.Fare * leg.NumOfPassengersAboard).Sum();
            totalCosts = legs.Select(leg => leg.GetTotalFuelCost()).Sum();
            netProfit = totalFares - totalCosts;
        }

        public async Task ComputeDriverStatistics(int id) {
            if (driverStats == null) {
                driverStats = new Dictionary<int, DriverStatisticResults>();
            }

            Driver driver = await _driverRepository.GetAsync(id);
            if (driver == null) {
                return;
            }

            IEnumerable<Leg> legs = await _legRepository.ListForDriverAsync(id);

            DriverStatisticResults results = new DriverStatisticResults();
            results.DriverID = id;
            results.Pickups = legs.Select(leg => leg.NumOfPassengersPickedUp).Sum();
            results.MilesDriven = legs.Select(leg => leg.Distance).Sum();
            if (await _legRepository.CountDriverLegsAsync(id) > 0)
            {
                results.AveragePickupDelay = legs.Select(leg => 
                     leg.StartTime.Subtract(leg.PickupRequestTime.GetValueOrDefault(leg.StartTime)).TotalMinutes).Average();
            }

            results.TotalFares = legs.Select(leg => leg.Fare * leg.NumOfPassengersAboard).Sum();

            results.TotalCosts = legs.Select(leg => leg.GetTotalFuelCost()).Sum();

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

        public DriverStatisticResults GetDriverStatisticResults(int id)
        {
            return driverStats[id];
        }
    }
}
