using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SophaTemp.Data;
using SophaTemp.Mappers;
using SophaTemp.Viewmodel;
using System.Linq;

namespace SophaTemp.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;
        private readonly PersonMapper _personMapper;
        private readonly ILogger<LoginController> _logger;


        public LoginController(AppDbContext context, PersonMapper mapper, ILogger<LoginController> logger)
        {
            _context = context;
            _personMapper = mapper;
            _logger = logger;

        }

        [HttpGet]
        public IActionResult Login()
        {
            // Récupérer le message d'erreur de la session
            var errorMessage = HttpContext.Session.GetString("ErrorMessage");
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                // Effacer le message d'erreur après l'avoir affiché
                HttpContext.Session.Remove("ErrorMessage");
            }

            _logger.LogInformation("Displaying login page.");
            return View();
        }

        [HttpPost]
        public IActionResult Login(PersonVm model)
        {
            _logger.LogInformation("Attempting to log in.");

            if (ModelState.IsValid)
            {
                var user = _context.Personnes
                    .Include(p => p.Passeport)
                    .FirstOrDefault(u => u.email.Trim() == model.Email.Trim() && u.motdepasse.Trim() == model.MotDePasse.Trim());

                if (user != null)
                {
                    // Stocker les informations de l'utilisateur dans la session
                    HttpContext.Session.SetString("UserId", user.PersonneId.ToString());
                    HttpContext.Session.SetString("UserEmail", user.email);
                    HttpContext.Session.SetString("UserRole", user.Passeport?.Nom ?? "User");

                    // Redirection
                    string redirectController = user.Passeport?.Nom switch
                    {
                        "AdminCommandes" => "Commandes",
                        "AdminPrincipal" => "Home",
                        "AdminProduits" => "Medicaments",
                        "AdminStock" => "Lots",
                        "AdminClients" => "Clients",
                        _ => "Home"
                    };

                    return RedirectToAction("Index", redirectController);
                }

                ModelState.AddModelError("", "Tentative de connexion invalide.");
            }

            return View(model);
        }
    }
}
