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
    [PasseportAuthorizationFilter("AdminClients", "AdminPrincipale")]
    public class ClientsController : Controller
    {
        private readonly AppDbContext _context;

        public ClientsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Clients
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.clients.Include(c => c.Whishlist);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Admin/Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.clients == null)
            {
                return NotFound();
            }

            var client = await _context.clients
                .Include(c => c.Whishlist)
                .FirstOrDefaultAsync(m => m.PersonneId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Admin/Clients/Create
        public IActionResult Create()
        {
            ViewData["WhishlistId"] = new SelectList(_context.Whishlists, "WhishlistId", "WhishlistId");
            return View();
        }

        // POST: Admin/Clients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientVm clientVm)
        {
            if (ModelState.IsValid)
            {
                ClientMapper clientMapper = new ClientMapper();
                Client newclient = clientMapper.ClientVmClient(clientVm);

                _context.clients.Add(newclient);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["WhishlistId"] = new SelectList(_context.Whishlists, "WhishlistId", "WhishlistId", clientVm.WhishlistId);
            return View(clientVm);
        }

        // GET: Admin/Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.clients == null)
            {
                return NotFound();
            }

            var client = await _context.clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            var clientVm = new ClientVm
            {
                LibellePharmacie = client.LibellePharmacie,
                Ville = client.Ville,
                Telephone = client.Telephone,
                X = client.X,
                Y = client.Y,
                Adresse = client.Adresse,
                EnGarde = client.EnGarde,
                WhishlistId = client.WhishlistId,
                nom = client.nom,
                prenom = client.prenom,
                email = client.email,
                motdepasse = client.motdepasse,
            };

            ViewData["WhishlistId"] = new SelectList(_context.Whishlists, "WhishlistId", "WhishlistId", clientVm.WhishlistId);
            return View(clientVm);
        }

        // POST: Admin/Clients/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClientVm clientVm)
        {
            if (ModelState.IsValid)
            {
                var client = new Client
                {
                    PersonneId = id,
                    LibellePharmacie = clientVm.LibellePharmacie,
                    Ville = clientVm.Ville,
                    Telephone = clientVm.Telephone,
                    X = clientVm.X,
                    Y = clientVm.Y,
                    Adresse = clientVm.Adresse,
                    EnGarde = clientVm.EnGarde,
                    WhishlistId = clientVm.WhishlistId,
                    nom = clientVm.nom,
                    prenom = clientVm.prenom,
                    email = clientVm.email,
                    motdepasse = clientVm.motdepasse,
                };

                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.PersonneId))
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

            ViewData["WhishlistId"] = new SelectList(_context.Whishlists, "WhishlistId", "WhishlistId", clientVm.WhishlistId);
            return View(clientVm);
        }

        // GET: Admin/Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.clients == null)
            {
                return NotFound();
            }

            var client = await _context.clients
                .Include(c => c.Passeport)
                .Include(c => c.Whishlist)
                .FirstOrDefaultAsync(m => m.PersonneId == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Admin/Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.clients == null)
            {
                return Problem("Entity set 'AppDbContext.Clients'  is null.");
            }
            var client = await _context.clients.FindAsync(id);
            if (client != null)
            {
                _context.clients.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return (_context.clients?.Any(e => e.PersonneId == id)).GetValueOrDefault();
        }
    }
}
