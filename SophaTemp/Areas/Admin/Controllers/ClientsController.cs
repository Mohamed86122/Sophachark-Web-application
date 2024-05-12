using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SophaTemp.Data;
using SophaTemp.Mappers;
using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Areas.Admin.Controllers
{
    [Area("Admin")]
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
            var appDbContext = _context.clients.Include(c => c.Passeport).Include(c => c.Whishlist);
            return View(await appDbContext.ToListAsync());
        }
        //DatatTable 
        [HttpGet]



        // GET: Admin/Clients/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: Admin/Clients/Create
        public IActionResult Create()
        {
            ViewData["WhishlistId"] = new SelectList(_context.Whishlists, "WhishlistId", "WhishlistId");
            return View();
        }
        
        private int GetNewPersonId()
        {
            
            return 0; // Laisser SQL Server générer automatiquement la valeur de l'identifiant

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

                // Assignez l'Id de la personne
                newclient.PersonneId = GetNewPersonId();

                _context.clients.Add(newclient);
                await _context.SaveChangesAsync();
                
                // Rediriger vers l'index des clients après avoir ajouté avec succès un nouveau client
                return RedirectToAction(nameof(Index));
            }
            ViewData["WhishlistId"] = new SelectList(_context.Whishlists, "WhishlistId", "Whishlist");
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
/*            ViewData["PasseportId"] = new SelectList(_context.Passeports, "PasseportId", "Nom", client.PasseportId);
*/            ViewData["WhishlistId"] = new SelectList(_context.Whishlists, "WhishlistId", "WhishlistId", client.WhishlistId);
            return View(client);
        }

        // POST: Admin/Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibellePharmacie,Ville,Telephone,X,Y,Adresse,EnGarde,WhishlistId,PersonneId,nom,prenom,email,motdepasse")] Client client)
        {
            if (id != client.PersonneId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            ViewData["PasseportId"] = new SelectList(_context.Passeports, "PasseportId", "Nom", client.PasseportId);
            ViewData["WhishlistId"] = new SelectList(_context.Whishlists, "WhishlistId", "WhishlistId", client.WhishlistId);
            return View(client);
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
