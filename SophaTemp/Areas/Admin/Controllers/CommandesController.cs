using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommandesController : Controller
    {
        private readonly AppDbContext _context;

        public CommandesController(AppDbContext context)
        {
            _context = context;
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
                .Select(l => new { l.DateDExpedition, l.Quantite })
                .ToListAsync();
            return Json(lots);
        }

        public IActionResult Create()
        {
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Nom");
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Nom");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommandeVm commandeVm)
        {
            if (ModelState.IsValid)
            {
                var commande = new Commande
                {
                    Numero = "Num-" + DateTime.Now.Ticks, // Generate a number for demonstration
                    DateCommande = commandeVm.DateCommande,
                    Status = commandeVm.Status,
                    IdLotCommande = 1 // Assuming you manage to get this ID correctly from the lot selection
                };

                _context.Add(commande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Nom", commandeVm.MedicamentId);
            ViewData["ClientId"] = new SelectList(_context.Clients, "ClientId", "Nom", commandeVm.ClientId);
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
