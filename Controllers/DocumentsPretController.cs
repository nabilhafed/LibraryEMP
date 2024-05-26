using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryEMP.Controllers
{
    public class DocumentsPretController : Controller
    {
        public readonly ApplicationDbContext _db;
        public DocumentsPretController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

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
