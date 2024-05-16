using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Viewmodel;
using System.Security.Claims;

namespace SophaTemp.Areas.Admin.Controllers
{   
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {


            return View();
        }
		
		
    }
}
