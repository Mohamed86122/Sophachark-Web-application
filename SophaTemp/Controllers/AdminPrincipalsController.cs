using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Models;

namespace SophaTemp.Controllers
{
    public class AdminPrincipalsController : Controller
    {
        private readonly AppDbContext _context;

        public AdminPrincipalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: AdminPrincipals
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.AdminPrincipals.Include(a => a.Passeport);
            return View(await appDbContext.ToListAsync());
        }

        // GET: AdminPrincipals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AdminPrincipals == null)
            {
                return NotFound();
            }

            var adminPrincipal = await _context.AdminPrincipals
                .Include(a => a.Passeport)
                .FirstOrDefaultAsync(m => m.PersonneId == id);
            if (adminPrincipal == null)
            {
                return NotFound();
            }

            return View(adminPrincipal);
        }

        // GET: AdminPrincipals/Create
        public IActionResult Create()
        {
            ViewData["PasseportId"] = new SelectList(_context.Passeports, "PasseportId", "PasseportId");
            return View();
        }

        // POST: AdminPrincipals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonneId,nom,prenom,email,motdepasse,PasseportId")] AdminPrincipal adminPrincipal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminPrincipal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PasseportId"] = new SelectList(_context.Passeports, "PasseportId", "PasseportId", adminPrincipal.PasseportId);
            return View(adminPrincipal);
        }

        // GET: AdminPrincipals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AdminPrincipals == null)
            {
                return NotFound();
            }

            var adminPrincipal = await _context.AdminPrincipals.FindAsync(id);
            if (adminPrincipal == null)
            {
                return NotFound();
            }
            ViewData["PasseportId"] = new SelectList(_context.Passeports, "PasseportId", "PasseportId", adminPrincipal.PasseportId);
            return View(adminPrincipal);
        }

        // POST: AdminPrincipals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PersonneId,nom,prenom,email,motdepasse,PasseportId")] AdminPrincipal adminPrincipal)
        {
            if (id != adminPrincipal.PersonneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminPrincipal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminPrincipalExists(adminPrincipal.PersonneId))
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
            ViewData["PasseportId"] = new SelectList(_context.Passeports, "PasseportId", "PasseportId", adminPrincipal.PasseportId);
            return View(adminPrincipal);
        }

        // GET: AdminPrincipals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AdminPrincipals == null)
            {
                return NotFound();
            }

            var adminPrincipal = await _context.AdminPrincipals
                .Include(a => a.Passeport)
                .FirstOrDefaultAsync(m => m.PersonneId == id);
            if (adminPrincipal == null)
            {
                return NotFound();
            }

            return View(adminPrincipal);
        }

        // POST: AdminPrincipals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AdminPrincipals == null)
            {
                return Problem("Entity set 'AppDbContext.AdminPrincipals'  is null.");
            }
            var adminPrincipal = await _context.AdminPrincipals.FindAsync(id);
            if (adminPrincipal != null)
            {
                _context.AdminPrincipals.Remove(adminPrincipal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminPrincipalExists(int id)
        {
          return (_context.AdminPrincipals?.Any(e => e.PersonneId == id)).GetValueOrDefault();
        }
    }
}
