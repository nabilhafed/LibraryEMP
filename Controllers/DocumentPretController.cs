using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryEMP.Controllers
{
    [Route("DocumentPretController")]

    public class DocumentPretController : Controller
    {
        public readonly ApplicationDbContext _db;
        public DocumentPretController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("getPret")]
        public dynamic? getPret(string search)
        {
            Console.WriteLine(search);
            var resultat =  _db.Prets
                .Select(p => new
                {
                    idAdherent = p.IdAdherent,
                    idExemplaire = p.IdExemplaire,
                    datePret = p.DatePret,
                    etatDate = p.EtatDuree
                })
                .ToList();
            return resultat; 
        }
    }
}
