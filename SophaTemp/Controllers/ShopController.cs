using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using SophaTemp.Viewmodel;

namespace SophaTemp.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;

        public ShopController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShopView()
        {
            ViewBag.Medicaments = _context.Medicaments.ToList();

            return View();
        }
    }
}
