using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROJETAKIP.Models.DataContext;
using PROJETAKIP.Models.ProjeTakip;

namespace PROJETAKIP.Controllers
{
    public class PersonelProjelerisController : Controller
    {
        private ProjeTakipDBContext db = new ProjeTakipDBContext();

        
        public ActionResult Index()
        {
            var projelistele = db.PersonelProjeleris.ToList();
            return View(projelistele);
        }


        public ActionResult Create()
        {
            ViewBag.PersonelBilgileriId = new SelectList(db.PersonelBilgileris, "PersonelBilgileriId", "AdSoyad");
            return View();
        }

        [HttpPost]
        public ActionResult Create(PersonelProjeleri projeObj, int[] PersonelBilgileriId)
        {
            foreach (var x in PersonelBilgileriId)  //PersonelBilgileri tablosundaki personel bilgilerini buluyoruz
            {
                projeObj.PersonelBilgileris.Add(db.PersonelBilgileris.Find(x));
            }
            projeObj.OlusturmaTarihi = DateTime.Now;  //create işlemi gerçekleşir gerçekleşmez o anki zamanı alır
            db.PersonelProjeleris.Add(projeObj);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        public ActionResult Edit(int id)
        {
            var projeObj = db.PersonelProjeleris.Find(id);
            return View(projeObj);
        }

        [HttpPost]
        public ActionResult Edit(PersonelProjeleri projeObj)
        {
            var projeDbObj = db.PersonelProjeleris.Find(projeObj.PersonelProjeId);
            projeDbObj.ProjeAciklama = projeObj.ProjeAciklama;
            projeDbObj.ProjeBaslik = projeObj.ProjeBaslik;
            projeDbObj.TamamlanmaOrani = projeObj.TamamlanmaOrani;
            projeDbObj.OncelikDurumu = projeObj.OncelikDurumu;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult Tamamla(int id)
        {
            var projeObj = db.PersonelProjeleris.Find(id);
            projeObj.TamamlanmaDurumu = true;
            projeObj.TamamlanmaOrani = 100;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


    }
}
