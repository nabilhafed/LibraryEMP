using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LibraryEMP.Controllers
{
    public class GDDController : Controller
    {
        public readonly ApplicationDbContext _db;
        public GDDController(ApplicationDbContext db)
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

        public dynamic? getNotices()
        {
            // First query: get notices
            var notices = _db.Notices
                .Where(n => n.Cote != null)
                .Select(n => new
                {
                    idNotice = n.IdNotice,
                    cote = n.Cote,
                    titre = n.TitrePropre
                })
                .ToList();

            // Second query: get the count of exemplaires grouped by Cote and ID_ETAT
            var exemplaireCounts = _db.Exemplaires
                .Where(e => e.Cote != null)
                .GroupBy(e => new { e.Cote, e.IdEtat })
                .Select(g => new
                {
                    cote = g.Key.Cote,
                    etat = g.Key.IdEtat,
                    count = g.Count()
                })
                .ToList();

            // Join the two results in memory and aggregate the counts
            var result = from notice in notices
                         join count in exemplaireCounts
                         on notice.cote equals count.cote into exemplaireGroup
                         from eg in exemplaireGroup.DefaultIfEmpty()
                         group eg by new { notice.idNotice, notice.cote , notice.titre } into g
                         select new
                         {
                             idNotice = g.Key.idNotice,
                             cote = g.Key.cote,
                             titre = g.Key.titre,
                             totalExemplaire = g.Sum(x => x != null ? x.count : 0),
                             perduNumber = g.Where(x => x != null && x.etat == 3).Sum(x => x.count),
                             disponibleNumber = g.Where(x => x != null && x.etat == 1).Sum(x => x.count),
                             pretNumber = g.Where(x => x != null && x.etat == 2).Sum(x => x.count),
                             enCoursDeTraitementNumber = g.Where(x => x != null && x.etat == 0).Sum(x => x.count)
                         };

            return result.ToList();
        }

        public dynamic getExemplairesFromCote(string cote)
        {
            return _db.Exemplaires
                    .Where(e => e.Cote == cote)
                    .Join(_db.EtatExemplaires,
                          e => e.IdEtat,
                          etat => etat.IdEtat,
                          (e, etat) => new
                          {
                              IdExemplaire = e.IdExemplaire,
                              idEtat = e.IdEtat,
                              etat = etat.LibelleEtat,
                          })
                    .ToList();
        }
    }
}
