using Microsoft.AspNetCore.Authentication;
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
            var errorMessage = HttpContext.Session.GetString("ErrorMessage");
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ModelState.AddModelError("", errorMessage);
                HttpContext.Session.Remove("ErrorMessage");
            }

            _logger.LogInformation("Displaying login page.");
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVm model)
        {
            _logger.LogInformation("Attempting to log in.");

            if (ModelState.IsValid)
            {
                var user = _context.Personnes
                    .Include(p => p.Passeport)
                    .FirstOrDefault(u => u.email.Trim() == model.Email.Trim() && u.motdepasse.Trim() == model.MotDePasse.Trim());

                if (user != null)
                {
                    HttpContext.Session.SetString("UserId", user.PersonneId.ToString());
                    HttpContext.Session.SetString("UserEmail", user.email);
                    HttpContext.Session.SetString("UserRole", user.Passeport?.Nom ?? "User");

                    string redirectController = user.Passeport?.Nom switch
                    {
                        "AdminCommandes" => "Commandes",
                        "AdminPrincipale" => "Acceuil",
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
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Optionally, you can also sign out the user if you are using authentication middleware
            await HttpContext.SignOutAsync();

            // Redirect to the desired URL
            return RedirectToAction("Login", "Login", new { area = "Admin" });
        }
    }
}