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
    public class СomponentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public СomponentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Сomponent
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null || _context.Components == null)
            {
                return NotFound();
            }
            var applicationDbContext = _context.Components.Where(t => t.CategoryId == id).Include(с => с.Categories).Include(с => с.Manufacturers);
            string Category = _context.Categories.FirstOrDefault(t => t.CategoryId == id).CategoryN;
            ViewBag.Category = Category;
            ViewBag.CategoryId = id;
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Сomponent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Components == null)
            {
                return NotFound();
            }

            var сomponent = await _context.Components
                .Include(с => с.Categories)
                .Include(с => с.Manufacturers)
                .FirstOrDefaultAsync(m => m.ComponentId == id);
            if (сomponent == null)
            {
                return NotFound();
            }

            return View(сomponent);
        }

        // GET: Сomponent/Create
        public IActionResult Create(int? id)
        {
            if (id == null || _context.Components == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = id;
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerName");
            return View();
        }

        // POST: Сomponent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ComponentId,ComponentsName,YearRelease,CategoryId,ManufacturerId,Description")] Сomponent сomponent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(сomponent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = сomponent.CategoryId });
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", сomponent.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerName", сomponent.ManufacturerId);
            return View(сomponent);
        }

        // GET: Сomponent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Components == null)
            {
                return NotFound();
            }

            var сomponent = await _context.Components.FindAsync(id);
            if (сomponent == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", сomponent.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerName", сomponent.ManufacturerId);
            return View(сomponent);
        }

        // POST: Сomponent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ComponentId,ComponentsName,YearRelease,CategoryId,ManufacturerId,Description")] Сomponent сomponent)
        {
            if (id != сomponent.ComponentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(сomponent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!СomponentExists(сomponent.ComponentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = сomponent.CategoryId });
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", сomponent.CategoryId);
            ViewData["ManufacturerId"] = new SelectList(_context.Manufacturers, "ManufacturerId", "ManufacturerName", сomponent.ManufacturerId);
            return View(сomponent);
        }

        // GET: Сomponent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Components == null)
            {
                return NotFound();
            }

            var сomponent = await _context.Components
                .Include(с => с.Categories)
                .Include(с => с.Manufacturers)
                .FirstOrDefaultAsync(m => m.ComponentId == id);
            if (сomponent == null)
            {
                return NotFound();
            }

            return View(сomponent);
        }

        // POST: Сomponent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Components == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Components'  is null.");
            }
            var сomponent = await _context.Components.FindAsync(id);
            if (сomponent != null)
            {
                _context.Components.Remove(сomponent);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = сomponent.CategoryId });
        }

        private bool СomponentExists(int id)
        {
          return (_context.Components?.Any(e => e.ComponentId == id)).GetValueOrDefault();
        }
    }
}
