using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public LoginController(AppDbContext context, PersonMapper mapper)
        {
            _context = context;
            _personMapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(PersonVm model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Personnes
                    .Include(p => p.Passeport)
                    .FirstOrDefault(u => u.email.Trim() == model.Email.Trim() && u.motdepasse.Trim() == model.MotDePasse.Trim());

                if (user != null)
                {
                    // Usage Session
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
