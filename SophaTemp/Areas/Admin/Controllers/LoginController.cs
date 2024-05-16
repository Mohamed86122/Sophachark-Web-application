using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Filter;
using SophaTemp.Mappers;
using SophaTemp.Models;
using SophaTemp.Viewmodel;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Login(PersonVm model)
        {
            if (ModelState.IsValid)
            {
                Personne person = _personMapper.AddMapVM(model);

                var user = _context.Personnes
                    .Include(p => p.Passeport)
                    .FirstOrDefault(u => u.email.Trim() == person.email.Trim() && u.motdepasse.Trim() == person.motdepasse.Trim());

                if (user != null)
                {
                    //Usage Session
                    HttpContext.Session.SetString("UserId", user.PersonneId.ToString());
                    HttpContext.Session.SetString("UserEmail", user.email);
                    HttpContext.Session.SetString("UserRole", user.Passeport?.Nom ?? "User");


                    // Remplissage de l'objet USer Identity
                    string redirectController = user.Passeport?.Nom switch
                    {
                        "AdminCommandes" => "",
                        "AdminPrincipale" => "Home",
                        "AdminProduits" => "Medicaments",
                        "AdminStock" => "Lots",
                        _ => "Home"
                    };

                    return RedirectToAction("Index", redirectController);
                }

                ModelState.AddModelError("", "Invalid login attempt.");
            }

            return View(model);

        }

    }
}