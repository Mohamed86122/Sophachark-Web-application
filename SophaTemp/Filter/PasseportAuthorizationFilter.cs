using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SophaTemp.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PasseportAuthorizationFilter : Attribute, IAsyncActionFilter
    {
        private readonly string _passeport;

        public PasseportAuthorizationFilter(string passeport)
        {
            _passeport = passeport;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();

            var userEmail = context.HttpContext.User.Identity.Name;
            var user = await dbContext.Personnes
                                      .Include(u => u.Passeport)
                                      .FirstOrDefaultAsync(u => u.email == userEmail);

            if (user != null && user.Passeport != null && user.Passeport.Nom == _passeport)
            {
                switch (user.Passeport.Nom)
                {
                    case "AdminCommandes":
                        context.Result = new RedirectToActionResult("Index", "Commande", null);
                        return;
                    case "AdminStock":
                        context.Result = new RedirectToActionResult("Index", "Lot", null);
                        return;
                    case "AdminPrincipale":
                        context.Result = new RedirectToActionResult("Index", "Principal", null);
                        return;
                    case "AdminProduit":
                        context.Result = new RedirectToActionResult("Index", "Produits", null);
                        return;
                    default:
                        context.Result = new RedirectToActionResult("Index", "Home", null);
                        return;
                }
                await next();
            }
            else
            {
                // Redirigez vers la page de login si l'utilisateur n'est pas autorisé
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
    }
}
