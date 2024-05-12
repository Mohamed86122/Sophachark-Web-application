using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
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

                var user = _context.Personnes.FirstOrDefault(u => u.email == person.email && u.motdepasse == person.motdepasse);

                if (user != null)
                {
                    // Remplissage de l'objet USer Identity
                    string redirectController = user.Passeport?.Nom switch
                    {
                        "AdminCommande" => "Commandes",
                        "AdminPrincipale" => "Principale",
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