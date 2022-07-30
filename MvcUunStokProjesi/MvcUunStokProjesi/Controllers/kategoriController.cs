using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcUunStokProjesi.Models.Entity;// modelin içindeki entity klasörünü kütüphane olarak tanımladık 
using PagedList;
using PagedList.Mvc;

namespace MvcUunStokProjesi.Controllers
{
    public class kategoriController : Controller
    {
        // GET: kategori
        MvcUrunstokEntities2 db = new MvcUrunstokEntities2();
        public ActionResult Index(int sayfa=1)
        {
            //   var degerler = db.tblkotegoriler.ToList();// select veri tabanı komutları yerine to list kulandık verileri listeleyecek 
            var degerler = db.tblkotegoriler.ToList().ToPagedList(sayfa, 4);// BURDA sayfalamada listelemeyi yapsın 1 ci sayfa 4 öğe döndürsün ,view kısmıda da index te onu da listelemeyi pagelist olarak yap 

            return View(degerler);
        }
        [HttpGet] // buda herhangi bişey yapılmaza yani butona filan basılmazsa sadece sayfayı geriye  döndür 
       public ActionResult yenikategori()
        {
            return View();
        }

        

        [HttpPost]// ben butona bastığım zaman ekleme işlmei yap 
        public ActionResult yenikategori(tblkotegoriler p1) // burda yeni bir sayfa açtık yenikategori aracılığı ile , sadece büyle kullanırsan sıkıntı ekleme her refresh olduğunda yeni ekleme yapar 
            // bu sıkıntı olamamsı için HttpGet ,HttpPost kullanacaz 
        {
            if (!ModelState.IsValid) // modelin durumunda doğrulama yapılmadıysa 
            {
                return View("yenikategori");
            }
            db.tblkotegoriler.Add(p1);
            db.SaveChanges();
            return View();

        }

        public ActionResult sil(int id) // burdaki sil view deki adres te sil nasıl yazılmışsa bunu öyle yaz
        {
            var kategori = db.tblkotegoriler.Find(id);
            db.tblkotegoriler.Remove(kategori);
            db.SaveChanges();// değişliklikleri kaydet 
            return RedirectToAction("Index");
        }

        public ActionResult kategorigetir(int id)
        {
            var ktgr = db.tblkotegoriler.Find(id);
            return View("kategorigetir",ktgr);// kategorigetir view ile birlikte ktgr değerlerinide bize dündürsün 
        }

        public ActionResult guncelle(tblkotegoriler p1)
        {
            var ktg = db.tblkotegoriler.Find(p1.kategoriid);// p1 referansından  den id yi bul
            ktg.kategoriad = p1.kategoriad; // p1 referansından kategoriadı ktg kategoriad a ata 
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}