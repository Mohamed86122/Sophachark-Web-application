using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Viewmodel;
using System.Linq;

namespace SophaTemp.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly AppDbContext _context;

        public ProductDetailsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult ProdDetails(int id)
        {
            var medicament = _context.Medicaments.FirstOrDefault(m => m.MedicamentId == id);
            if (medicament == null)
            {
                return NotFound();
            }

            var lot = _context.Lots.FirstOrDefault(l => l.MedicamentId == id);
            if (lot == null)
            {
                return NotFound();
            }

            var relatedProducts = _context.Medicaments
                .Where(m => m.MedicamentId != id)
                .Take(4)
                .ToList();

            var medicamentDetailVm = new MedicamentDetailVm
            {
                MedicamentId = medicament.MedicamentId,
                Nom = medicament.Nom,
                Description = medicament.Description,
                Image = medicament.Image,
                PrixVente = lot.PrixVente,
                RelatedProducts = relatedProducts.Select(rp => new MedicamentDetailVm
                {
                    Nom = rp.Nom,
                    Description = rp.Description,
                    Image = rp.Image,
                    PrixVente = _context.Lots.FirstOrDefault(l => l.MedicamentId == rp.MedicamentId)?.PrixVente ?? 0
                }).ToList()
            };

            return View(medicamentDetailVm);
        }
    }
}
