using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace LibraryEMP.Controllers
{
    public class GDAController : Controller
    {
        public readonly ApplicationDbContext _db;
        public GDAController(ApplicationDbContext db)
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


        public dynamic? getAdherents(string search)
        {
            return _db.Adherents.Select((a) => new
            {
                idAdherent = a.IdAdherent,
                nom = a.Nom,
                prenom = a.Prenom,
                idPosition = a.IdPosition,
                position = _db.Positions.Where((p) => p.IdPosition == a.IdPosition).First().LibellePosition,
                idCategorie = a.IdCategorie,
                categorie = _db.Categories.Where((c) => c.IdCategorie == a.IdCategorie).First().LibelleCategorie,
                etatAdherent = a.EtatAdherent,
                etat = _db.EtatAdherents.Where((e) => e.IdEtat == a.EtatAdherent).First().DescEtat,
                canBeDeleted = !_db.Prets.Any((p)=>p.IdAdherent == a.IdAdherent)
            }).ToList();
        }

        public dynamic? getInformation()
        {
            var pos = _db.Positions.ToList();
            var cat = _db.Categories.Where((c) => c.IdCategorie != "R").ToList();

            return new {pos,cat};
        }

        public bool isIDAvailable(string idAdherent)
        {
            return !_db.Adherents.Any((a)=>a.IdAdherent.ToUpper() == idAdherent.ToUpper());
        }


        public bool addAdherent(string idAdherent , string adherentName , string adherentPrename , string idPosition , string idCategorie)
        {
            Console.WriteLine(idAdherent + "  " + adherentName + "  ");
            //test
            if ( _db.Adherents.Any((a) => a.IdAdherent.ToUpper() == idAdherent))
                return false;
            if (adherentName.Length < 3 || Regex.IsMatch(adherentName, @"[!@#$%^&*(),.?""{}|<>]"))
                return false;
            if (adherentPrename.Length < 3 || Regex.IsMatch(adherentPrename, @"[!@#$%^&*(),.?""{}|<>]"))
                return false;

            //insert
            Adherent adherent = new Adherent();
            adherent.IdAdherent = idAdherent;
            adherent.Nom = adherentName;
            adherent.Prenom = adherentPrename;
            adherent.IdPosition = int.Parse(idPosition);
            adherent.IdCategorie = idCategorie;
            adherent.EtatAdherent = 1;
            _db.Adherents.Add(adherent);
            _db.SaveChanges();

            return true;

        }

        public bool deleteAdherent(string idAdherent)
        {
            //test if adherent can be deleted
            bool canBeDeleted = !_db.Prets.Any((p) => p.IdAdherent == idAdherent);
            Console.WriteLine(idAdherent + canBeDeleted);
            if (canBeDeleted == false)
                return false;
            try
            {
                Adherent adherentToRemove = _db.Adherents.Where((a) => a.IdAdherent == idAdherent).First();
                _db.Adherents.Remove(adherentToRemove);
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool modifiedAdherent(string idAdherentOld , string idAdherent, string adherentName, string adherentPrename, string idPosition, string idCategorie)
        {
            //test
            if (!_db.Adherents.Any((a) => a.IdAdherent.ToUpper() == idAdherentOld.ToUpper()))
                return false;
            if ( idAdherent != idAdherentOld && _db.Adherents.Any((a) => a.IdAdherent.ToUpper() == idAdherent.ToUpper()))
                return false;
            if (adherentName.Length < 3 || Regex.IsMatch(adherentName, @"[!@#$%^&*(),.?""{}|<>]"))
                return false;
            if (adherentPrename.Length < 3 || Regex.IsMatch(adherentPrename, @"[!@#$%^&*(),.?""{}|<>]"))
                return false;
            try
            {
                Adherent adherent = _db.Adherents.Where((a) => a.IdAdherent.ToUpper() == idAdherent.ToUpper()).First();
                adherent.IdAdherent = idAdherent;
                adherent.Nom = adherentName;
                adherent.Prenom = adherentPrename;
                adherent.IdPosition = int.Parse(idPosition);
                adherent.IdCategorie = idCategorie;
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
