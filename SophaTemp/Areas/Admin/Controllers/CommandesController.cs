using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SophaTemp.Data;
using SophaTemp.Filter;
using SophaTemp.Mappers;
using SophaTemp.Models;
using SophaTemp.Viewmodel;

namespace SophaTemp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [PasseportAuthorizationFilter("AdminCommandes", "AdminPrincipale")]
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
        public async Task<IActionResult> Index(CommandeVm vm)
        {
            var commandes = await _context.Commandes
                .Include(c => c.Client)
                .Include(c => c.Medicament)
                .ToListAsync();

            var commandeVms = commandes.Select(c => new CommandeVm
            {
                ClientId = c.ClientId,
                MedicamentId = c.MedicamentId,
                DateCommande = c.DateCommande,
                Status = c.Status,
                LotCommandeId = c.LotCommandeId,
                Data = c.Quantite.ToString(),
                Livraisons = c.Livraisons
            }).ToList();

            ViewBag.CommandeVms = commandeVms;

            return View(commandes);
        }

        // GET: Admin/Commandes/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.clients, "ClientId", "LibellePharmacie");
            ViewData["MedicamentId"] = new SelectList(_context.Medicaments.Select(m => new { m.MedicamentId, m.Nom }), "MedicamentId", "Nom");
            ViewData["LotCommandeId"] = new SelectList(_context.LotCommandes, "LotCommandeId", "LotCommandeId");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommandeVm commandeVm)
        {
            if (ModelState.IsValid)
            {
                var commande = _commandeMapper.CommandeFromVm(commandeVm);

                // Convertir les détails de la commande à partir de la propriété `Data`
                if (!string.IsNullOrEmpty(commandeVm.Data))
                {
                    var details = JsonConvert.DeserializeObject<List<CommandeDetailVm>>(commandeVm.Data);
                    if (details != null)
                    {
                        foreach (var detail in details)
                        {
                            var lot = await _context.Lots.FindAsync(detail.LotId);
                            if (lot != null)
                            {
                                lot.Quantite -= detail.Quantite; // Mettre à jour la quantité du lot
                            }
                        }
                    }
                }

                _context.Add(commande);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["LotCommandeId"] = new SelectList(_context.LotCommandes, "LotCommandeId", "LotCommandeId", commandeVm.LotCommandeId);
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
                return Problem("Entity set 'AppDbContext.Commandes' is null.");
            }
            var commande = await _context.Commandes.FindAsync(id);
            if (commande != null)
            {
                _context.Commandes.Remove(commande);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Button Details Commande dans facture
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var commande = await _context.Commandes
                .Include(c => c.Client) // Inclure les informations du client
                .Include(c => c.Medicament) // Inclure les informations du médicament
                .FirstOrDefaultAsync(m => m.CommandeId == id);

            if (commande == null)
            {
                return NotFound();
            }

            var viewModel = new CommandeDetailsVm
            {
                CommandeId = commande.CommandeId,
                DateCommande = commande.DateCommande,
                Status = commande.Status,
                Quantite = commande.Quantite,
                ClientNom = commande.Client.nom,
                MedicamentNom = commande.Medicament.Nom
            };

            return View(viewModel);
        }
        // Autres actions du contrôleur...

        [HttpGet("GenerateInvoice/{id}")]
        public async Task<IActionResult> GenerateInvoice(int id)
        {
            var commande = await _context.Commandes
                .Include(c => c.Client) // Inclure les informations du client
                .Include(c => c.Medicament) // Inclure les informations du médicament
                .FirstOrDefaultAsync(m => m.CommandeId == id);

            if (commande == null)
            {
                return NotFound();
            }

            using (var ms = new MemoryStream())
            {
                var document = new PdfDocument();
                var page = document.AddPage();
                var gfx = XGraphics.FromPdfPage(page);
                var fontRegular = new XFont("Verdana", 12, XFontStyleEx.Regular);
                var fontBold = new XFont("Verdana", 20, XFontStyleEx.Bold);
                var pen = new XPen(XColors.Black, 1); // Pen for the border

                gfx.DrawString("Facture", fontBold, XBrushes.Black, new XRect(0, 0, page.Width, 50), XStringFormats.Center);

                int yPoint = 80;
                gfx.DrawString($"Pharmacie : {commande.Client.nom}", fontRegular, XBrushes.Black, new XRect(40, yPoint, page.Width, 50), XStringFormats.TopLeft);
                yPoint += 20;
                gfx.DrawString($"Date : {commande.DateCommande.ToString("dd/MM/yyyy")}", fontRegular, XBrushes.Black, new XRect(40, yPoint, page.Width, 50), XStringFormats.TopLeft);
                yPoint += 20;
                gfx.DrawString($"Médicament : {commande.Medicament.Nom}", fontRegular, XBrushes.Black, new XRect(40, yPoint, page.Width, 50), XStringFormats.TopLeft);
                yPoint += 20;
                gfx.DrawString($"Quantité : 350 ", fontRegular, XBrushes.Black, new XRect(40, yPoint, page.Width, 50), XStringFormats.TopLeft);
                yPoint += 20;
                gfx.DrawString($"Status : {commande.Status}", fontRegular, XBrushes.Black, new XRect(40, yPoint, page.Width, 50), XStringFormats.TopLeft);

                yPoint += 40;

                document.Save(ms);
                ms.Position = 0;

                return File(ms.ToArray(), "application/pdf", "Invoice.pdf");
            }
        }
        private bool CommandeExists(int id)
        {
            return (_context.Commandes?.Any(e => e.CommandeId == id)).GetValueOrDefault();
        }
    }
}
