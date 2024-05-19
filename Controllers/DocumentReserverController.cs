using LibraryEMP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryEMP.Controllers
{
    [Route("DocumentReserverController")]

    public class DocumentReserverController : Controller
    {
        public readonly ApplicationDbContext _db;
        public DocumentReserverController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("GetReservationTriByIdAdherent")]

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
        [HttpGet]
        [Route("GetReservationTriByIdCote")]

        public dynamic GetReservationTriByIdCote()
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
                .OrderBy(r => r.Cote) // Tri par Cote
                .ToList();

            return result;
        }
        [HttpGet]
        [Route("GetReservationTriByIdDate")]

        public dynamic GetReservationTriByIdDate()
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
                .OrderBy(r => r.HeureReservation) // Tri par Cote
                .ToList();

            return result;
        }
    }
}
