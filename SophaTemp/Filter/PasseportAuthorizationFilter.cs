using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SophaTemp.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace SophaTemp.Filter
{
    public class PasseportAuthorizationFilter : IAsyncActionFilter
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
                    case "AdminCommande":
                        context.Result = new RedirectToActionResult("Index", "Commandes", null);
                        return;
                    case "AdminClient":
                        context.Result = new RedirectToActionResult("Index", "Clients", null);
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
            }

            // If no user is found or role does not match, redirect to login
            context.Result = new RedirectToActionResult("Login", "Account", null);
            // Skip the next action execution
        }
    }
}
