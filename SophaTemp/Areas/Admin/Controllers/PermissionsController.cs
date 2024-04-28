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
    public class PermissionsController : Controller
    {
        private readonly AppDbContext _context;

        public PermissionsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Permissions
        public async Task<IActionResult> Index()
        {
              return _context.permissions != null ? 
                          View(await _context.permissions.ToListAsync()) :
                          Problem("Entity set 'AppDbContext.permissions'  is null.");
        }

        // GET: Admin/Permissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.permissions == null)
            {
                return NotFound();
            }

            var permission = await _context.permissions
                .FirstOrDefaultAsync(m => m.PermissionId == id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // GET: Admin/Permissions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Permissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PermissionId,Nom,Description")] Permission permission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(permission);
        }

        // GET: Admin/Permissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.permissions == null)
            {
                return NotFound();
            }

            var permission = await _context.permissions.FindAsync(id);
            if (permission == null)
            {
                return NotFound();
            }
            return View(permission);
        }

        // POST: Admin/Permissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PermissionId,Nom,Description")] Permission permission)
        {
            if (id != permission.PermissionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionExists(permission.PermissionId))
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
            return View(permission);
        }

        // GET: Admin/Permissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.permissions == null)
            {
                return NotFound();
            }

            var permission = await _context.permissions
                .FirstOrDefaultAsync(m => m.PermissionId == id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // POST: Admin/Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.permissions == null)
            {
                return Problem("Entity set 'AppDbContext.permissions'  is null.");
            }
            var permission = await _context.permissions.FindAsync(id);
            if (permission != null)
            {
                _context.permissions.Remove(permission);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PermissionExists(int id)
        {
          return (_context.permissions?.Any(e => e.PermissionId == id)).GetValueOrDefault();
        }
    }
}
