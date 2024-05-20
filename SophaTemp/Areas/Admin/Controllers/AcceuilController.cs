using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SophaTemp.Data;
using SophaTemp.Filter;
using SophaTemp.Viewmodel;
using System.Security.Claims;

namespace SophaTemp.Areas.Admin.Controllers
{   
    [Area("Admin")]
    [PasseportAuthorizationFilter("AdminPrincipale")]

    public class AcceuilController : Controller
    {
        private readonly AppDbContext _context;

        public AcceuilController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {


            return View();
        }
		
		
    }
}
