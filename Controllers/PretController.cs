using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace LibraryEMP.Controllers
{
    #pragma warning disable CS8602 // Dereference of a possibly null reference.
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
                isCurrentlyBorrowedbyHim =   _db.Prets
                                .Join(_db.Exemplaires,
                                    pret => pret.IdExemplaire,
                                    exemplaire => exemplaire.IdExemplaire,
                                    (pret, exemplaire) => new { Pret = pret, Exemplaire = exemplaire })
                                .Where(join => join.Pret.IdAdherent.ToUpper() == IdAdherent.ToUpper() && join.Exemplaire.Cote.ToUpper() == cote.ToUpper())
                                .Count() > 0
                //see if the Adherent has reserve that book before ?
                //reservedByHim = _db.Reservations.Any(r => r.Cote.ToUpper() == cote.ToUpper() && r.IdAdherent.ToUpper() == IdAdherent.ToUpper())

            })
            .FirstOrDefault();

            if (book != null)
            {
                if(!book.isCurrentlyBorrowedbyHim)
                {
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

        [HttpPost]
        [Route("addNewDocumentBorrow")]
        public dynamic? addNewDocumentBorrow(string idExemplaire, string IdAdherent , string returnDate)
        {
            //TODO: add test!!  

            var transaction = _db.Database.BeginTransaction();
            try
            {
                //get cote!
                string? cote = _db.Exemplaires.FirstOrDefault(e => e.IdExemplaire.ToUpper() == idExemplaire.ToUpper()).Cote;

                ////change status of exemple to 2 (Prêté)
                //_db.Exemplaires.FirstOrDefault(e => e.IdExemplaire.ToUpper() == idExemplaire.ToUpper()).IdEtat = 2;
                //_db.SaveChanges();

                ////add new Borrow Line
                //_db.Prets.Add(new Pret { IdExemplaire = idExemplaire, IdAdherent = IdAdherent, DatePret = DateTime.Parse(returnDate), EtatDuree = "F" });
                //_db.SaveChanges();

                //change state of exemple
                //_db.Exemplaires.FirstOrDefault(e => e.IdExemplaire.ToUpper() == idExemplaire.ToUpper()).IdEtat = 2;
                //_db.SaveChanges();

                try
                {
                    var reservationToDelete = _db.Reservations.FirstOrDefault(r => r.IdAdherent.ToUpper() == IdAdherent.ToUpper() && r.Cote.ToUpper() == cote.ToUpper());
                    if (reservationToDelete != null)
                    {
                        // Remove the found reservation
                        _db.Reservations.Remove(reservationToDelete);
                        _db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"An error occurred while deleting reservation: {ex.Message}");
                }

                transaction.Commit();
                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                transaction.Rollback();
                return null;
            }
            
        }

    }
}
