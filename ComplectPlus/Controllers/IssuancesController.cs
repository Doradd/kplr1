using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ComplectPlus.Data;
using ComplectPlus.Models;

namespace ComplectPlus.Controllers
{
    public class IssuancesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssuancesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Issuances
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Issuances.Include(i => i.Staffs);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Issuances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Issuances == null)
            {
                return NotFound();
            }

            var issuance = await _context.Issuances
                .Include(i => i.Staffs)
                .FirstOrDefaultAsync(m => m.IssuanceId == id);
            if (issuance == null)
            {
                return NotFound();
            }

            return View(issuance);
        }

        // GET: Issuances/Create
        public IActionResult Create()
        {
           
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Surname");
            return View();
        }

        // POST: Issuances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IssuanceId,ComponentId,StaffId,DateIssuance,Quantity")] Issuance issuance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(issuance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Name", issuance.StaffId);
            return View(issuance);
        }

        // GET: Issuances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Issuances == null)
            {
                return NotFound();
            }

            var issuance = await _context.Issuances.FindAsync(id);
            if (issuance == null)
            {
                return NotFound();
            }
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Name", issuance.StaffId);
            return View(issuance);
        }

        // POST: Issuances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IssuanceId,ComponentId,StaffId,DateIssuance,Quantity")] Issuance issuance)
        {
            if (id != issuance.IssuanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issuance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssuanceExists(issuance.IssuanceId))
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
            ViewData["StaffId"] = new SelectList(_context.Staffs, "StaffId", "Name", issuance.StaffId);
            return View(issuance);
        }

        // GET: Issuances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Issuances == null)
            {
                return NotFound();
            }

            var issuance = await _context.Issuances
                .Include(i => i.Staffs)
                .FirstOrDefaultAsync(m => m.IssuanceId == id);
            if (issuance == null)
            {
                return NotFound();
            }

            return View(issuance);
        }

        // POST: Issuances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Issuances == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Issuances'  is null.");
            }
            var issuance = await _context.Issuances.FindAsync(id);
            if (issuance != null)
            {
                _context.Issuances.Remove(issuance);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssuanceExists(int id)
        {
          return (_context.Issuances?.Any(e => e.IssuanceId == id)).GetValueOrDefault();
        }
    }
}
