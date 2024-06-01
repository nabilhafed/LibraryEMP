using LibraryEMP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text;

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
			UserConnection? userConnection = null;
			if(HttpContext.Session.GetString("SessionID") != null)
				userConnection = _db.UserConnections.FirstOrDefault(x => x.SessionID == HttpContext.Session.GetString("SessionID"));
			if (userConnection == null)
				userConnection = _db.UserConnections.FirstOrDefault(x => x.SessionID == Request.Cookies["SessionID"]);

			if (userConnection != null && userConnection.SessionID != null && userConnection.SessionExpires != null && userConnection.SessionExpires > DateTime.Now){
				HttpContext.Session.SetString("SessionID", userConnection.SessionID);
				return RedirectToAction("index", "Pret");
			}
			else
				return View();
		}

		[HttpPost]
		public ActionResult Login(string username, string password, bool remember)
		{
			UserConnection userConnection = _db.UserConnections.FirstOrDefault(x => x.Username == username && x.Password == password);

			if (userConnection != null)
			{
				string sessionID = GenerateRandomString(20);
				userConnection.SessionID = sessionID;
				userConnection.SessionExpires = DateTime.Now.AddMinutes(30);
				_db.SaveChanges();

				if (Request.Form["remember"] == "on")
				{
					CookieOptions options = new CookieOptions();
					options.Expires = DateTime.Now.AddMinutes(30);
					Response.Cookies.Append("SessionID", sessionID, options);
				}
				HttpContext.Session.SetString("SessionID", sessionID);
				HttpContext.Session.SetInt32("isAdmin", userConnection.Type == "admin" ? 1 : 0 );

                return RedirectToAction("index", "Pret");
			}
			else
			{
				return View("Index");
			}
		}

		public IActionResult Logout()
		{
			_db.UserConnections.FirstOrDefault(x => x.SessionID == HttpContext.Session.GetString("SessionID")).SessionExpires = DateTime.Now.AddDays(-1);
			_db.SaveChanges();
			HttpContext.Session.SetString("SessionID", "NULL");
			return RedirectToAction("index", "Login");
		}

		public string GenerateRandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			StringBuilder stringBuilder = new StringBuilder();
			Random random = new Random();

			for (int i = 0; i < length; i++)
			{
				stringBuilder.Append(chars[random.Next(chars.Length)]);
			}

			return stringBuilder.ToString();
		}
	}
}
