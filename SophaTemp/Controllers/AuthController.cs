using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using SophaTemp.Viewmodel;
using SophaTemp.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace SophaTemp.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(AppDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVm model)
        {
            if (ModelState.IsValid)
            {
                var client = _context.clients.SingleOrDefault(c => c.email == model.Email && c.motdepasse == model.MotDePasse);
                if (client != null)
                {
                    var session = _httpContextAccessor.HttpContext.Session;
                    session.SetInt32("ClientId", client.ClientId);
                    session.SetString("ClientName", $"{client.nom} {client.prenom}");
                    return RedirectToAction("ShopView", "Shop");
                }
                ModelState.AddModelError(string.Empty, "Email ou mot de passe incorrect");
            }
            return View("Index", model);
        }

        public IActionResult Logout()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.Remove("ClientId");
            session.Remove("ClientName");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult UpdateProfile()
        {
            // Récupérer les informations du client pour afficher le formulaire de mise à jour du profil
            int? clientId = _httpContextAccessor.HttpContext.Session.GetInt32("ClientId");
            if (clientId == null)
            {
                return RedirectToAction("Index", "Auth");
            }

            var client = _context.clients.Find(clientId);
            if (client == null)
            {
                return NotFound();
            }

            var model = new ClientVm
            {
                ClientId = client.ClientId,
                LibellePharmacie = client.LibellePharmacie,
                Ville = client.Ville,
                Telephone = client.Telephone,
                X = client.X,
                Y = client.Y,
                Adresse = client.Adresse,
                EnGarde = client.EnGarde,
                nom = client.nom,
                prenom = client.prenom,
                email = client.email
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateProfile(ClientVm model)
        {
            if (ModelState.IsValid)
            {
                var client = _context.clients.Find(model.ClientId);
                if (client == null)
                {
                    return NotFound();
                }

                client.LibellePharmacie = model.LibellePharmacie;
                client.Ville = model.Ville;
                client.Telephone = model.Telephone;
                client.X = model.X;
                client.Y = model.Y;
                client.Adresse = model.Adresse;
                client.EnGarde = model.EnGarde;
                client.nom = model.nom;
                client.prenom = model.prenom;
                client.email = model.email;

                _context.SaveChanges();

                // Mettre à jour le nom du client dans la session
                var session = _httpContextAccessor.HttpContext.Session;
                session.SetString("ClientName", $"{client.nom} {client.prenom}");

                return RedirectToAction("ProfileUpdated");
            }

            return View(model);
        }

        public IActionResult ProfileUpdated()
        {
            return View();
        }
    }
}
