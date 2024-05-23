using LibraryEMP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace LibraryEMP.Controllers
{
    [Route("GDAController")]
    public class GDAController : Controller
    {
        public readonly ApplicationDbContext _db;
        public GDAController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        [Route("getAdherents")]
        public dynamic? getAdherents(string search)
        {
            Console.WriteLine(search);
            return _db.Adherents.ToList();
        }
        //

    }
}
