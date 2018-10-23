using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DriverTracker.Models;

namespace DriverTracker.Controllers
{
    public class DriversController : Controller
    {
        private readonly MvcDriverContext _context;

        public DriversController(MvcDriverContext context)
        {
            _context = context;
        }

        // GET: Drivers
        public async Task<IActionResult> Index()
        {
            // total drivers
            int numOfDrivers = await _context.Drivers.CountAsync();
            ViewData["NumberOfDrivers"] = numOfDrivers + " driver" + (numOfDrivers == 1 ? "" : "s");

            // total pickups
            int pickups = await _context.Legs.Select(leg => (leg.PreviousLeg == null) ? leg.NumOfPassengersAboard : Math.Min(0, leg.NumOfPassengersAboard - leg.PreviousLeg.NumOfPassengersAboard))
                                        .SumAsync();
            ViewData["Pickups"] = pickups + " passenger pickup" + (pickups == 1 ? "" : "s");

            // total miles driven
            decimal milesDriven = await _context.Legs.Select(leg => leg.Distance).SumAsync();
            ViewData["MilesDriven"] = milesDriven + " mile" + ((milesDriven > 0 && milesDriven < 1) ? "" : "s") + " driven";

            // average pickup delay in minutes
            if (await _context.Legs.CountAsync() > 0)
            {
                double avgPickupDelay = await _context.Legs.Select(leg => leg.StartTime.Subtract(leg.PickupRequestTime.GetValueOrDefault(leg.StartTime)).TotalMinutes).AverageAsync();
                ViewData["AveragePickupDelay"] = avgPickupDelay + " minute" + ((avgPickupDelay > 0 && avgPickupDelay < 1) ? "" : "s");
            }

            return View(await _context.Drivers.ToListAsync());
        }

        // GET: Drivers/Details/5
        public async Task<IActionResult> Details(int? id)
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

            await _context.Legs.Where(leg => leg.DriverID == driver.DriverID).LoadAsync();

            return View(driver);
        }

        // GET: Drivers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Drivers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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
