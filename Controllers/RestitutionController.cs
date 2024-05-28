﻿using System.Drawing.Printing;
using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace LibraryEMP.Controllers
{
    public class RestitutionController : Controller
    {
        public readonly ApplicationDbContext _db;
        public RestitutionController(ApplicationDbContext db)
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

            ValiderRestitution(IdExemplaire, DateTime.Now);

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


    
        public void RenouvellementExemplaire(string IdAdherent , string IdExemplaire)
        {
            // Récupérer le prêt à supprimer
            var pret = _db.Prets
                .FirstOrDefault(p => p.IdExemplaire.ToUpper() == IdExemplaire.ToUpper() && p.IdAdherent.ToUpper() == IdAdherent.ToUpper());


            if (pret != null)
            {
                // Supprimer le prêt de la base de données
                _db.Prets.Remove(pret);
            }

            ValiderRenouvellement(IdExemplaire, DateTime.Now, IdAdherent);

            _db.SaveChanges();

        }

        //fonction pour la validation de retour d'un exemplaire 

        public void ValiderRestitution(string  ExemplaireDisponible, DateTime dateRetour)
        {
            var cote = _db.Exemplaires
                .Where(p => p.IdExemplaire.ToUpper() == ExemplaireDisponible.ToUpper())
                .Select(p => p.Cote).FirstOrDefault();
            // Convertir la cote et l'exemplaire en majuscules
            cote = cote.ToUpper();
            ExemplaireDisponible = ExemplaireDisponible.ToUpper();

            // Récupérer le nombre de réservations pour la cote
            int nbrReservations = _db.Reservations
                .Count(r => r.Cote.ToUpper() == cote);

            // Récupérer le nombre de prêts pour la cote et l'utilisateur spécifique
            string idAdherent = "99/999";
            int nbrPretReservations = _db.Prets
                .Count(p => p.IdExemplaire.ToUpper().StartsWith(cote + "/") && p.IdAdherent == idAdherent);

            if (nbrReservations > 0)
            {
                if (nbrPretReservations < nbrReservations)
                {
                    // Insérer un nouveau prêt
                    var pret = new Pret
                    {
                        IdAdherent = idAdherent,
                        IdExemplaire = ExemplaireDisponible,
                        DatePret = dateRetour,
                        EtatDuree = "F"
                    };
                    _db.Prets.Add(pret);

                    // Mettre à jour l'état de l'exemplaire
                    var exemplaire = _db.Exemplaires
                        .FirstOrDefault(e => e.IdExemplaire.ToUpper() == ExemplaireDisponible);
                    if (exemplaire != null)
                    {
                        exemplaire.IdEtat = 2;
                    }
                }
            }
            else
            {
                // Mettre à jour l'état de l'exemplaire si aucune réservation n'est en cours
                var exemplaire = _db.Exemplaires
                    .FirstOrDefault(e => e.IdExemplaire.ToUpper() == ExemplaireDisponible);
                if (exemplaire != null)
                {
                    exemplaire.IdEtat = 1;
                }
            }

            // Enregistrer les changements dans la base de données
            _db.SaveChanges();
        }


        public void ValiderRenouvellement(string ExemplaireDisponible, DateTime dateRetour,string IdAdherent)
        {
            var cote = _db.Exemplaires
                .Where(p => p.IdExemplaire.ToUpper() == ExemplaireDisponible.ToUpper())
                .Select(p => p.Cote).FirstOrDefault();
            // Convertir la cote et l'exemplaire en majuscules
            cote = cote.ToUpper();
            ExemplaireDisponible = ExemplaireDisponible.ToUpper();

            // Récupérer le nombre de réservations pour la cote
            int nbrReservations = _db.Reservations
                .Count(r => r.Cote.ToUpper() == cote);

            // Récupérer le nombre de prêts pour la cote et l'utilisateur spécifique
            string idAdherent = "99/999";
            int nbrPretReservations = _db.Prets
                .Count(p => p.IdExemplaire.ToUpper().StartsWith(cote + "/") && p.IdAdherent == idAdherent);

            if (nbrReservations > 0)
            {
                if (nbrPretReservations < nbrReservations)
                {
                    // Insérer un nouveau prêt
                    var pret = new Pret
                    {
                        IdAdherent = idAdherent,
                        IdExemplaire = ExemplaireDisponible,
                        DatePret = dateRetour,
                        EtatDuree = "F"
                    };
                    _db.Prets.Add(pret);

                    // Mettre à jour l'état de l'exemplaire
                    var exemplaire = _db.Exemplaires
                        .FirstOrDefault(e => e.IdExemplaire.ToUpper() == ExemplaireDisponible);
                    if (exemplaire != null)
                    {
                        exemplaire.IdEtat = 2;
                    }
                }
                else
                {
                    if (!AdherentPénalilser(IdAdherent)) // la'dherent n'est pas pénalisé
                    {
                        var pret = new Pret
                        {
                            IdAdherent = IdAdherent,
                            IdExemplaire = ExemplaireDisponible,
                            DatePret = DateTime.Now,
                            EtatDuree = "F"
                        };

                        _db.Prets.Add(pret);
                    }
                   

                }
            }
            else
            {
                // Mettre à jour l'état de l'exemplaire si aucune réservation n'est en cours
                var exemplaire = _db.Exemplaires
                    .FirstOrDefault(e => e.IdExemplaire.ToUpper() == ExemplaireDisponible);
                if (exemplaire != null)
                {
                    exemplaire.IdEtat = 1;
                }
            }

            // Enregistrer les changements dans la base de données
            _db.SaveChanges();
        }

        public Boolean AdherentPénalilser(string IdAherent)
        {
            var etatAdherent = _db.Adherents
                 .Where(p => p.IdAdherent.ToUpper() == IdAherent.ToUpper())
                 .Select(p => p.EtatAdherent).FirstOrDefault();

            if(etatAdherent != null)
            {
                if (etatAdherent == 2)
                {
                    return true;
                }
            }
            return false;
        }


    }
}