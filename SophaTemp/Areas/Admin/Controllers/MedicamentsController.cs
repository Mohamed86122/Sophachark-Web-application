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
using SophaTemp.Services;
using SophaTemp.Viewmodel;

namespace SophaTemp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MedicamentsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IUploadFileService UploadFileService;

        public MedicamentsController(AppDbContext context, IUploadFileService UploadFileService)
        {
            _context = context;
            this.UploadFileService = UploadFileService;
        }

        // GET: Admin/Medicaments
        public async Task<IActionResult> Index()
        {
            List<Medicament> list = await _context.Medicaments.Include(m => m.MedicamentCategoryMedicaments).ToListAsync();
              return _context.Medicaments != null ? 
                          View(list ) :
                          Problem("Entity set 'AppDbContext.Medicaments'  is null.");
        }

        // GET: Admin/Medicaments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Medicaments == null)
            {
                return NotFound();
            }

            var medicament = await _context.Medicaments
                .FirstOrDefaultAsync(m => m.MedicamentId == id);
            if (medicament == null)
            {
                return NotFound();
            }

            return View(medicament);
        }

        // GET: Admin/Medicaments/Create
        public IActionResult Create()
        {

            ViewBag.CategorieIds = new SelectList(_context.Categories, "CategoryMedicamentId", "Libelle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MedicamentAddVM medicament)
        {
            if (ModelState.IsValid)
            {
                Medicament m = MedicamentMapper.MedicamentAddVmTpMedicament(medicament, _context);
                m.Image = UploadFileService.Upload(medicament.Image, "Medicaments", true);
                m.MedicamentCategoryMedicaments = new List<MedicamentCategoryMedicament>();
                foreach (var categoryId in medicament.SelectedCategorieIds)
                {
                    m.MedicamentCategoryMedicaments.Add(new MedicamentCategoryMedicament
                    {
                        CategoryMedicamentId = categoryId
                    });
                }
                _context.Add(m);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategorieIds = new SelectList(_context.Categories, "CategoryMedicamentId", "Libelle");

            return View(medicament);
        }


        // GET: Admin/Medicaments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Medicaments == null)
            {
                return NotFound();
            }

            var medicament = await _context.Medicaments.FindAsync(id);
            if (medicament == null)
            {
                return NotFound();
            }
            return View(medicament);
        }

        // POST: Admin/Medicaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MedicamentId,Reference,Nom,Description,Image,QuantiteEnAlerte")] Medicament medicament)
        {
            if (id != medicament.MedicamentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medicament);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicamentExists(medicament.MedicamentId))
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
            return View(medicament);
        }

        // GET: Admin/Medicaments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Medicaments == null)
            {
                return NotFound();
            }

            var medicament = await _context.Medicaments
                .FirstOrDefaultAsync(m => m.MedicamentId == id);
            if (medicament == null)
            {
                return NotFound();
            }

            return View(medicament);
        }

        // POST: Admin/Medicaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Medicaments == null)
            {
                return Problem("Entity set 'AppDbContext.Medicaments'  is null.");
            }
            var medicament = await _context.Medicaments.FindAsync(id);
            if (medicament != null)
            {
                _context.Medicaments.Remove(medicament);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedicamentExists(int id)
        {
          return (_context.Medicaments?.Any(e => e.MedicamentId == id)).GetValueOrDefault();
        }
    }
}
