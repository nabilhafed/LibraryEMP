using System.Diagnostics;
//using LibraryEMP.Managers;
using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryEMP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
            
        }

        public IActionResult start()
        {
            return View("Pret");
        }
       /// avoir la liste des document réserver 
       public  IActionResult ListDocumentRéserver()
        {
            return View("liste Resrver") ;
        }

        [HttpGet]
        public IActionResult pageManager( string page )
        {
            return PartialView(page);
        }

        [HttpGet]
        public bool connectToDatabase(string username, string password)
        {
            //return DatabaseManager.connect("EMPLIBRARY", "localhost", "1521", username , password);
            return true;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    
}
