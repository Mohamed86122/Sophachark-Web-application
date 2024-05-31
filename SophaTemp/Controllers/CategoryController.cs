using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SophaTemp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public async Task<IActionResult> MedicationsByCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var medications = _context.Medicaments
                .Where(m => m.MedicamentCategoryMedicaments.Any(mc => mc.CategoryMedicamentId == id))
                .ToList();

            var categories = _context.Categories.ToList();

            var viewModel = new CategoryMedicationsViewModel
            {
                Category = category,
                Medications = medications,
                Categories = categories
            };

            return View(viewModel);
        }
    }
}
