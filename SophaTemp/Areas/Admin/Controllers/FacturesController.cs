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
    public class FacturesController : Controller
    {
        private readonly AppDbContext _context;

        public FacturesController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> GetCommandes(string term)
        {
            // Si term n'est pas numérique, retourner une liste vide ou gérer autrement.
            if (!int.TryParse(term, out var numericTerm))
            {
                return Json(Enumerable.Empty<dynamic>());
            }

            var commandes = await _context.Commandes
                .Where(c => c.CommandeId == numericTerm) // Recherche par CommandeId
                .Select(c => new {  value = c.CommandeId })
                .ToListAsync();

            return Json(commandes);
        }


        // GET: Admin/Factures
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Factures.Include(f => f.Commande);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Factures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Factures == null)
            {
                return NotFound();
            }

            var facture = await _context.Factures
                .Include(f => f.Commande)
                .FirstOrDefaultAsync(m => m.FactureId == id);
            if (facture == null)
            {
                return NotFound();
            }

            return View(facture);
        }

        // GET: Admin/Factures/Create
        public IActionResult Create()
        {
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "CommandeId", "CommandeId");
            return View();
        }

        // POST: Admin/Factures/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FactureVm factureVm)
        {
            if (ModelState.IsValid)
            {
                FactureMapper factureMapper = new FactureMapper();
                Facture newfacture = factureMapper.FactureVmFacture(factureVm);


                _context.Add(newfacture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "CommandeId", "CommandeId", factureVm.CommandeId);
            return View(factureVm);
        }

        // GET: Admin/Factures/Edit/5
        // GET: Admin/Factures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Factures == null)
            {
                return NotFound();
            }


            var facture = await _context.Factures.FindAsync(id);
            if (facture == null)
            {
                return NotFound();
            }

            // Assurez-vous de peupler le ViewBag avec les données nécessaires
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "CommandeId", "CommandeId", facture.CommandeId);
            return View(facture);
        }


        // POST: Admin/Factures/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FactureId,Numero,Montant,DateFacturation,CommandeId")] Facture facture)
        {
            if (id != facture.FactureId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(facture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactureExists(facture.FactureId))
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
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "CommandeId", "CommandeId", facture.CommandeId);
            return View(facture);
        }



        // GET: Admin/Factures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Factures == null)
            {
                return NotFound();
            }

            var facture = await _context.Factures
                .Include(f => f.Commande)
                .FirstOrDefaultAsync(m => m.FactureId == id);
            if (facture == null)
            {
                return NotFound();
            }

            return View(facture);
        }

        // POST: Admin/Factures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Factures == null)
            {
                return Problem("Entity set 'AppDbContext.Factures'  is null.");
            }
            var facture = await _context.Factures.FindAsync(id);
            if (facture != null)
            {
                _context.Factures.Remove(facture);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FactureExists(int id)
        {
          return (_context.Factures?.Any(e => e.FactureId == id)).GetValueOrDefault();
        }
    }
}
