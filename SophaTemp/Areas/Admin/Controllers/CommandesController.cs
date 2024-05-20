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
using SophaTemp.Filter;


namespace SophaTemp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PasseportAuthorizationFilter("AdminCommandes")]

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


        // GET: Admin/Commandes/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.clients, "ClientId", "LibellePharmacie");
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments.Select(m => new { m.MedicamentId, m.Nom }), "MedicamentId", "Nom");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommandeVm commandeVm)
        {
            if (ModelState.IsValid)
            {
                var commande = _commandeMapper.CommandeFromVm(commandeVm);

                // Ajouter les lots de commande au contexte
                foreach (var lotCommande in commande.LotsCommande)
                {
                    _context.LotCommandes.Add(lotCommande);
                }

                _context.Add(commande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientId"] = new SelectList(_context.clients, "ClientId", "LibellePharmacie", commandeVm.ClientId);
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments, "MedicamentId", "Nom", commandeVm.MedicamentId);

            return View(commandeVm);
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
