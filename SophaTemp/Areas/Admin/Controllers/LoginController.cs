using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Viewmodel;
using System.Security.Claims;

namespace SophaTemp.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login(LoginVm model)
        {
            if (ModelState.IsValid)
            {
                // Récupérer l'utilisateur par email et mot de passe
                var user = _context.Personnes
                    .Include(p => p.Passeport.Permissions) // Inclure les permissions du Passeport
                    .FirstOrDefault(u => u.email == model.Email && u.motdepasse == model.MotDePasse);

                if (user != null)
                {
                    // Créer un cookie d'authentification
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.PersonneId.ToString()),
                    new Claim(ClaimTypes.Name, user.nom + " " + user.prenom),
                    new Claim(ClaimTypes.Email, user.email),
                    // Ajouter un claim pour les permissions (ex: CRUD_Medicament)
                    new Claim("Permissions", string.Join(",", user.Passeport.Permissions.Select(p => p.Nom)))
                };

                    var identity = new ClaimsIdentity(claims, "AdminIdentity");
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(principal);

                    // Redirection en fonction des permissions
                    if (user.Passeport.Permissions.Any(p => p.Nom == "AccèsAdminClient"))
                    {
                        return RedirectToAction("Index", "Client", new { area = "Admin" }); // Redirection vers Admin/Client/Index
                    }
                    else if (user.Passeport.Permissions.Any(p => p.Nom == "AccèsAdminProduit"))
                    {
                        // Redirection vers la page d'accueil de l'administrateur produit
                        return RedirectToAction("Index", "Produit", new { area = "Admin" }); // Remplacer "Produit" par le nom de votre contrôleur
                    }
                    // ... (ajouter d'autres redirections en fonction des permissions)
                }
                else
                {
                    ModelState.AddModelError("", "Identifiants incorrects.");
                }
            }

            return View(model);

        }
    }
}
