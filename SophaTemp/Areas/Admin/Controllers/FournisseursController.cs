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
    public class FournisseursController : Controller
    {
        private readonly AppDbContext _context;

        public FournisseursController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Fournisseurs
        public async Task<IActionResult> Index()
        {
              return _context.Fournisseurs != null ? 
                          View(await _context.Fournisseurs.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Fournisseurs'  is null.");
        }

        // GET: Admin/Fournisseurs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Fournisseurs == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseurs
                .FirstOrDefaultAsync(m => m.FournisseurId == id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            return View(fournisseur);
        }

        // GET: Admin/Fournisseurs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Fournisseurs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FournisseurId,NomComplet")] Fournisseur fournisseur)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fournisseur);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fournisseur);
        }

        // GET: Admin/Fournisseurs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fournisseurs == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur == null)
            {
                return NotFound();
            }
            return View(fournisseur);
        }

        // POST: Admin/Fournisseurs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FournisseurId,NomComplet")] Fournisseur fournisseur)
        {
            if (id != fournisseur.FournisseurId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fournisseur);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FournisseurExists(fournisseur.FournisseurId))
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
            return View(fournisseur);
        }

        // GET: Admin/Fournisseurs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fournisseurs == null)
            {
                return NotFound();
            }

            var fournisseur = await _context.Fournisseurs
                .FirstOrDefaultAsync(m => m.FournisseurId == id);
            if (fournisseur == null)
            {
                return NotFound();
            }

            return View(fournisseur);
        }

        // POST: Admin/Fournisseurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fournisseurs == null)
            {
                return Problem("Entity set 'AppDbContext.Fournisseurs'  is null.");
            }
            var fournisseur = await _context.Fournisseurs.FindAsync(id);
            if (fournisseur != null)
            {
                _context.Fournisseurs.Remove(fournisseur);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FournisseurExists(int id)
        {
          return (_context.Fournisseurs?.Any(e => e.FournisseurId == id)).GetValueOrDefault();
        }
    }
}
