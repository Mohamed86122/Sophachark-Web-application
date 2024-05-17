using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SophaTemp.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PasseportAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly string _passeport;

        public PasseportAuthorizationFilter(string passeport)
        {
            _passeport = passeport;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<PasseportAuthorizationFilter>>();
            var userPassport = context.HttpContext.Session.GetString("UserRole");

            // Ajoutez des messages de débogage
            logger.LogInformation($"User passport in session: {userPassport}, required: {_passeport}");

            if (userPassport == null || userPassport != _passeport)
            {
                logger.LogInformation("Accès refusé.");
                context.HttpContext.Session.SetString("ErrorMessage", "Vous n'êtes pas autorisé à accéder à cette page.");
                context.Result = new RedirectToActionResult("Login", "Login", new { area = "Admin" });
            }
            else
            {
                logger.LogInformation("Accès autorisé.");
            }
        }
    }
}
