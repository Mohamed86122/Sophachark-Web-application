using Microsoft.AspNetCore.Mvc;

namespace SophaTemp.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShopView()
        {
            return View();
        }
    }
}
