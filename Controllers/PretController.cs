using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;


namespace LibraryEMP.Controllers
{
    #pragma warning disable CS8602 // Dereference of a possibly null reference.
     public class PretController : Controller
    {
        public readonly ApplicationDbContext _db;
        public PretController(ApplicationDbContext db)
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

        public dynamic? getHolidays()
        {
            return _db.JoursFeries.Select(j => new
            {
                date = j.DateJourFerie
            }).ToArray();
        }

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
                                .Any(join => join.Pret.IdAdherent.ToUpper() == IdAdherent.ToUpper() && join.Exemplaire.Cote.ToUpper() == cote.ToUpper())
            })
            .FirstOrDefault();

            if (book != null)
            {
                if(!book.isCurrentlyBorrowedbyHim)
                {
                    var exemples = _db.Exemplaires
                    .Where(e => e.Cote.ToUpper() == cote.ToUpper())
                    .Select(e => new
                    {
                        idEtat = e.IdEtat,
                        idExemplaire = e.IdExemplaire
                    }).ToList();

                    //if he reserved the the book
                    if(_db.Reservations.Any(r => r.Cote.ToUpper() == cote.ToUpper() && r.IdAdherent.ToUpper() == IdAdherent.ToUpper()))
                    {
                        //get position of that adhrent
                        var reservedListPos = _db.Reservations.Where(r => r.Cote.ToUpper() == cote.ToUpper())
                            .OrderBy(r => r.HeureReservation).
                            ToList().FindIndex(r => r.IdAdherent.ToUpper() == IdAdherent.ToUpper());

                        //get Borrowd exemples with IdAdherent == 99/999
                        var reservedExemplesBy99_999 = _db.Prets.Where(p => p.IdAdherent == "99/999")
                            .Join(
                                _db.Exemplaires.Where(e => e.Cote.ToUpper() == cote.ToUpper() && e.IdEtat == 2),
                                pret => pret.IdExemplaire,
                                exemple => exemple.IdExemplaire,
                                (pret, exemple) => exemple.IdExemplaire
                            ).Count();
                        //get reservedEexmpleID
                        string reservedExemple = "";
                        if (reservedExemplesBy99_999 >= reservedListPos)
                            for (int i = 0; i < exemples.Count(); i++)
                                if (exemples[i].idEtat == 2 && reservedListPos-- == 0)
                                    reservedExemple = exemples[i].idExemplaire;
                        return new { book, exemples , reservedExemple };
                    }

                    return new { book, exemples };
                }
                return new { book };
            }
            return null;
        }

        public dynamic? addNewDocumentBorrow(string idExemplaire, string IdAdherent , string returnDate)
        {
            //TODO: add test!!  

            var transaction = _db.Database.BeginTransaction();
            try
            {
                //test 
                var adherent = _db.Adherents.Where(a => a.IdAdherent.ToUpper() == IdAdherent.ToUpper()).FirstOrDefault();
                //test state
                if (adherent.EtatAdherent != 2)
                    throw new Exception("this adherent state is unsatisfiable");
                //test max number
                int borrowedDocuments = _db.Prets.Count( p => p.IdAdherent.ToUpper() == IdAdherent.ToUpper());
                var maxDocumentsToBorrow = _db.Categories.Where( c => c.IdCategorie == adherent.IdCategorie ).FirstOrDefault().NombreDocument;
                if(maxDocumentsToBorrow <= borrowedDocuments)
                    throw new Exception("this adherent can't borrow more Documents!!");

                //get cote!
                string? cote = _db.Exemplaires.FirstOrDefault(e => e.IdExemplaire.ToUpper() == idExemplaire.ToUpper()).Cote;
                //test double borrow
                bool isCurrentlyBorrowedbyHim = _db.Prets
                                .Join(_db.Exemplaires,
                                    pret => pret.IdExemplaire,
                                    exemplaire => exemplaire.IdExemplaire,
                                    (pret, exemplaire) => new { Pret = pret, Exemplaire = exemplaire })
                                .Any(join => join.Pret.IdAdherent.ToUpper() == IdAdherent.ToUpper() && join.Exemplaire.Cote.ToUpper() == cote.ToUpper());
                if (isCurrentlyBorrowedbyHim)
                    throw new Exception("can't borrow the same Document !!");

                ////change status of exemple to 2 (Prêté)
                _db.Exemplaires.FirstOrDefault(e => e.IdExemplaire.ToUpper() == idExemplaire.ToUpper()).IdEtat = 2;
                _db.SaveChanges();

                ////add new Borrow Line
                _db.Prets.Add(new Pret { IdExemplaire = idExemplaire, IdAdherent = IdAdherent, DatePret = DateTime.Parse(returnDate), EtatDuree = "F" });
                _db.SaveChanges();

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
