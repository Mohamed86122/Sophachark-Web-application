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
        public IActionResult Index()
        {
            return View();
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
                    MedicamentId = l.Medicament.MedicamentId,
                    Nom = l.Medicament.Nom,
                    Description = l.Medicament.Description,
                    Image = l.Medicament.Image,
                    PrixVente = l.PrixVente,
                    Quantite = l.Quantite
                })
                .ToListAsync();

            return Json(new { success = true, data = results });
        }
        [HttpPost]
        public IActionResult PartialSearchResults(string searchResults)
        {
            var results = JsonConvert.DeserializeObject<List<SearchVm>>(searchResults);
            return PartialView("_SearchResults", results);
        }
    }
}
