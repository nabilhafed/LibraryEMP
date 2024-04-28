using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Diagnostics;
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
                    readyDate = categor.DureePret
                }
            )
            .FirstOrDefault();
        }

        [HttpGet]
        [Route("getBookByCote")]
        public dynamic? getBookByCote(string cote)
        {
            var book =  _db.Notices
            .Where(n => n.Cote.ToUpper() == cote.ToUpper())
            .Select(n => new
            {
                titrePropre = n.TitrePropre,
                idNotice = n.IdNotice
            })
            .FirstOrDefault();

            var avilables = _db.Exemplaires
            .Where(e => e.Cote.ToUpper() == cote.ToUpper())
            .ToList();

            return new { book, avilables };
        }

    }
}
