﻿using Microsoft.AspNetCore.Mvc;
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
            List<ShowCartItem> showCartItems = new List<ShowCartItem>();
            foreach (Medicament item in Medicaments)
            {
                ShowCartItem cartItem = new ShowCartItem
                {
                    Medicament = item,
                    Prix = lot.First(l => l.MedicamentId == item.MedicamentId).PrixVente,
                    Quantite = lot.First(l => l.MedicamentId == item.MedicamentId).Quantite
                };
                showCartItems.Add(cartItem);
            }
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.showCartItems = showCartItems;
            return View();
        }

        [HttpPost]
        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var session = HttpContext.Session;
            int? clientId = session.GetInt32("ClientId");
            if (clientId == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var cart = session.GetObject<List<CartLineVm>>("Cart") ?? new List<CartLineVm>();
            var existingCartLine = cart.FirstOrDefault(l => l.idMedicament == id);
            if (existingCartLine != null)
            {
                existingCartLine.Quantite += quantity;
            }
            else
            {
                var medicament = _context.Medicaments.FirstOrDefault(m => m.MedicamentId == id);
                var lot = _context.Lots.FirstOrDefault(l => l.IsPublic && l.MedicamentId == id);
                if (medicament != null && lot != null)
                {
                    var Cartline = new CartLineVm
                    {
                        idMedicament = id,
                        Name = medicament.Nom,
                        Image = medicament.Image,
                        Quantite = quantity,
                        PrixdeVente = lot.PrixVente
                    };
                    cart.Add(Cartline);
                }
            }

            session.SetObject("Cart", cart);
            session.SetString("Count", cart.Count.ToString());
            return Json(new { totalItems = cart.Count });
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

            var whishlists = session.GetObject<List<WhishlistLineVm>>("Wishlist") ?? new List<WhishlistLineVm>();
            var existingwishlistLine = whishlists.FirstOrDefault(l => l.idMedicament == id);
            if (existingwishlistLine != null)
            {
                existingwishlistLine.Quantite++;
            }
            else
            {
                var medicament = _context.Medicaments.FirstOrDefault(m => m.MedicamentId == id);
                var lot = _context.Lots.FirstOrDefault(l => l.IsPublic && l.MedicamentId == id);
                if (medicament != null && lot != null)
                {
                    var whishlistline = new WhishlistLineVm
                    {
                        idMedicament = id,
                        Name = medicament.Nom,
                        Image = medicament.Image,
                        Quantite = 1,
                        PrixdeVente = lot.PrixVente
                    };
                    whishlists.Add(whishlistline);
                }
            }

            session.SetObject("Wishlist", whishlists);
            session.SetString("WishlistCount", whishlists.Count.ToString());
            return Json(new { totalItems = whishlists.Count });
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
            var whishlists = session.GetObject<List<WhishlistLineVm>>("Wishlist") ?? new List<WhishlistLineVm>();
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
