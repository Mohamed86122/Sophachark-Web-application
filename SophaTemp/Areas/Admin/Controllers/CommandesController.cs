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
    public class CommandesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly CommandeMapper _commandeMapper;


        public CommandesController(AppDbContext context,CommandeMapper command)
        {
            _context = context;
            _commandeMapper = command;

        }

        public async Task<IActionResult> Index()
        {
            return _context.Commandes != null ?
                View(await _context.Commandes.ToListAsync()) :
                Problem("Entity set 'AppDbContext.Commandes' is null.");
        }

        public async Task<IActionResult> GetLotsByMedicamentId(int medicamentId)
        {
            var lots = await _context.Lots
                .Where(l => l.MedicamentId == medicamentId)
                .Select(l => new { l.LotId,l.DateDExpedition, l.Quantite })
                .ToListAsync();
            return Json(lots);
        }
        public async Task<IActionResult> GetLotsPartial(int? medicamentId)
        {
            if (!medicamentId.HasValue)
            {
                return PartialView("LotsPartial", new List<Lot>());  // Retourne une liste vide si aucun ID n'est fourni
            }

            var lots = await _context.Lots
                                     .Where(l => l.MedicamentId == medicamentId.Value)
                                     .ToListAsync();
            return PartialView("LotsPartial", lots);
        }

        public IActionResult Create()
        {
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Nom");
            ViewData["ClientId"] = new SelectList(_context.clients, "ClientId", "LibellePharmacie");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommandeVm commandeVm)
        {

            if (ModelState.IsValid)
            {
                var lotsDisponibles = _context.Lots
                    .Where(l => l.MedicamentId == commandeVm.MedicamentId && l.Quantite >= commandeVm.Quantite)
                    .ToList();

                var commande = _commandeMapper.CommandeMapperAddVm(commandeVm);

                if (lotsDisponibles.Any())
                {
                    _context.Add(commande);

                    foreach (var lot in lotsDisponibles)
                    {
                        lot.Quantite -= commandeVm.Quantite;
                        _context.Update(lot);
                    }

                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", $"La quantité demandée de {commandeVm.Quantite} ne peut être couverte par les lots sélectionnés.");
                }
            }

            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Nom", commandeVm.MedicamentId);
            ViewData["ClientId"] = new SelectList(_context.clients, "ClientId", "LibellePharmacie", commandeVm.ClientId);

            return View(commandeVm);

        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Commandes == null)
            {
                return NotFound();
            }

            var commande = await _context.Commandes.FindAsync(id);
            if (commande == null)
            {
                return NotFound();
            }
            return View(commande);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CommandeVm commandeVm)
        {
            if (id != commandeVm.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var commandeToUpdate = await _context.Commandes.FindAsync(id);
                if (commandeToUpdate != null)
                {
                    commandeToUpdate.DateCommande = commandeVm.DateCommande;
                    commandeToUpdate.Status = commandeVm.Status;
                    // Update other properties as needed

                    try
                    {
                        _context.Update(commandeToUpdate);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CommandeExists(commandeToUpdate.CommandeId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(commandeVm);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Commandes == null)
            {
                return NotFound();
            }

            var commande = await _context.Commandes
                .FirstOrDefaultAsync(m => m.CommandeId == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Commandes == null)
            {
                return Problem("Entity set 'AppDbContext.Commandes' is null.");
            }
            var commande = await _context.Commandes.FindAsync(id);
            if (commande != null)
            {
                _context.Commandes.Remove(commande);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool CommandeExists(int id)
        {
            return _context.Commandes.Any(e => e.CommandeId == id);
        }
    }
}
