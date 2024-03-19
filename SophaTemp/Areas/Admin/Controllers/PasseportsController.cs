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
    public class PasseportsController : Controller
    {
        private readonly AppDbContext _context;

        public PasseportsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Passeports
        public async Task<IActionResult> Index()
        {
              return _context.Passeports != null ? 
                          View(await _context.Passeports.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.Passeports'  is null.");
        }

        // GET: Admin/Passeports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Passeports == null)
            {
                return NotFound();
            }

            var passeport = await _context.Passeports
                .FirstOrDefaultAsync(m => m.PasseportId == id);
            if (passeport == null)
            {
                return NotFound();
            }

            return View(passeport);
        }

        // GET: Admin/Passeports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Passeports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PasseportId,Nom,Prenom,PermissionsJson")] Passeport passeport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(passeport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(passeport);
        }

        // GET: Admin/Passeports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Passeports == null)
            {
                return NotFound();
            }

            var passeport = await _context.Passeports.FindAsync(id);
            if (passeport == null)
            {
                return NotFound();
            }
            return View(passeport);
        }

        // POST: Admin/Passeports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PasseportId,Nom,Prenom,PermissionsJson")] Passeport passeport)
        {
            if (id != passeport.PasseportId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passeport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasseportExists(passeport.PasseportId))
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
            return View(passeport);
        }

        // GET: Admin/Passeports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Passeports == null)
            {
                return NotFound();
            }

            var passeport = await _context.Passeports
                .FirstOrDefaultAsync(m => m.PasseportId == id);
            if (passeport == null)
            {
                return NotFound();
            }

            return View(passeport);
        }

        // POST: Admin/Passeports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Passeports == null)
            {
                return Problem("Entity set 'AppDbContext.Passeports'  is null.");
            }
            var passeport = await _context.Passeports.FindAsync(id);
            if (passeport != null)
            {
                _context.Passeports.Remove(passeport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PasseportExists(int id)
        {
          return (_context.Passeports?.Any(e => e.PasseportId == id)).GetValueOrDefault();
        }
    }
}
