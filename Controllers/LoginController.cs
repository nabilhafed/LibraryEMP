using LibraryEMP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryEMP.Controllers
{
	public class LoginController : Controller
	{
		public readonly ApplicationDbContext _db;
		public LoginController(ApplicationDbContext db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			if( HttpContext.Session.GetInt32("IsLoggedIn") == 1 )
				return RedirectToAction("index", "Pret");
			else
				return View();
		}

		[HttpPost]
		public ActionResult Login(string username , string password)
		{
			HttpContext.Session.SetInt32("IsLoggedIn", 1);
			return RedirectToAction("index", "Pret");
		}

		public IActionResult Logout()
		{
			HttpContext.Session.SetInt32("IsLoggedIn" , 0);
			return RedirectToAction("index", "Login");
		}
	}
}
