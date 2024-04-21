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
            var passeports = await _context.Passeports
                .Include(p => p.Permissions)
                .ToListAsync();
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
            ViewBag.Permission = new SelectList(_context.permissions, "PermissionId", "Nom");
            return View();
        }

        // POST: Admin/Passeports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PasseportVm model)
        {
            if (ModelState.IsValid)
            {
                PasseportMapper passeportMap = new PasseportMapper(_context);
                Passeport passeport = passeportMap.PassportAddVmmap(model);

                foreach (var permissionId in model.SelectedpasseportIds)
                {
                    var existingPermission = await _context.permissions
                        .FindAsync(permissionId); // Assurez-vous que cette permission existe déjà dans la base de données.
                    if (existingPermission != null)
                    {
                        passeport.Permissions.Add(existingPermission); // Ajoutez l'instance existante au lieu d'en créer une nouvelle.
                    }
                }

                _context.Add(passeport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Permission = new SelectList(_context.permissions, "PermissionId", "Nom");
            return View(model);
        }
        // GET: Admin/Passeports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Passeports == null)
            {
                return NotFound();
            }

            var passeport = await _context.Passeports
                .Include(p => p.Permissions)  // Assurez-vous de charger également les permissions
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
            ViewBag.Permission = new SelectList(_context.permissions, "PermissionId", "Nom", viewModel.SelectedpasseportIds);
            return View(viewModel);
        }


          // POST: Admin/Passeports/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, PasseportVm model)  // Utiliser ViewModel au lieu de binder directement le modèle Passeport
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

            passeportToUpdate.Nom = model.Nom;  // Mise à jour des champs simples
            // Mise à jour des permissions
            var updatedPermissions = _context.permissions
                .Where(p => model.SelectedpasseportIds.Contains(p.PermissionId))
                .ToList();

            passeportToUpdate.Permissions = updatedPermissions;  // Mise à jour de la collection des permissions

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

        // Recharger les permissions si la soumission du formulaire échoue
        model.SelectedpasseportIds = _context.permissions
            .Where(p => model.SelectedpasseportIds.Contains(p.PermissionId))
            .Select(p => p.PermissionId)
            .ToList();

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
                .Include(p => p.Permissions)  // Charger les permissions pour afficher les détails avant la suppression
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
            var passeport = await _context.Passeports.FindAsync(id);
            if (passeport == null)
            {
                return NotFound();
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
