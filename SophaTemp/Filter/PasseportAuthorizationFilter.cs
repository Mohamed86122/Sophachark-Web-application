using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using Microsoft.EntityFrameworkCore;

namespace SophaTemp.Filter
{
    public class PasseportAuthorizationFilter : ActionFilterAttribute
    {
        private readonly AppDbContext _context;
        private string passeport;

        public PasseportAuthorizationFilter(string passport)
        {
            this.passeport = passeport;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var userEmail = context.HttpContext.User.Identity.Name;

            var user = _context.Personnes
                               .Include(u => u.Passeport)
                               .FirstOrDefault(u => u.email == userEmail);

            if (user != null && user.Passeport != null)
            {
                switch (user.Passeport.Nom)
                {
                    case "AdminCommande":
                        context.Result = new RedirectToActionResult("Index", "Commandes", null);
                        break;
                    case "AdminClient":
                        context.Result = new RedirectToActionResult("Index", "Clients", null);
                        break;
                    case "AdminPrincipale":
                        context.Result = new RedirectToActionResult("Index", "Principal", null);
                        break;
                    case "AdminProduit":
                        context.Result = new RedirectToActionResult("Index", "Produits", null);
                        break;
                    default:
                        context.Result = new RedirectToActionResult("Index", "Home", null);
                        break;
                }
            }
            else
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
