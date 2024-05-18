using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Extensions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace SophaTemp.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ShopController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult ShopView()
        {
            ViewBag.Medicaments = _context.Medicaments.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            int? clientId = session.GetInt32("ClientId");
            if (clientId == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var lot = _context.Lots.FirstOrDefault(l => l.MedicamentId == id);
            if (lot == null)
            {
                return NotFound();
            }

            var cart = session.GetObject<List<Lot>>("Cart") ?? new List<Lot>();

            var existingLot = cart.FirstOrDefault(l => l.MedicamentId == id);
            if (existingLot != null)
            {
                existingLot.Quantite++;
            }
            else
            {
                lot.Quantite = 1;
                cart.Add(lot);
            }

            session.SetObject("Cart", cart);
            int totalItems = cart.Sum(l => l.Quantite);
            return Json(new { totalItems });
        }

        [HttpPost]
        public IActionResult AddToWishlist(int id)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            int? clientId = session.GetInt32("ClientId");
            if (clientId == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var lot = _context.Lots.FirstOrDefault(l => l.MedicamentId == id);
            if (lot == null)
            {
                return NotFound();
            }

            var wishlist = session.GetObject<List<Lot>>("Wishlist") ?? new List<Lot>();

            var existingLot = wishlist.FirstOrDefault(l => l.MedicamentId == id);
            if (existingLot == null)
            {
                wishlist.Add(lot);
            }

            session.SetObject("Wishlist", wishlist);
            int totalItems = wishlist.Count;
            return Json(new { totalItems });
        }

        public IActionResult ViewCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var cart = session.GetObject<List<Lot>>("Cart") ?? new List<Lot>();

            var lotIds = cart.Select(l => l.MedicamentId).Distinct().ToList();
            var medicaments = _context.Medicaments
                .Where(m => lotIds.Contains(m.MedicamentId))
                .ToDictionary(m => m.MedicamentId, m => new { m.Nom, m.Image });

            ViewBag.Medicaments = medicaments;

            return View(cart);
        }

        public IActionResult ViewWishlist()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var wishlist = session.GetObject<List<Lot>>("Wishlist") ?? new List<Lot>();

            var lotIds = wishlist.Select(l => l.MedicamentId).Distinct().ToList();
            var medicaments = _context.Medicaments
                .Where(m => lotIds.Contains(m.MedicamentId))
                .ToDictionary(m => m.MedicamentId, m => new { m.Nom, m.Image });

            ViewBag.Medicaments = medicaments;

            return View(wishlist);
        }

        public IActionResult ClearCart()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove("Cart");
            return RedirectToAction("ShopView");
        }

        public IActionResult ClearWishlist()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove("Wishlist");
            return RedirectToAction("ShopView");
        }
    }
}
