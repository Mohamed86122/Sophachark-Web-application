using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Models;

namespace SophaTemp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryMedicamentsController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryMedicamentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CategoryMedicaments
        public async Task<IActionResult> Index()
        {
              return _context.CategoryMedicament != null ? 
                          View(await _context.CategoryMedicament.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.CategoryMedicament'  is null.");
        }

        // GET: Admin/CategoryMedicaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CategoryMedicament == null)
            {
                return NotFound();
            }

            var categoryMedicament = await _context.CategoryMedicament
                .FirstOrDefaultAsync(m => m.CategoryMedicamentId == id);
            if (categoryMedicament == null)
            {
                return NotFound();
            }

            return View(categoryMedicament);
        }

        // GET: Admin/CategoryMedicaments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CategoryMedicaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryMedicamentId,Reference,Libelle")] CategoryMedicament categoryMedicament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoryMedicament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryMedicament);
        }

        // GET: Admin/CategoryMedicaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CategoryMedicament == null)
            {
                return NotFound();
            }

            var categoryMedicament = await _context.CategoryMedicament.FindAsync(id);
            if (categoryMedicament == null)
            {
                return NotFound();
            }
            return View(categoryMedicament);
        }

        // POST: Admin/CategoryMedicaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryMedicamentId,Reference,Libelle")] CategoryMedicament categoryMedicament)
        {
            if (id != categoryMedicament.CategoryMedicamentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoryMedicament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryMedicamentExists(categoryMedicament.CategoryMedicamentId))
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
            return View(categoryMedicament);
        }

        // GET: Admin/CategoryMedicaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CategoryMedicament == null)
            {
                return NotFound();
            }

            var categoryMedicament = await _context.CategoryMedicament
                .FirstOrDefaultAsync(m => m.CategoryMedicamentId == id);
            if (categoryMedicament == null)
            {
                return NotFound();
            }

            return View(categoryMedicament);
        }

        // POST: Admin/CategoryMedicaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CategoryMedicament == null)
            {
                return Problem("Entity set 'AppDbContext.CategoryMedicament'  is null.");
            }
            var categoryMedicament = await _context.CategoryMedicament.FindAsync(id);
            if (categoryMedicament != null)
            {
                _context.CategoryMedicament.Remove(categoryMedicament);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryMedicamentExists(int id)
        {
          return (_context.CategoryMedicament?.Any(e => e.CategoryMedicamentId == id)).GetValueOrDefault();
        }
    }
}
