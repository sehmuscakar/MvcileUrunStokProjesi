using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;// bunlar eklnemeli sayfalama için 
using PagedList.Mvc; // buda 

using MvcUunStokProjesi.Models.Entity;


namespace MvcUunStokProjesi.Controllers
{
    public class urunlerController : Controller
    {
        // GET: urunler
        MvcUrunstokEntities2 db = new MvcUrunstokEntities2();
        public ActionResult Index(int sayfa=1)
        {
            // var degerler = db.tblurunler.ToList();
            var degerler = db.tblurunler.ToList().ToPagedList(sayfa, 4);
            return View(degerler);
        }

        [HttpGet] // bu sayfa geri döndüğünde olsun 
        public ActionResult urunekle()
        {
            List<SelectListItem> degerler = (from i in db.tblkotegoriler.ToList() // linq sorgusu ,kategorileri listele ve i ye ata ,sonra seçili liste öğelerinden yeni seç kategoriad texte id yi                                          
                                             select new SelectListItem      // value ata ve bunların hepsini listele 
                                             {
                                                 Text = i.kategoriad,
                                                 Value   = i.kategoriid.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler; // buda diğer sayfada tanımlanması için yapılması lazım çok önemli bir şey  

            return View();
        }

        [HttpPost] // bu post olayı olduğunda olsun butona basıldığında olsun 
        public ActionResult urunekle (tblurunler p1)
        {
            var ktg = db.tblkotegoriler.Where(m => m.kategoriid == p1.tblkotegoriler.kategoriid).FirstOrDefault();
            // db kategoriler içinde şart ling sorugusundaki kategoriid eşitse p1 deki kategori id sinde ilk olanı getirsin 
            p1.tblkotegoriler = ktg;
            db.tblurunler.Add(p1);
            db.SaveChanges();
            return RedirectToAction("Index"); // kaydetme işlemi olduktan sonra index sayfasına yönlendirsin 


        }
        public ActionResult sil(int id) // burdaki sil view deki adres te sil nasıl yazılmışsa bunu öyle yaz
        {
            var urun = db.tblurunler.Find(id);
            db.tblurunler.Remove(urun);
            db.SaveChanges();// değişliklikleri kaydet 
            return RedirectToAction("Index");

        }

        public ActionResult urungetir(int id)
        {
            var urun = db.tblurunler.Find(id);

            List<SelectListItem> degerler = (from i in db.tblkotegoriler.ToList() // linq sorgusu ,kategorileri listele ve i ye ata ,sonra seçili liste öğelerinden yeni seç kategoriad texte id yi                                          
                                             select new SelectListItem      // value ata ve bunların hepsini listele 
                                             {
                                                 Text = i.kategoriad,
                                                 Value = i.kategoriid.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;
            return View("urungetir", urun);

        }

        public ActionResult guncelle(tblurunler p)
        {
            var urun = db.tblurunler.Find(p.urunid);
            urun.urunad = p.urunad;
            urun.marka = p.marka;
            urun.fiyat = p.fiyat;
            urun.stok = p.stok;
            //  urun.urunkategori = p.urunkategori;// büyle yaparsan hata alırsın diğerleri değişir bu değişmez ve hata alırsın 
      //    var ktg = db.tblkotegoriler.Where(m => m.kategoriid == p.tblkotegoriler.kategoriid).FirstOrDefault();
        //   urun.urunkategori = ktg.kategoriid;
            db.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}