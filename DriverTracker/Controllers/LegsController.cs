using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DriverTracker.Models;

using Microsoft.AspNetCore.Authorization;

namespace DriverTracker.Controllers
{
    public class LegsController : Controller
    {
        private readonly MvcDriverContext _context;
        private readonly IAuthorizationService _authorizationService;

        public LegsController(MvcDriverContext context,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _authorizationService = authorizationService;
        }

        // GET: Legs
        [Authorize(Roles = "Admin,Analyst")]
        public async Task<IActionResult> Index()
        {
            var mvcDriverContext = _context.Legs.Include(l => l.Driver);
            return View(await mvcDriverContext.ToListAsync());
        }

        // GET: Legs/Details/5
        public async Task<IActionResult> Details(int? id, int? driver)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            if (id == null)
            {
                return NotFound();
            }

            var leg = await _context.Legs
                .Include(l => l.Driver)
                .FirstOrDefaultAsync(m => m.LegID == id);
            if (leg == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, leg.Driver, "DriverInfoPolicy");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            if (driver != null)
            {
                var referralDriver = await _context.Drivers.FirstOrDefaultAsync(m => m.DriverID == driver);
                var refAuthResult = await _authorizationService.AuthorizeAsync(User, referralDriver, "DriverInfoPolicy");
                if (refAuthResult.Succeeded)
                    ViewData["DriverID"] = driver;
            }

            return View(leg);
        }

        // GET: Legs/Create/1
        public async Task<IActionResult> Create(int? id, bool? fromdriver)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            if (id == null)
            {
                ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID");
            }
            else
            {
                var driver = await _context.Drivers.FirstOrDefaultAsync(m => m.DriverID == id);
                var authResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicy");

                if (!authResult.Succeeded)
                {
                    return Forbid();
                }
            }

            ViewData["FromDriverPage"] = fromdriver.GetValueOrDefault(false) ? id : null;

            return View();
        }

        // POST: Legs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(bool? fromdriver, [Bind("LegID,DriverID,StartAddress,PickupRequestTime,StartTime,DestinationAddress,ArrivalTime,Distance,Fare,NumOfPassengersAboard,NumOfPassengersPickedUp,FuelCost")] Leg leg)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            if (ModelState.IsValid)
            {
                var driver = await _context.Drivers.FirstOrDefaultAsync(m => m.DriverID == leg.DriverID);
                var authResult = await _authorizationService.AuthorizeAsync(User, driver, "DriverInfoPolicy");
                if (!authResult.Succeeded)
                {
                    return Forbid();
                }

                _context.Add(leg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DriverTracker.Controllers.DriversController.Details),
                                        "Drivers",
                                        new { id = leg.DriverID });
            }
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID", leg.DriverID);

            ViewData["FromDriverPage"] = fromdriver.GetValueOrDefault(false) ? leg.DriverID as int? : null;

            return View(leg);
        }

        // GET: Legs/Edit/5
        public async Task<IActionResult> Edit(int? id, int? driver)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            if (id == null)
            {
                return NotFound();
            }

            var leg = await _context.Legs.Include(l => l.Driver)
                .FirstOrDefaultAsync(l => l.LegID == id);
            if (leg == null)
            {
                return NotFound();
            }

            var driverObj = leg.Driver;
            var authResult = await _authorizationService.AuthorizeAsync(User, driverObj, "DriverInfoPolicy");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID", leg.DriverID);

            if (driver != null)
            {
                var referralDriver = await _context.Drivers.FirstOrDefaultAsync(m => m.DriverID == driver);
                var refAuthResult = await _authorizationService.AuthorizeAsync(User, referralDriver, "DriverInfoPolicy");
                if (refAuthResult.Succeeded)
                    ViewData["FromDriverPage"] = driver;
            }
                    

            return View(leg);
        }

        // POST: Legs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int? driver, [Bind("LegID,DriverID,StartAddress,PickupRequestTime,StartTime,DestinationAddress,ArrivalTime,Distance,Fare,NumOfPassengersAboard,NumOfPassengersPickedUp,FuelCost")] Leg leg)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            if (id != leg.LegID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var driverObj = await _context.Drivers.FirstOrDefaultAsync(m => m.DriverID == leg.DriverID);
                var authResult = await _authorizationService.AuthorizeAsync(User, driverObj, "DriverInfoPolicy");

                if (!authResult.Succeeded)
                {
                    return Forbid();
                }

                try
                {
                    _context.Update(leg);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LegExists(leg.LegID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(DriverTracker.Controllers.DriversController.Details),
                                        "Drivers",
                                        new { id = leg.DriverID });
            }
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID", leg.DriverID);

            if (driver != null)
            {
                var referralDriver = await _context.Drivers.FirstOrDefaultAsync(m => m.DriverID == driver);
                var refAuthResult = await _authorizationService.AuthorizeAsync(User, referralDriver, "DriverInfoPolicy");
                if (refAuthResult.Succeeded)
                    ViewData["FromDriverPage"] = driver;
            }

            return View(leg);
        }

        // GET: Legs/Delete/5
        public async Task<IActionResult> Delete(int? id, int? driver)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }

            if (id == null)
            {
                return NotFound();
            }

            var leg = await _context.Legs
                .Include(l => l.Driver)
                .FirstOrDefaultAsync(m => m.LegID == id);
            if (leg == null)
            {
                return NotFound();
            }

            var authResult = await _authorizationService.AuthorizeAsync(User, leg.Driver, "DriverInfoPolicy");
            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            if (driver != null)
            {
                var referralDriver = await _context.Drivers.FirstOrDefaultAsync(m => m.DriverID == driver);
                var refAuthResult = await _authorizationService.AuthorizeAsync(User, referralDriver, "DriverInfoPolicy");
                if (refAuthResult.Succeeded)
                    ViewData["FromDriverPage"] = driver;
            }

            return View(leg);
        }

        // POST: Legs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int? driver)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Challenge();
            }


            var leg = await _context.Legs.Include(l => l.Driver).FirstOrDefaultAsync(l => l.LegID == id);

            var authResult = await _authorizationService.AuthorizeAsync(User, leg.Driver, "DriverInfoPolicy");

            if (!authResult.Succeeded)
            {
                return Forbid();
            }

            _context.Legs.Remove(leg);
            await _context.SaveChangesAsync();
            if (driver == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                var referralDriver = await _context.Drivers.FirstOrDefaultAsync(m => m.DriverID == driver);
                var refAuthResult = await _authorizationService.AuthorizeAsync(User, referralDriver, "DriverInfoPolicy");
                if (refAuthResult.Succeeded)
                {
                    return RedirectToAction(nameof(DriverTracker.Controllers.DriversController.Details),
                                            "Drivers",
                                            new { id = leg.DriverID });
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
        }

        private bool LegExists(int id)
        {
            return _context.Legs.Any(e => e.LegID == id);
        }
    }
}
