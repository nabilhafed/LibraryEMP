using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryEMP.Controllers
{
    public class AutreParametreController : Controller
    {
        

        public readonly ApplicationDbContext _db;
        public AutreParametreController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("IsLoggedIn") != 1)
                return RedirectToAction("index", "Login");
            else
                return View();
        }

        public dynamic? getJouresFeries()
        {

                var dateJourFeries = _db.JoursFeries
                    .Select(p => new
                    {
                        dateJourFeries = p.DateJourFerie
                    })
                    .ToList();

            return dateJourFeries;
        }

        public dynamic? getPenalite()
        {

            var dateJourFeries = _db.Penalites
                .Select(p => new
                {
                    id_Catégorie     = p.IdCategorie ,
                    joursRetard      = p.JoursRetard ,
                    nombreJourRetard = p.NombreJoursRetard
                })
                .ToList();

            return dateJourFeries;
        }


    }
}
