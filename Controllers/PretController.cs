using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace LibraryEMP.Controllers
{
    [Route("PretController")]
    public class PretController : Controller
    {
        public readonly ApplicationDbContext _db;
        public PretController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("getUserByID")]
        public dynamic? getUserByID(string IdAdherent)
        {   
            return _db.Adherents
            .Where(a => a.IdAdherent.ToUpper() == IdAdherent.ToUpper())
            .Join(
                _db.Categories,
                adherent => adherent.IdCategorie,
                categor => categor.IdCategorie,
                (adherent, categor) => new
                {
                    nom = adherent.Nom,
                    prenom = adherent.Prenom,
                    etatAdherent = adherent.EtatAdherent,
                    readyDate = categor.DureePret,
                    //get the max borrow number and current borrow book
                    documentsAuthorized = categor.NombreDocument,
                    documentsOnborrow = _db.Prets.Count(p => p.IdAdherent.ToUpper() == adherent.IdAdherent.ToUpper())
                }
            )
            .FirstOrDefault();
        }

        [HttpGet]
        [Route("getHolidays")]
        public dynamic? getHolidays()
        {
            return _db.JoursFeries.Select(j => new
            {
                date = j.DateJourFerie
            }).ToArray();
        }

        [HttpGet]
        [Route("getBookByCoteForBorrow")]
        public dynamic? getBookByCoteForBorrow(string cote , string IdAdherent)
        {
            var book =  _db.Notices
            .Where(n => n.Cote.ToUpper() == cote.ToUpper())
            .Select(n => new
            {
                titrePropre = n.TitrePropre,
                idNotice = n.IdNotice,
                //see if the Adherent is currently borrow that book !!
                canBeBorrow =   _db.Prets
                                .Join(_db.Exemplaires,
                                    pret => pret.IdExemplaire,
                                    exemplaire => exemplaire.IdExemplaire,
                                    (pret, exemplaire) => new { Pret = pret, Exemplaire = exemplaire })
                                .Where(join => join.Pret.IdAdherent.ToUpper() == IdAdherent.ToUpper() && join.Exemplaire.Cote.ToUpper() == cote.ToUpper())
                                .Count() == 0
                //see if the Adherent has reserve that book before ?
                //reservedByHim = _db.Reservations.Any(r => r.Cote.ToUpper() == cote.ToUpper() && r.IdAdherent.ToUpper() == IdAdherent.ToUpper())

            })
            .FirstOrDefault();

            if (book != null)
            {
                if(book.canBeBorrow){
                    var avilables = _db.Exemplaires
                    .Where(e => e.Cote.ToUpper() == cote.ToUpper())
                    .Select(e => new
                    {
                        idEtat = e.IdEtat,
                        idExemplaire = e.IdExemplaire
                }).ToList();
                    return new { book, avilables };
                }
                return new { book };
            }
            return null;
        }

    }
}
