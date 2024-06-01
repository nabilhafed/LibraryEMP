using LibraryEMP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryEMP.Controllers
{
    public class DocumentsReserverController : Controller
    {
        public readonly ApplicationDbContext _db;
        public DocumentsReserverController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
			UserConnection? userConnection = _db.UserConnections.FirstOrDefault(x => x.SessionID == HttpContext.Session.GetString("SessionID"));
			if (userConnection != null && userConnection.SessionExpires > DateTime.Now)
				return View();
			else
				return RedirectToAction("index", "Login");
		}


        public dynamic GetReservationTriByIdAdherent()
        {
 			var documentsRéserver = _db.Reservations
                .Select(p => new
                {
                    IdAdherent = p.IdAdherent,
                    Cote = p.Cote,
                    HeureReservation = p.HeureReservation
                })
                .ToList();

            var adherents = _db.Adherents
                .Select(p => new
                {
                    IdAdherent = p.IdAdherent,
                    Nom = p.Nom,
                    Prenom = p.Prenom
                })
                .ToList();

            var result = documentsRéserver
                .Join(
                    adherents,
                    doc => doc.IdAdherent,
                    adh => adh.IdAdherent,
                    (doc, adh) => new
                    {
                        IdAdherent = doc.IdAdherent,
                        Cote = doc.Cote,
                        HeureReservation = doc.HeureReservation,
                        Nom = adh.Nom,
                        Prenom = adh.Prenom
                    })
                .OrderBy(r => r.IdAdherent) // Tri par IdAdherent
                .ToList();

            return result;
        }
    }
}
