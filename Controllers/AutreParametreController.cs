using LibraryEMP.Models;
using Microsoft.AspNetCore.Http;
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
        public dynamic? RemoveJourFeries(string dateJourFeries)
        {


            DateTime date = DateTime.Parse(dateJourFeries);
            int year = date.Year;
            int month = date.Month;
            int day = date.Day;
            int hour = date.Hour;
            int minute = date.Minute;
            int second = date.Second;
            // Récupérer le jour férié à supprimer
            var jourFerie = _db.JoursFeries
                .FirstOrDefault(j => (j.DateJourFerie.Date) == date);

            if (jourFerie != null)
            {
                // Supprimer le jour férié de la base de données
                _db.JoursFeries.Remove(jourFerie);
                _db.SaveChanges(); // Enregistrer les changements
                return date;
            }

            return false;
        }



    }
}
