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
    public class PersonnesController : Controller
    {
        private readonly AppDbContext _context;

        public PersonnesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Personnes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Personnes.Include(p => p.Passeport);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Personnes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Personnes == null)
            {
                return NotFound();
            }

            var personne = await _context.Personnes
                .Include(p => p.Passeport)
                .FirstOrDefaultAsync(m => m.PersonneId == id);
            if (personne == null)
            {
                return NotFound();
            }

            return View(personne);
        }

        // GET: Admin/Personnes/Create
        public IActionResult Create()
        {
            ViewData["PasseportId"] = new SelectList(_context.Passeports, "PasseportId", "PasseportId");
            return View();
        }

        // POST: Admin/Personnes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonneId,nom,prenom,email,motdepasse,PasseportId")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PasseportId"] = new SelectList(_context.Passeports, "PasseportId", "PasseportId", personne.PasseportId);
            return View(personne);
        }

        // GET: Admin/Personnes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Personnes == null)
            {
                return NotFound();
            }

            var personne = await _context.Personnes.FindAsync(id);
            if (personne == null)
            {
                return NotFound();
            }
            ViewData["PasseportId"] = new SelectList(_context.Passeports, "PasseportId", "PasseportId", personne.PasseportId);
            return View(personne);
        }

        // POST: Admin/Personnes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonneId,nom,prenom,email,motdepasse,PasseportId")] Personne personne)
        {
            if (id != personne.PersonneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personne);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonneExists(personne.PersonneId))
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
            ViewData["PasseportId"] = new SelectList(_context.Passeports, "PasseportId", "PasseportId", personne.PasseportId);
            return View(personne);
        }

        // GET: Admin/Personnes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Personnes == null)
            {
                return NotFound();
            }

            var personne = await _context.Personnes
                .Include(p => p.Passeport)
                .FirstOrDefaultAsync(m => m.PersonneId == id);
            if (personne == null)
            {
                return NotFound();
            }

            return View(personne);
        }

        // POST: Admin/Personnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Personnes == null)
            {
                return Problem("Entity set 'AppDbContext.Personnes'  is null.");
            }
            var personne = await _context.Personnes.FindAsync(id);
            if (personne != null)
            {
                _context.Personnes.Remove(personne);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonneExists(int id)
        {
          return (_context.Personnes?.Any(e => e.PersonneId == id)).GetValueOrDefault();
        }
    }
}
