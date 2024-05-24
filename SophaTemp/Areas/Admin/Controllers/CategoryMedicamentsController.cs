using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Mappers;
using SophaTemp.Models;
using SophaTemp.Viewmodel;

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
              return _context.Categories != null ? 
                          View(await _context.Categories.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Categories'  is null.");
        }

        // GET: Admin/CategoryMedicaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoryMedicament = await _context.Categories
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
        public async Task<IActionResult> Create(CategoryMedicamentVM category)
        {
            if (ModelState.IsValid)
            {
                CategoryMedicamentMapper map = new CategoryMedicamentMapper();
                CategoryMedicament categoryMedicament = map.CategoryMedicamentAddMap(category);
                _context.Add(categoryMedicament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoryMedicament = await _context.Categories.FindAsync(id);
            if (categoryMedicament == null)
            {
                return NotFound();
            }

            var categoryMedicamentVM = new CategoryMedicamentVM
            {
                Reference = categoryMedicament.Reference,
                Libelle = categoryMedicament.Libelle
            };

            ViewBag.CategoryMedicamentId = id; // Stockez l'ID dans ViewBag
            return View(categoryMedicamentVM);
        }

        // POST: Admin/CategoryMedicaments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryMedicamentVM categoryMedicamentVM)
        {
            if (ModelState.IsValid)
            {
                var mapper = new CategoryMedicamentMapper();
                var categoryMedicament = mapper.CategoryMedicamentAddMap(categoryMedicamentVM);
                categoryMedicament.CategoryMedicamentId = id; // Assignez l'ID

                try
                {
                    _context.Update(categoryMedicament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryMedicamentExists(id))
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

            ViewBag.CategoryMedicamentId = id; // Stockez l'ID dans ViewBag pour la vue en cas d'erreur de validation
            return View(categoryMedicamentVM);
        }
        // GET: Admin/CategoryMedicaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var categoryMedicament = await _context.Categories
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
            if (_context.Categories == null)
            {
                return Problem("Entity set 'AppDbContext.Categories'  is null.");
            }
            var categoryMedicament = await _context.Categories.FindAsync(id);
            if (categoryMedicament != null)
            {
                _context.Categories.Remove(categoryMedicament);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryMedicamentExists(int id)
        {
          return (_context.Categories?.Any(e => e.CategoryMedicamentId == id)).GetValueOrDefault();
        }
    }
}
