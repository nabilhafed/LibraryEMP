using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;


namespace LibraryEMP.Controllers
{
    [Route("RestitutionController")]
    public class RestitutionController : Controller
    {
        public readonly ApplicationDbContext _db;
        public RestitutionController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("getUserByID")]

        public dynamic? GetUserAndExemplairesByID(string IdAdherent)
        {
            if (IdAdherent != "")
            { var user = _db.Adherents
                .Where(a => a.IdAdherent.ToUpper() == IdAdherent.ToUpper())
                .Select(adherent => new
                {
                    nom = adherent.Nom,
                    prenom = adherent.Prenom,
                    etatAdherent = adherent.EtatAdherent,
                })
                .FirstOrDefault();

                var exemplaires = _db.Prets
                    .Where(p => p.IdAdherent.ToUpper() == IdAdherent.ToUpper())
                    .Select(p => new

                    { idExemplaire = p.IdExemplaire })
                    .ToList();

                return new
                {
                    user,
                    exemplaires
                };
            }
            else
                return null;
        }
        [HttpGet]
        [Route("getSelectExemplaire")]

        public dynamic? getSelectExemplaire(string IdExemplaire)
        {
            // Récupérer la cote de l'exemplaire spécifié
            var coteExemplaire = _db.Exemplaires
                .Where(p => p.IdExemplaire.ToUpper() == IdExemplaire.ToUpper())
                .Select(p => p.Cote)
                .FirstOrDefault();
            var datePret  = _db.Prets.Where(p => p.IdExemplaire.ToUpper() == IdExemplaire.ToUpper())
                .Select(p => p.DatePret)
                .FirstOrDefault();

            // Vérifier si la cote de l'exemplaire a été trouvée
            if (coteExemplaire != null)
            {
                // Rechercher la notice correspondant à la cote de l'exemplaire
                var notice = _db.Notices
                    .Where(p => p.Cote.ToUpper() == coteExemplaire.ToUpper())
                    .Select(p => new
                    {
                        PropreTitle = p.TitrePropre
                    })
                    .FirstOrDefault();



                return new { notice , datePret };
            }
            else
            {
                // Gérer le cas où l'exemplaire avec l'ID spécifié n'existe pas
                return null;
            }
        }
        [HttpGet]
        [Route("RetourExemplaire")]

        public dynamic? RetourExemplaire(string IdExemplaire)
        {
            return null;
        }

        [HttpGet]
        [Route("RenouvellementExemplaire")]
        public dynamic? RenouvellementExemplaire(string IdExemplaire)
        {
            return null;
        }



    }
}