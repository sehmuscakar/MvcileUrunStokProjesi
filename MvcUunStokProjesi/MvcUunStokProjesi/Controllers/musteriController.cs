using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUunStokProjesi.Models.Entity;

namespace MvcUunStokProjesi.Controllers
{
    public class musteriController : Controller
    {
        MvcUrunstokEntities2 db = new MvcUrunstokEntities2();
        // GET: musteri
        public ActionResult Index(string p)
        {
            var degerler = from d in db.tblmusteriler select d;// linq sorgusu :musterilerden cek d ye at ve ve d den çek 
            if (!string.IsNullOrEmpty(p))// p değeri boş değilse veya null değilse bunları yap 
            {
                degerler = degerler.Where(m => m.musteriad.Contains(p));// musteri adında değerler p ile aynı olanları getirecek
            }
            return View(degerler.ToList());// paremetre boş sa bunu yap 
            //var degerler = db.tblmusteriler.ToList();
           // return View(degerler);
        }

        [HttpGet]
        public ActionResult yenimusteri()
        {
            return View();
        }


        [HttpPost]
        public ActionResult yenimusteri(tblmusteriler p1)
        {
            if (!ModelState.IsValid) // modelin durumunda doğrulama yapılmadıysa 
            {
                return View("yenimusteri");
            }
            db.tblmusteriler.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult sil(int id)
        {
            var musteri = db.tblmusteriler.Find(id);
            db.tblmusteriler.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult musterigetir(int id)
        {
            var mus = db.tblmusteriler.Find(id);
            return View("musterigetir", mus); // musterigetir sayfasını geri döndürsün mus verileri ile birlikte 


        }

        public ActionResult guncelle(tblmusteriler p1) 
        {
            var musteri = db.tblmusteriler.Find(p1.musteriid);
            musteri.musteriad = p1.musteriad;
            musteri.musterisoyad = p1.musterisoyad;
            db.SaveChanges();
            return RedirectToAction("Index");//çalıştırırken normal index ten çalıştır ki hatta olmasın  
        }
    }
}