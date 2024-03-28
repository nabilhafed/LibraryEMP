using System.Diagnostics;
using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryEMP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Pret()
        {
            return View();
        }

        public IActionResult gestion_des_adherents()
        {
            return View();
        }
        public IActionResult Restitution()
        {
            return View();
        }

        // Action method to return Pages
        public IActionResult pageManager( string page )
        {
            return PartialView(page);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
