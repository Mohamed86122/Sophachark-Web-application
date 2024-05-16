using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Extensions;
using System.Collections.Generic;
using System.Linq;

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

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var lot = _context.Lots.Find(id);

            var cart = HttpContext.Session.GetObject<List<Lot>>("Cart") ?? new List<Lot>();

            var existingLot = cart.FirstOrDefault(l => l.LotId == id);

            if (existingLot != null)
            {
                existingLot.Quantite++;
            }
            else
            {
                lot.Quantite = 1;
                cart.Add(lot);
            }

            HttpContext.Session.SetObject("Cart", cart);

            int totalItems = cart.Sum(l => l.Quantite);
            return Json(new { totalItems });
        }

        [HttpPost]
        public IActionResult AddToWishlist(int id)
        {
            var lot = _context.Lots.Find(id);

            var wishlist = HttpContext.Session.GetObject<List<Lot>>("Wishlist") ?? new List<Lot>();

            var existingLot = wishlist.FirstOrDefault(l => l.LotId == id);

            if (existingLot == null)
            {
                wishlist.Add(lot);
            }

            HttpContext.Session.SetObject("Wishlist", wishlist);

            int totalItems = wishlist.Count;
            return Json(new { totalItems });
        }

        public IActionResult ViewCart()
        {
            var cart = HttpContext.Session.GetObject<List<Lot>>("Cart");
            return View(cart);
        }

        public IActionResult ViewWishlist()
        {
            var wishlist = HttpContext.Session.GetObject<List<Lot>>("Wishlist");
            return View(wishlist);
        }

        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("ShopView");
        }

        public IActionResult ClearWishlist()
        {
            HttpContext.Session.Remove("Wishlist");
            return RedirectToAction("ShopView");
        }
    }
}
