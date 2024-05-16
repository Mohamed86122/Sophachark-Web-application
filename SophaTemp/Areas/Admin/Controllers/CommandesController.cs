using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using SophaTemp.Data;
using SophaTemp.Mappers;
using SophaTemp.Viewmodel;
using SophaTemp.Models;
using ModelLotSelection = SophaTemp.Models.LotSelection;
using ViewModelLotSelection = SophaTemp.Viewmodel.LotSelection;


namespace SophaTemp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommandesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly CommandeMapper _commandeMapper;

        public CommandesController(AppDbContext context, CommandeMapper mapper)
        {
            _context = context;
            _commandeMapper = mapper;
        }

        // GET: Admin/Commandes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Commandes.Include(c => c.Client);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Commandes/Lots
        public async Task<IActionResult> Lots(int medicamentId)
        {
            var lots = await _context.Lots
                                     .Where(l => l.MedicamentId == medicamentId && l.Quantite > 0)
                                     .ToListAsync();
            return PartialView("_LotsPartial", lots);
        }

        // GET: Admin/Commandes/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.clients, "ClientId", "LibellePharmacie");
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Nom");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommandeVm commandeVm)
        {
            if (!string.IsNullOrEmpty(commandeVm.SelectedLotsString))
            {
                commandeVm.LotSelections = ParseLotSelections(commandeVm.SelectedLotsString);
            }

            if (commandeVm.LotSelections == null || !commandeVm.LotSelections.Any())
            {
                ModelState.AddModelError("LotSelections", "LotSelections cannot be null or empty.");
            }

            if (ModelState.IsValid)
            {
                var commande = _commandeMapper.CommandeFromVm(commandeVm);

                foreach (var lotSelection in commandeVm.LotSelections)
                {
                    var lot = await _context.Lots.FindAsync(lotSelection.LotId);
                    if (lot != null && lot.Quantite >= lotSelection.Quantite)
                    {
                        lot.Quantite -= lotSelection.Quantite;
                        _context.Update(lot);
                    }
                    else
                    {
                        ModelState.AddModelError("", $"Quantité insuffisante pour le lot {lotSelection.LotId}.");
                        ViewData["ClientId"] = new SelectList(_context.clients, "ClientId", "LibellePharmacie", commandeVm.ClientId);
                        ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Nom", commandeVm.MedicamentId);
                        return View(commandeVm);
                    }
                }

                commande.SelectedLotsString = commandeVm.SelectedLotsString;
                _context.Add(commande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientId"] = new SelectList(_context.clients, "ClientId", "LibellePharmacie", commandeVm.ClientId);
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Nom", commandeVm.MedicamentId);

            return View(commandeVm);
        }
        private List<Viewmodel.LotSelection> ParseLotSelections(string selectedLotsString)
        {
            var lotSelections = new List<Viewmodel.LotSelection>();

            if (string.IsNullOrWhiteSpace(selectedLotsString))
            {
                return lotSelections;
            }

            var lots = selectedLotsString.Split(';');

            foreach (var lot in lots)
            {
                var details = lot.Split(',');

                if (details.Length == 3)
                {
                    try
                    {
                        var lotIdPart = details[0].Split(':')[1].Trim();
                        var quantitePart = details[1].Split(':')[1].Trim();
                        var medicamentIdPart = details[2].Split(':')[1].Trim();

                        var lotId = int.Parse(lotIdPart);
                        var quantite = int.Parse(quantitePart);
                        var medicamentId = int.Parse(medicamentIdPart);

                        lotSelections.Add(new Viewmodel.LotSelection
                        {
                            LotId = lotId,
                            Quantite = quantite,
                            MedicamentId = medicamentId
                        });

                        // Log de débogage
                        Console.WriteLine($"Lot ajouté : Lot ID={lotId}, Quantite={quantite}, Medicament ID={medicamentId}");
                    }
                    catch (Exception ex)
                    {
                        // Ajoutez ici une gestion des erreurs si nécessaire
                        Console.WriteLine("Erreur de format ou de parsing : " + ex.Message);
                    }
                }
            }

            return lotSelections;
        }





        // GET: Admin/Commandes/Edit/5
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
            ViewData["ClientId"] = new SelectList(_context.clients, "PersonneId", "PersonneId", commande.ClientId);
            return View(commande);
        }

        // POST: Admin/Commandes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommandeId,ClientId,DateCommande,Status,Quantite,IdLotCommande")] Commande commande)
        {
            if (id != commande.CommandeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommandeExists(commande.CommandeId))
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
            ViewData["ClientId"] = new SelectList(_context.clients, "PersonneId", "PersonneId", commande.ClientId);
            return View(commande);
        }

        // GET: Admin/Commandes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Commandes == null)
            {
                return NotFound();
            }

            var commande = await _context.Commandes
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.CommandeId == id);
            if (commande == null)
            {
                return NotFound();
            }

            return View(commande);
        }

        // POST: Admin/Commandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Commandes == null)
            {
                return Problem("Entity set 'AppDbContext.Commandes'  is null.");
            }
            var commande = await _context.Commandes.FindAsync(id);
            if (commande != null)
            {
                _context.Commandes.Remove(commande);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Button Details Commande dans facture
        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            var commande = _context.Commandes.Find(id);
            if (commande == null)
            {
                return NotFound();
            }
            return View(commande);
        }

        private bool CommandeExists(int id)
        {
            return (_context.Commandes?.Any(e => e.CommandeId == id)).GetValueOrDefault();
        }
    }
}
