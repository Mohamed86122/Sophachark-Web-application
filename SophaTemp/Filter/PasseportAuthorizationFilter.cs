using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SophaTemp.Data;



namespace SophaTemp.Filter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PasseportAuthorizationFilter : Attribute, IAuthorizationFilter
    {
        private readonly string[] _passeports;

        public PasseportAuthorizationFilter(params string[] passeports)
        {
            _passeports = passeports;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<PasseportAuthorizationFilter>>();
            var userPassport = context.HttpContext.Session.GetString("UserRole");

            logger.LogInformation($"User passport in session: {userPassport}, required: {string.Join(", ", _passeports)}");

            if (userPassport == null || !_passeports.Contains(userPassport))
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
