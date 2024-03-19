using Microsoft.AspNetCore.Mvc;

namespace SophaTemp.Controllers
{
    public class ProductDetailsController : Controller
    {
        public IActionResult ProdDetails()
        {
            return View();
        }
    }
}
