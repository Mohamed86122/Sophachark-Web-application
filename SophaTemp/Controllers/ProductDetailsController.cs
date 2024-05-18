using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using SophaTemp.Models;
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

            ViewBag.RelatedProducts = relatedProducts;
            ViewBag.PrixVente = lot.PrixVente;

            return View(medicament);
        }
    }
}
