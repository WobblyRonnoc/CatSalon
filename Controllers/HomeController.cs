using CatSalon.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CatSalon.Models.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CatSalon.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult About()
        {
            return View();
        }        
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Index(bool? logout)
        {
            if(logout != null)
            {
                HttpContext.Session.Clear();
            }
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
            if (HttpContext.Session.GetString("userId") != null)
            {
                using (CatSalonContext context = new CatSalonContext())
                {
                    var owner = await context.Owners
                        .FirstOrDefaultAsync(m => m.Id == Convert.ToInt32(HttpContext.Session.GetString("userId")));
                    return RedirectToAction("Dashboard", "Owners", new { area = "Owners" });
                }
            }
            return RedirectToAction("Login", "Owners", new { area = "Owners" });
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}