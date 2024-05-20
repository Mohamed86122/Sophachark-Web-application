using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SophaTemp.Data;


//[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
//public class PasseportAuthorizationFilter : Attribute, IAsyncActionFilter
//{
//    private readonly string[] _passeports;

//    public PasseportAuthorizationFilter(params string[] passeports)
//    {
//        _passeports = passeports;
//    }

//    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//    {
//        var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();
//        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<PasseportAuthorizationFilter>>();

//        logger.LogInformation("Starting authorization filter.");

//        // Vérifiez si l'utilisateur est authentifié
//        if (!context.HttpContext.User.Identity.IsAuthenticated)
//        {
//            logger.LogInformation("User is not authenticated.");
//            context.HttpContext.Session.SetString("ErrorMessage", "Vous n'êtes pas autorisé à accéder à cette page.");
//            context.Result = new RedirectToActionResult("Login", "Login", new { area = "Admin" });
//            return;
//        }

//        var userEmail = context.HttpContext.User.Identity.Name;
//        logger.LogInformation($"Authenticated user email: {userEmail}");

//        var user = await dbContext.Personnes
//                                  .Include(u => u.Passeport)
//                                  .FirstOrDefaultAsync(u => u.email == userEmail);

//        if (user == null)
//        {
//            logger.LogInformation("User not found in database.");
//            context.HttpContext.Session.SetString("ErrorMessage", "Utilisateur non trouvé.");
//            context.Result = new RedirectToActionResult("Login", "Login", new { area = "Admin" });
//            return;
//        }

//        var userPassport = user.Passeport?.Nom;
//        logger.LogInformation($"User passport: {userPassport}");

//        if (userPassport == null || !_passeports.Contains(userPassport))
//        {
//            logger.LogInformation("User does not have the required passport.");
//            context.HttpContext.Session.SetString("ErrorMessage", "Vous n'êtes pas autorisé à accéder à cette page.");
//            context.Result = new RedirectToActionResult("Login", "Login", new { area = "Admin" });
//            return;
//        }

//        logger.LogInformation("User is authorized.");
//        await next();
//    }
//}
//}

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
