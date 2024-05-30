using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SophaTemp.Data;
using SophaTemp.Viewmodel;

namespace SophaTemp.Controllers
{
    public class SearchController : Controller
    {
        private readonly AppDbContext _context;

        public SearchController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> SearchMedicament(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                return Json(new { success = false, message = "Le terme de recherche est vide" });
            }

            var results = await _context.Lots
                .Include(l => l.Medicament)
                .Where(l => l.Medicament.Nom.Contains(search) && l.IsPublic)
                .Select(l => new SearchVm
                {
                    MedicamentId = l.Medicament.MedicamentId, // Inclure l'ID du médicament
                    Nom = l.Medicament.Nom,
                    Description = l.Medicament.Description,
                    Image = l.Medicament.Image,
                    PrixVente = l.PrixVente,
                    Quantite = l.Quantite
                })
                .ToListAsync();

            return Json(new { success = true, data = results });
        }
        [HttpGet]
        public async Task<IActionResult> GetMedicamentSuggestions(string term)
        {
            var suggestions = await _context.Medicaments
                .Where(m => m.Nom.Contains(term))
                .Select(m => new
                {
                    label = m.Nom,
                    value = m.MedicamentId
                })
                .ToListAsync();

            return Json(suggestions);
        }

    }
}
