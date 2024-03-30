using System.Diagnostics;
using LibraryEMP.Managers;
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

        public IActionResult start()
        {
            return View("Pret");
        }

        [HttpGet]
        public IActionResult pageManager( string page )
        {
            return PartialView(page);
        }

        [HttpGet]
        public bool connectToDatabase( string username , string password )
        {
            return DatabaseManager.connect("EMPLIBRARY" , username , password);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
