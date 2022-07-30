using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUunStokProjesi.Models.Entity;

using MvcUunStokProjesi.Models.Entity;
namespace MvcUunStokProjesi.Controllers
{
    public class satisController : Controller
    {
        // GET: satis
        MvcUrunstokEntities2 db = new MvcUrunstokEntities2();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult yenisatis()
        {
            return View();
        }

        [HttpPost]
        public ActionResult yenisatis(tblsatişlar p)
        {
            db.tblsatişlar.Add(p);
            db.SaveChanges();
            return View("Index");
        }

    }
}