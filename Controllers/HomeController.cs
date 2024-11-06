using E_Platform.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace E_Platform.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (User.IsInRole("Administrador"))
            {
                return RedirectToAction("AdminDashboard");
            }
            else if (User.IsInRole("Alumno"))
            {
                return RedirectToAction("StudentDashboard");
            }

            return View();
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

        [Authorize(Roles = "Administrador")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "Alumno")]
        public IActionResult StudentDashboard()
        {
            return View();
        }

    }
}
