using System;
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
    [PasseportAuthorizationFilter( "AdminPrincipale")]
    public class PasseportsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PasseportMapper _passeportMapper;

        public PasseportsController(AppDbContext context, PasseportMapper passeportMapper)
        {
            _context = context;
            _passeportMapper = passeportMapper;
        }

        // GET: Admin/Passeports
        public async Task<IActionResult> Index()
        {
            var passeports = await _context.Passeports
                .Include(p => p.Permissions)
                .ToListAsync();
            return View(passeports);
        }

        // GET: Admin/Passeports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Passeports == null)
            {
                return NotFound();
            }

            var passeport = await _context.Passeports
                .Include(p => p.Permissions)
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
            ViewBag.Permission = new SelectList(_context.permissions, "PermissionId", "Nom");
            return View();
        }

        // POST: Admin/Passeports/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PasseportVm passeportVm)
        {
            if (ModelState.IsValid)
            {
                Passeport newPasseport = _passeportMapper.PassportAddVmmap(passeportVm);

                _context.Add(newPasseport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Permission = new SelectList(_context.permissions, "PermissionId", "Nom");
            return View(passeportVm);
        }

        // GET: Admin/Passeports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var passeport = await _context.Passeports
                .Include(p => p.Permissions)
                .FirstOrDefaultAsync(p => p.PasseportId == id);
            if (passeport == null)
            {
                return NotFound();
            }

            var viewModel = new PasseportVm
            {
                Nom = passeport.Nom,
                SelectedpasseportIds = passeport.Permissions.Select(p => p.PermissionId).ToList()
            };

            ViewBag.PasseportId = id;
            ViewBag.Permission = new SelectList(_context.permissions, "PermissionId", "Nom", viewModel.SelectedpasseportIds);
            return View(viewModel);
        }

        // POST: Admin/Passeports/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PasseportVm model)
        {
            if (ModelState.IsValid)
            {
                var passeportToUpdate = await _context.Passeports
                    .Include(p => p.Permissions)
                    .FirstOrDefaultAsync(p => p.PasseportId == id);

                if (passeportToUpdate == null)
                {
                    return NotFound();
                }

                passeportToUpdate.Nom = model.Nom;
                var updatedPermissions = _context.permissions
                    .Where(p => model.SelectedpasseportIds.Contains(p.PermissionId))
                    .ToList();

                passeportToUpdate.Permissions = updatedPermissions;

                try
                {
                    _context.Update(passeportToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasseportExists(passeportToUpdate.PasseportId))
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

            ViewBag.PasseportId = id;
            ViewBag.Permission = new SelectList(_context.permissions, "PermissionId", "Nom", model.SelectedpasseportIds);
            return View(model);
        }

        // GET: Admin/Passeports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Passeports == null)
            {
                return NotFound();
            }

            var passeport = await _context.Passeports
                .Include(p => p.Permissions)
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
            var passeport = await _context.Passeports
                .Include(p => p.Permissions)
                .FirstOrDefaultAsync(p => p.PasseportId == id);

            if (passeport == null)
            {
                return NotFound();
            }

            // Supprimez les permissions associées
            if (passeport.Permissions != null)
            {
                _context.permissions.RemoveRange(passeport.Permissions);
            }

            _context.Passeports.Remove(passeport);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool PasseportExists(int id)
        {
            return (_context.Passeports?.Any(e => e.PasseportId == id)).GetValueOrDefault();
        }
    }
}
