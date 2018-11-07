using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


using DriverTracker.Models;

namespace DriverTracker.Controllers
{
    public class AnalystsController : Controller
    {
        private readonly MvcDriverContext _context;

        public AnalystsController(MvcDriverContext context)
        {
            _context = context;
        }

        // GET: Analysts
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Analysts.ToListAsync());
        }

        // GET: Analysts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analyst = await _context.Analysts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (analyst == null)
            {
                return NotFound();
            }

            return View(analyst);
        }

        // GET: Analysts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Analysts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,Username,FullName,Email,PhoneNumber,SMSNumber,AccountStatus,ReceivesSMSAlertsNewDrivers,ReceivesSMSAlertsDriversTerminated,ReceivesSMSAlertsLongDriverWaits,SMSAlertDriverWaitTime")] Analyst analyst)
        {
            if (ModelState.IsValid)
            {
                _context.Add(analyst);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(analyst);
        }

        // GET: Analysts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analyst = await _context.Analysts.FindAsync(id);
            if (analyst == null)
            {
                return NotFound();
            }
            return View(analyst);
        }

        // POST: Analysts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,Username,FullName,Email,PhoneNumber,SMSNumber,AccountStatus,ReceivesSMSAlertsNewDrivers,ReceivesSMSAlertsDriversTerminated,ReceivesSMSAlertsLongDriverWaits,SMSAlertDriverWaitTime")] Analyst analyst)
        {
            if (id != analyst.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(analyst);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalystExists(analyst.ID))
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
            return View(analyst);
        }

        // GET: Analysts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analyst = await _context.Analysts
                .FirstOrDefaultAsync(m => m.ID == id);
            if (analyst == null)
            {
                return NotFound();
            }

            return View(analyst);
        }

        // POST: Analysts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var analyst = await _context.Analysts.FindAsync(id);
            _context.Analysts.Remove(analyst);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnalystExists(int id)
        {
            return _context.Analysts.Any(e => e.ID == id);
        }
    }
}
