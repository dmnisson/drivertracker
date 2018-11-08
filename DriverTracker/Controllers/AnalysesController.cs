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
    public class AnalysesController : Controller
    {
        private readonly MvcDriverContext _context;

        public AnalysesController(MvcDriverContext context)
        {
            _context = context;
        }

        // GET: Analyses
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var mvcDriverContext = _context.Analyses.Include(a => a.Analyst).Include(a => a.Driver);
            return View(await mvcDriverContext.ToListAsync());
        }

        // GET: Analyses/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analysis = await _context.Analyses
                .Include(a => a.Analyst)
                .Include(a => a.Driver)
                .FirstOrDefaultAsync(m => m.AnalysisID == id);
            if (analysis == null)
            {
                return NotFound();
            }

            return View(analysis);
        }

        // GET: Analyses/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AnalystID"] = new SelectList(_context.Analysts, "ID", "ID");
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID");
            return View();
        }

        // POST: Analyses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AnalysisID,AnalystID,DriverID")] Analysis analysis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(analysis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AnalystID"] = new SelectList(_context.Analysts, "ID", "ID", analysis.AnalystID);
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID", analysis.DriverID);
            return View(analysis);
        }

        // GET: Analyses/Edit/5
        [Authorize(Roles = "Admin,Analyst")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analysis = await _context.Analyses.FindAsync(id);
            if (analysis == null)
            {
                return NotFound();
            }
            ViewData["AnalystID"] = new SelectList(_context.Analysts, "ID", "ID", analysis.AnalystID);
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID", analysis.DriverID);
            return View(analysis);
        }

        // POST: Analyses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin,Analyst")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AnalysisID,AnalystID,DriverID")] Analysis analysis)
        {
            if (id != analysis.AnalysisID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(analysis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnalysisExists(analysis.AnalysisID))
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
            ViewData["AnalystID"] = new SelectList(_context.Analysts, "ID", "ID", analysis.AnalystID);
            ViewData["DriverID"] = new SelectList(_context.Drivers, "DriverID", "DriverID", analysis.DriverID);
            return View(analysis);
        }

        // GET: Analyses/Delete/5
        [Authorize(Roles="Admin,Analyst")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var analysis = await _context.Analyses
                .Include(a => a.Analyst)
                .Include(a => a.Driver)
                .FirstOrDefaultAsync(m => m.AnalysisID == id);
            if (analysis == null)
            {
                return NotFound();
            }

            return View(analysis);
        }

        // POST: Analyses/Delete/5
        [Authorize(Roles = "Admin,Analyst")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var analysis = await _context.Analyses.FindAsync(id);
            _context.Analyses.Remove(analysis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnalysisExists(int id)
        {
            return _context.Analyses.Any(e => e.AnalysisID == id);
        }
    }
}
