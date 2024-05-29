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
            
            // Récupérer le jour férié à supprimer
            var jourFerie = _db.JoursFeries
                .FirstOrDefault(j => j.DateJourFerie == date);

            if (jourFerie != null)
            {
                // Supprimer le jour férié de la base de données
                _db.JoursFeries.Remove(jourFerie);
                _db.SaveChanges(); // Enregistrer les changements
                return date;
            }

            return false;
        }
        public dynamic? AddJourFeries(string inputDate)
        {


            DateTime date = DateTime.Parse(inputDate);
             
           

            if (date != null)
            {
                var JourFeries = new JoursFery
                {
                    DateJourFerie = date
                };
                // Supprimer le jour férié de la base de données
                _db.JoursFeries.Add(JourFeries);
                _db.SaveChanges(); // Enregistrer les changements
                return true;
                
            }

            return false;
        }
        public dynamic? UpdateJourFeries(string inputModifier, string inputDate)
        {
            DateTime date = DateTime.Parse(inputDate);

            DateTime dateModifier = DateTime.Parse(inputModifier);

            if (date != null)
            {
                // Recherche du jour férié existant par ID
                var jourFerie = _db.JoursFeries
                               .FirstOrDefault(j => j.DateJourFerie == dateModifier);
                _db.JoursFeries.Remove(jourFerie);

                var JourFerie = new JoursFery
                {
                    DateJourFerie = date
                };
                _db.JoursFeries.Add(JourFerie);

                _db.SaveChanges(); // Enregistrer les changements
                return date;

            }

            return false;
        }

        



    }
}
