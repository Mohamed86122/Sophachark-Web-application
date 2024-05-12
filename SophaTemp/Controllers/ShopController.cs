using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using SophaTemp.Models;
using SophaTemp.Viewmodel;
using SophaTemp.Extensions;

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
        // Action pour ajouter un produit au panier
        public IActionResult AddToCart(int id)
        {
            // Récupérer le produit à partir de son ID
            var medicament = _context.Medicaments.Find(id);

            // Vérifier si le panier existe dans la session
            if (HttpContext.Session.GetObject<List<Medicament>>("Cart") == null)
            {
                // Créer un nouveau panier s'il n'existe pas
                List<Medicament> cart = new List<Medicament>();
                cart.Add(medicament);
                HttpContext.Session.SetObject("Cart", cart);
            }
            else
            {
                // Ajouter le produit au panier existant
                var cart = HttpContext.Session.GetObject<List<Medicament>>("Cart");
                cart.Add(medicament);
                HttpContext.Session.SetObject("Cart", cart);
            }

            // Rediriger vers la page du produit après l'ajout au panier
            return RedirectToAction("ShopView");
        }

        public IActionResult ViewCart()
        {
            // Récupérer le panier depuis la session
            var cart = HttpContext.Session.GetObject<List<Medicament>>("Cart");

            // Envoyer le panier à la vue partielle
            return PartialView("_CartPartial", cart);
        }

        // Action pour vider le panier
        public IActionResult ClearCart()
        {
            // Supprimer le panier de la session
            HttpContext.Session.Remove("Cart");

            // Rediriger vers la page d'accueil ou une autre page appropriée
            return RedirectToAction("Index");
        }
    }
}
