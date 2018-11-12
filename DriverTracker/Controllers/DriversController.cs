using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DriverTracker.Models;
using DriverTracker.Domain;

using Microsoft.AspNetCore.Authorization;

namespace DriverTracker.Controllers
{
    public class DriversController : Controller
    {
        private readonly MvcDriverContext _context;
        private readonly DriverStatistics _driverStatisticsService;

        public DriversController(MvcDriverContext context)
        {
            _context = context;
            _driverStatisticsService = new DriverStatistics(_context);
        }

        // GET: Drivers
        [Authorize(Roles = "Admin,Driver")]
        public async Task<IActionResult> Index()
        {
            _driverStatisticsService.ComputeCompanyStatistics();
            // total drivers
            int numOfDrivers = _driverStatisticsService.NumOfDrivers;
            ViewData["NumberOfDrivers"] = numOfDrivers + " driver" + (numOfDrivers == 1 ? "" : "s");

            // total pickups
            int pickups = _driverStatisticsService.Pickups;
            ViewData["Pickups"] = pickups + " passenger pickup" + (pickups == 1 ? "" : "s");

            // total miles driven
            decimal milesDriven = _driverStatisticsService.MilesDriven;
            ViewData["MilesDriven"] = milesDriven + " mile" + ((milesDriven > 0 && milesDriven < 1) ? "" : "s") + " driven";


            // average pickup delay in minutes
            if (_driverStatisticsService.AveragePickupDelay.HasValue)
            {
                double avgPickupDelay = _driverStatisticsService.AveragePickupDelay.Value;
                ViewData["AveragePickupDelay"] = avgPickupDelay + " minute" + ((avgPickupDelay > 0 && avgPickupDelay < 1) ? "" : "s");
            }

            decimal totalFares = _driverStatisticsService.TotalFares;
            ViewData["TotalFares"] = "$" + totalFares;

            decimal totalCosts = _driverStatisticsService.TotalCosts;
            ViewData["TotalCosts"] = "$" + totalCosts;

            decimal netProfit = _driverStatisticsService.NetProfit;
            ViewData["NetProfit"] = "$" + netProfit;

            return View(await _context.Drivers.ToListAsync());
        }

        // GET: Drivers/Details/5
        [Authorize(Roles = "Admin,Driver")]
        public async Task<IActionResult> Details(int? id)
        {
            _driverStatisticsService.ComputeDriverStatistics(id.Value);
            // total pickups
            int pickups = _driverStatisticsService.GetPickupsBy(id.Value);
            ViewData["Pickups"] = pickups + " passenger pickup" + (pickups == 1 ? "" : "s");

            // total miles driven
            decimal milesDriven = _driverStatisticsService.GetMilesDrivenBy(id.Value);
            ViewData["MilesDriven"] = milesDriven + " mile" + ((milesDriven > 0 && milesDriven < 1) ? "" : "s") + " driven";

            // average pickup delay in minutes
            if (_driverStatisticsService.GetAveragePickupDelayBy(id.Value).HasValue)
            {
                double avgPickupDelay = _driverStatisticsService.GetAveragePickupDelayBy(id.Value).Value;
                ViewData["AveragePickupDelay"] = avgPickupDelay + " minute" + ((avgPickupDelay > 0 && avgPickupDelay < 1) ? "" : "s");
            }

            decimal totalFares = _driverStatisticsService.GetTotalFaresBy(id.Value);
            ViewData["TotalFares"] = "$" + totalFares;

            decimal totalCosts = _driverStatisticsService.GetTotalCostsBy(id.Value);
            ViewData["TotalCosts"] = "$" + totalCosts;

            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.DriverID == id);
            if (driver == null)
            {
                return NotFound();
            }

            await _context.Legs.Where(leg => leg.DriverID == driver.DriverID).LoadAsync();

            return View(driver);
        }

        // GET: Drivers/Create
        [Authorize(Roles = "Admin,Driver")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Driver")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DriverID,UserID,Name,LicenseNumber")] Driver driver)
        {
            if (ModelState.IsValid)
            {
                _context.Add(driver);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(driver);
        }

        // GET: Drivers/Edit/5
        [Authorize(Roles = "Admin,Driver")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers.FindAsync(id);
            if (driver == null)
            {
                return NotFound();
            }
            return View(driver);
        }

        // POST: Drivers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Driver")]
        public async Task<IActionResult> Edit(int id, [Bind("DriverID,UserID,Name,LicenseNumber")] Driver driver)
        {
            if (id != driver.DriverID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(driver);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DriverExists(driver.DriverID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(driver);
        }

        // GET: Drivers/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var driver = await _context.Drivers
                .FirstOrDefaultAsync(m => m.DriverID == id);
            if (driver == null)
            {
                return NotFound();
            }

            return View(driver);
        }

        // POST: Drivers/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var driver = await _context.Drivers.FindAsync(id);
            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DriverExists(int id)
        {
            return _context.Drivers.Any(e => e.DriverID == id);
        }
    }
}
