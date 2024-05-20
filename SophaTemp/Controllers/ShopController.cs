using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Extensions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using SophaTemp.Viewmodel;

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
            List<Lot> lot = _context.Lots.Where(l => l.IsPublic).ToList();
            var Medicaments = _context.Medicaments.Where(m => lot.Select(l => l.MedicamentId).Contains(m.MedicamentId));
            List<ShowCartItem > showCartItems = new List<ShowCartItem>();
            foreach (Medicament item in Medicaments) {
                ShowCartItem cartItem = new ShowCartItem();
                cartItem.Medicament = item;
                cartItem.Prix = lot.Where(l => l.MedicamentId == item.MedicamentId).First().PrixVente; 
                cartItem.Quantite = lot.Where(l => l.MedicamentId == item.MedicamentId).First().Quantite;
                showCartItems.Add(cartItem);
            }
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.showCartItems = showCartItems;
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int id)
        {
            var session = HttpContext.Session;
            int? clientId = session.GetInt32("ClientId");
            if (clientId == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var Cartline = new CartLineVm();
            

            var cart = session.GetObject<List<CartLineVm>>("Cart") ?? new List<CartLineVm>();

            var existingCartLine = cart.FirstOrDefault(l => l.idMedicament == id);
            if (existingCartLine != null)
            {
                existingCartLine.Quantite++;
            }
            else
            {
                Cartline.idMedicament = id;
                Cartline.Name = _context.Medicaments.Where(medicament => medicament.MedicamentId == id).First().Nom;
                Cartline.Image = _context.Medicaments.Where(medicament => medicament.MedicamentId == id).First().Image;
                Cartline.Quantite = 1;
                Cartline.PrixdeVente = _context.Lots.Where(l => l.IsPublic == true && l.MedicamentId == id).First().PrixVente;
                cart.Add(Cartline);
            }

            session.SetObject("Cart", cart);
            session.SetString("Count", cart.Count.ToString());
            int totalItems = cart.Count();
            return Json(new { totalItems });
        }

        [HttpPost]
        public IActionResult AddToWishlist(int id)
        {
            var session = HttpContext.Session;
            int? clientId = session.GetInt32("ClientId");
            if (clientId == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var whishlistline = new WhishlistLineVm();


            var whishlists = session.GetObject<List<WhishlistLineVm>>("Whishlist") ?? new List<WhishlistLineVm>();

            var existingwishlistLine = whishlists.FirstOrDefault(l => l.idMedicament == id);

            if (existingwishlistLine != null)
            {
                existingwishlistLine.Quantite++;
            }
            else
            {
                whishlistline.idMedicament = id;
                whishlistline.Name = _context.Medicaments.Where(medicament => medicament.MedicamentId == id).First().Nom;
                whishlistline.Image = _context.Medicaments.Where(medicament => medicament.MedicamentId == id).First().Image;
                whishlistline.PrixdeVente = _context.Lots.Where(l => l.IsPublic == true && l.MedicamentId == id).First().PrixVente;
                whishlists.Add(whishlistline);
            }

            session.SetObject("Wishlist", whishlists);      
            session.SetString("Count", whishlists.Count.ToString());
            int totalItems = whishlists.Count;
            return Json(new { totalItems });

        }

        public IActionResult ViewCart()
        {
            var session = HttpContext.Session;
            var cart = session.GetObject<List<CartLineVm>>("Cart") ?? new List<CartLineVm>();



            return View(cart);
        }

        public IActionResult ViewWishlist()
        {
            var session = HttpContext.Session;
            var whishlists = session.GetObject<List<WhishlistLineVm>>("Whishlist") ?? new List<WhishlistLineVm>();

            return View(whishlists);
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
