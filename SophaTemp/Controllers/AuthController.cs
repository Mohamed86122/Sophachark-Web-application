using Microsoft.AspNetCore.Mvc;

namespace SophaTemp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
