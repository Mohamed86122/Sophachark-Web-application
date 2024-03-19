using Microsoft.AspNetCore.Mvc;

namespace SophaTemp.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Contact()
        {
            return View();
        }
    }
}
