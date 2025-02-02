﻿using System;
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
    [PasseportAuthorizationFilter("AdminCommandes", "AdminPrincipale")]

    public class FacturesController : Controller
    {
        private readonly AppDbContext _context;

        private readonly FactureMapper _factureMapper;

        public FacturesController(AppDbContext context, FactureMapper factureMapper)
        {
            _context = context;
            _factureMapper = factureMapper;
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
                .Select(c => new { value = c.CommandeId })
                .ToListAsync();

            return Json(commandes);
        }


        // GET: Admin/Factures
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Factures.Include(f => f.Commande);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> CommandeDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.Commandes
                .Include(c => c.Factures) // Assurez-vous que la propriété "Factures" existe dans la classe "Commande"
                .FirstOrDefaultAsync(m => m.CommandeId == id);

            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
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
        public IActionResult Create()
        {
            ViewData["CommandeId"] = new SelectList(_context.Commandes, "CommandeId", "CommandeId");
            return View();
        }

        // POST: Admin/Factures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FactureVm factureVm)
        {
            if (ModelState.IsValid)
            {
                Facture newfacture = _factureMapper.FactureVmFacture(factureVm);

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
            if (id == null)
            {
                return NotFound();
            }

            var facture = await _context.Factures.FindAsync(id);
            if (facture == null)
            {
                return NotFound();
            }

            var factureVm = new FactureVm
            {
                Numero = facture.Numero,
                Montant = facture.Montant,
                DateFacturation = facture.DateFacturation,
                CommandeId = facture.CommandeId
            };

            ViewData["CommandeId"] = new SelectList(_context.Commandes, "CommandeId", "CommandeId", facture.CommandeId);
            return View(factureVm);
        }

        // POST: Admin/Factures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FactureVm factureVm)
        {
            if (ModelState.IsValid)
            {
                var mapper = new FactureMapper();
                var facture = mapper.FactureVmFacture(factureVm);
                facture.FactureId = id; // Assignez l'ID

                try
                {
                    _context.Update(facture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FactureExists(id))
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

            ViewData["CommandeId"] = new SelectList(_context.Commandes, "CommandeId", "CommandeId", factureVm.CommandeId);
            return View(factureVm);
        }


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
