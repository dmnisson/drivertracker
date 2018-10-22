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
    public class LegsController : Controller
    {
        private readonly MvcDriverContext _context;

        public LegsController(MvcDriverContext context)
        {
            _context = context;
        }

        // GET: Legs
        public async Task<IActionResult> Index()
        {
            var mvcDriverContext = _context.Legs.Include(l => l.Driver);
            return View(await mvcDriverContext.ToListAsync());
        }

        // GET: Legs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

            return View(leg);
        }

        // GET: Legs/Create/1
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID");
            }
            else
            {
                ViewData["DriverID"] = new SelectList(new int[] {id.Value});
            }
            ViewData["PreviousLegID"] = new SelectList(_context.Legs, "LegID", "LegID");
            return View();
        }

        // POST: Legs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LegID,DriverID,PreviousLegID,StartAddress,PickupRequestTime,StartTime,DestinationAddress,ArrivalTime,Distance,Fare,NumOfPassengersAboard,PreviousLeg")] Leg leg)
        {
            if (ModelState.IsValid)
            {
                _context.Add(leg);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID", leg.DriverID);
            return View(leg);
        }

        // GET: Legs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leg = await _context.Legs.FindAsync(id);
            if (leg == null)
            {
                return NotFound();
            }
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID", leg.DriverID);
            return View(leg);
        }

        // POST: Legs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LegID,DriverID,StartAddress,PickupRequestTime,StartTime,DestinationAddress,ArrivalTime,Distance,Fare,NumOfPassengersAboard")] Leg leg)
        {
            if (id != leg.LegID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID", leg.DriverID);
            return View(leg);
        }

        // GET: Legs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
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

            return View(leg);
        }

        // POST: Legs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leg = await _context.Legs.FindAsync(id);
            _context.Legs.Remove(leg);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LegExists(int id)
        {
            return _context.Legs.Any(e => e.LegID == id);
        }
    }
}
