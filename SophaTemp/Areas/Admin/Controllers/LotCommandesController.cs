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
    public class LotCommandesController : Controller
    {
        private readonly AppDbContext _context;

        public LotCommandesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/LotCommandes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.LotCommandes.Include(l => l.Lot);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/LotCommandes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LotCommandes == null)
            {
                return NotFound();
            }

            var lotCommande = await _context.LotCommandes
                .Include(l => l.Lot)
                .FirstOrDefaultAsync(m => m.LotCommandeId == id);
            if (lotCommande == null)
            {
                return NotFound();
            }

            return View(lotCommande);
        }

        // GET: Admin/LotCommandes/Create
        public IActionResult Create()
        {
            ViewData["LotId"] = new SelectList(_context.Lots, "LotId", "Libelle");
            return View();
        }

        // POST: Admin/LotCommandes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LotCommandeVm lotc)
        {
            if (ModelState.IsValid)
            {
                LotCommandeMapper map = new LotCommandeMapper();
                LotCommande newLot = map.LotaddVmCommande(lotc);
                _context.Add(newLot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LotId"] = new SelectList(_context.Lots, "LotId", "Libelle", lotc.LotId);
            return View(lotc);
        }

        // GET: Admin/LotCommandes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LotCommandes == null)
            {
                return NotFound();
            }

            var lotCommande = await _context.LotCommandes.FindAsync(id);
            if (lotCommande == null)
            {
                return NotFound();
            }
            ViewData["LotId"] = new SelectList(_context.Lots, "LotId", "LotId", lotCommande.LotId);
            return View(lotCommande);
        }

        // POST: Admin/LotCommandes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LotCommandeId,Frais,Quantite,LotId")] LotCommande lotCommande)
        {
            if (id != lotCommande.LotCommandeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lotCommande);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LotCommandeExists(lotCommande.LotCommandeId))
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
            ViewData["LotId"] = new SelectList(_context.Lots, "LotId", "LotId", lotCommande.LotId);
            return View(lotCommande);
        }

        // GET: Admin/LotCommandes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LotCommandes == null)
            {
                return NotFound();
            }

            var lotCommande = await _context.LotCommandes
                .Include(l => l.Lot)
                .FirstOrDefaultAsync(m => m.LotCommandeId == id);
            if (lotCommande == null)
            {
                return NotFound();
            }

            return View(lotCommande);
        }

        // POST: Admin/LotCommandes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LotCommandes == null)
            {
                return Problem("Entity set 'AppDbContext.LotCommandes'  is null.");
            }
            var lotCommande = await _context.LotCommandes.FindAsync(id);
            if (lotCommande != null)
            {
                _context.LotCommandes.Remove(lotCommande);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LotCommandeExists(int id)
        {
          return (_context.LotCommandes?.Any(e => e.LotCommandeId == id)).GetValueOrDefault();
        }
    }
}
