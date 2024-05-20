using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Filter;
using SophaTemp.Mappers;
using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PasseportAuthorizationFilter("AdminCommandes")]

    public class LotsController : Controller
    {
        private readonly AppDbContext _context;

        public LotsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Lots
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Lots.Include(l => l.Fournisseur).Include(l => l.Medicament);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Lots/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Lots == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots
                .Include(l => l.Fournisseur)
                .Include(l => l.Medicament)
                .FirstOrDefaultAsync(m => m.LotId == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        [Route("[controller]/[action]/{id}")]
        public IActionResult GetByMedicamentId(int id)
        {
            // Ajout de condiction IsPublic = false
            return View(_context.Lots.Where(l => l.MedicamentId == id && l.IsPublic == false).ToList());
        }
        // GET: Admin/Lots/Create
        public IActionResult Create() 
        {
            ViewData["FournisseurId"] = new SelectList(_context.Fournisseurs.OrderBy(f => f.NomComplet), "FournisseurId", "NomComplet");
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments.OrderBy(m => m.Nom), "MedicamentId", "Nom");

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LotAddVm lot)
        {
            if (ModelState.IsValid)
            {
                // Déboguez les valeurs de MedicamentId et FournisseurId
                Console.WriteLine($"MedicamentId soumis: {lot.MedicamentId}");
                Console.WriteLine($"FournisseurId soumis: {lot.FournisseurId}");

                // Vérifiez que le MedicamentId existe dans la table Medicament
                var medicamentExists = await _context.Medicaments.AnyAsync(m => m.MedicamentId == lot.MedicamentId);
                if (!medicamentExists)
                {
                    ModelState.AddModelError("MedicamentId", "Le médicament spécifié n'existe pas.");
                }

                // Vérifiez que le FournisseurId existe dans la table Fournisseur
                var fournisseurExists = await _context.Fournisseurs.AnyAsync(f => f.FournisseurId == lot.FournisseurId);
                if (!fournisseurExists)
                {
                    ModelState.AddModelError("FournisseurId", "Le fournisseur spécifié n'existe pas.");
                }

                if (!ModelState.IsValid)
                {
                    // Rechargez les dropdowns s'il y a des erreurs de validation
                    ViewData["FournisseurId"] = new SelectList(_context.Fournisseurs.OrderBy(f => f.NomComplet), "FournisseurId", "NomComplet", lot.FournisseurId);
                    ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Nom", lot.MedicamentId);
                    return View(lot);
                }

                // Mapper et créer le nouveau lot
                LotsMapper lotsMapper = new LotsMapper();
                Lot newLot = lotsMapper.LotaddVmLot(lot);
                _context.Add(newLot);
                await _context.SaveChangesAsync();
                Console.WriteLine($"Montant soumis: {lot.Montant}");
                return RedirectToAction(nameof(Index));
            }

            // Rechargez les dropdowns s'il y a des erreurs de validation
            ViewData["FournisseurId"] = new SelectList(_context.Fournisseurs.OrderBy(f => f.NomComplet), "FournisseurId", "NomComplet", lot.FournisseurId);
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments.OrderBy(m => m.Nom), "MedicamentId", "Nom", lot.MedicamentId);
            return View(lot);
        }


        // GET: Admin/Lots/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Lots == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots.FindAsync(id);
            if (lot == null)
            {
                return NotFound();
            }

            ViewData["FournisseurId"] = new SelectList(_context.Fournisseurs.OrderBy(f => f.NomComplet), "FournisseurId", "NomComplet", lot.FournisseurId);
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Nom", lot.MedicamentId);
            return View(lot);
        }

        // POST: Admin/Lots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // POST: Admin/Lots/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LotId,Montant,Quantite,PrixAchat,PrixVente,DateDeProduction,DateDExpedition,MedicamentId,FournisseurId")] Lot lot)
        {
            if (id != lot.LotId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // Débogage pour vérifier les valeurs soumises
                Console.WriteLine($"LotId soumis: {lot.LotId}");
                Console.WriteLine($"Montant soumis: {lot.Montant}");
                Console.WriteLine($"Quantite soumise: {lot.Quantite}");
                Console.WriteLine($"PrixAchat soumis: {lot.PrixAchat}");
                Console.WriteLine($"PrixVente soumis: {lot.PrixVente}");
                Console.WriteLine($"DateDeProduction soumise: {lot.DateDeProduction}");
                Console.WriteLine($"DateDExpedition soumise: {lot.DateDExpedition}");
                Console.WriteLine($"MedicamentId soumis: {lot.MedicamentId}");
                Console.WriteLine($"FournisseurId soumis: {lot.FournisseurId}");

                try
                {
                    _context.Update(lot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotExists(lot.LotId))
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

            // Rechargez les dropdowns s'il y a des erreurs de validation
            ViewData["FournisseurId"] = new SelectList(_context.Fournisseurs.OrderBy(f => f.NomComplet), "FournisseurId", "NomComplet", lot.FournisseurId);
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Nom", lot.MedicamentId);
            return View(lot);
        }


        // GET: Admin/Lots/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Lots == null)
            {
                return NotFound();
            }

            var lot = await _context.Lots
                .Include(l => l.Fournisseur)
                .Include(l => l.Medicament)
                .FirstOrDefaultAsync(m => m.LotId == id);
            if (lot == null)
            {
                return NotFound();
            }

            return View(lot);
        }

        // POST: Admin/Lots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Lots == null)
            {
                return Problem("Entity set 'AppDbContext.Lots'  is null.");
            }
            var lot = await _context.Lots.FindAsync(id);
            if (lot != null)
            {
                _context.Lots.Remove(lot);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LotExists(int id)
        {
          return (_context.Lots?.Any(e => e.LotId == id)).GetValueOrDefault();
        }

        
            public async Task<IActionResult> RPublic(int id)
        {
            if (_context.Lots == null)
            {
                return Problem("Entity set 'AppDbContext.Lots'  is null.");
            }
            var lot = await _context.Lots.FindAsync(id);
            lot.IsPublic = true;
            var lotPublic = await _context.Lots.Where(l => l.MedicamentId == lot.MedicamentId && l.IsPublic == true ).FirstOrDefaultAsync();

            if(lotPublic != null)
            {
                lotPublic.IsPublic = false;
            }
         

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
