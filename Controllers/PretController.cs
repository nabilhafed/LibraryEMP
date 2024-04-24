using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
            .Where(a => a.IdAdherent.ToUpper() == (IdAdherent.ToUpper()))
            .Select(a => new
            {
                nom = a.Nom,
                prenom = a.Prenom,
                idCategorie = a.IdCategorie,
                etatAdherent = a.EtatAdherent

            }).FirstOrDefault();
        }

    }
}
