using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryEMP.Controllers
{
    [Route("AutreParametreController")]
    public class AutreParametreController : Controller
    {
        

        public readonly ApplicationDbContext _db;
        public AutreParametreController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("getJouresFeries")]

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
        [HttpGet]
        [Route("getPenalite")]

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
