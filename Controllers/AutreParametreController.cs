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
    }
}
