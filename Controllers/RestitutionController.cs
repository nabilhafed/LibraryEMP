using System.Drawing.Printing;
using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


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
            {
                var adherent = _db.Adherents
                    .Where(a => a.IdAdherent.ToUpper() == IdAdherent.ToUpper())
                    .Select(a => new
                    {
                        nom = a.Nom,
                        prenom = a.Prenom,
                        etatAdherent = a.EtatAdherent
                    })
                    .FirstOrDefault();

                var exemplaires = _db.Prets
                    .Where(p => p.IdAdherent.ToUpper() == IdAdherent.ToUpper())
                    .Select(p => new
                    {
                        idExemplaire = p.IdExemplaire
                    })
                    .ToList();

                return new
                {
                    adherent,
                    exemplaires
                };
            }
            else
            {

                return null;
            }
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
                var TitrePropre = _db.Notices
                    .Where(p => p.Cote.ToUpper() == coteExemplaire.ToUpper())
                    .Select(
                        p => p.TitrePropre
                    )
                    .FirstOrDefault();

                return new { TitrePropre, datePret };
            }
            else
            {
                // Gérer le cas où l'exemplaire avec l'ID spécifié n'existe pas
                return null;
            }
        }
        [HttpGet]
        [Route("RetourExemplaire")]

        public void RetourExemplaire(string IdExemplaire, string IdAdherent)
        {
            // Récupérer la date de prêt
            var datePret = _db.Prets
                .Where(p => p.IdExemplaire.ToUpper() == IdExemplaire.ToUpper() && p.IdAdherent.ToUpper() == IdAdherent.ToUpper())
                .Select(p => p.DatePret)
                .FirstOrDefault();

            // Insérer les données dans la table HISTORIQUE_PRET
            var historiquePret = new HistoriquePret
            {
                IdAdherent = IdAdherent,
                IdExemplaire = IdExemplaire,
                DatePret = datePret,
                DateRetour = DateTime.Now // Date de retour actuelle
            };

            // Ajouter l'objet historiquePret à la base de données

            //_db.HistoriquePrets.Add(historiquePret);

            // Récupérer le prêt à supprimer
            var pret = _db.Prets
                .FirstOrDefault(p => p.IdExemplaire.ToUpper() == IdExemplaire.ToUpper() && p.IdAdherent.ToUpper() == IdAdherent.ToUpper());

            if (pret != null)
            {
                // Supprimer le prêt de la base de données
                _db.Prets.Remove(pret);
            }
        //
            // Enregistrer les modifications dans la base de données
            _db.SaveChanges();

         
        }


        [HttpGet]
        [Route("RenouvellementExemplaire")]
        public dynamic? RenouvellementExemplaire(string IdExemplaire)
        {
            return null;
        }



    }
}