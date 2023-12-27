using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PROJETAKIP.Models.DataContext;
using PROJETAKIP.Models.Personel;

namespace PROJETAKIP.Controllers
{
    public class PersonelBilgilerisController : Controller
    {
        private ProjeTakipDBContext db = new ProjeTakipDBContext();  //veritabanı bağlantısı kuruyor

        // İndex: Personel ve bilgilerini listeler
        // Create: Yeni personel oluşturmasını sağlar
        // Details: Seçilen personelin bilgilerini gösterir
        // Edit: Seçilen personelin bilgilerini editlemeyi (güncellemeyi) sağlar
        // Delete: Seçilen personelin silinmesini sağlar


        // GET: PersonelBilgileris
        public ActionResult Index()  //verileri listeler
        {
            return View(db.PersonelBilgileris.ToList());
        }


        // GET: PersonelBilgileris/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonelBilgileris/Create, veritabanında görülmesini sağlıyor
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonelBilgileri personelBilgileri)
        {
            if (ModelState.IsValid)
            {
                db.PersonelBilgileris.Add(personelBilgileri);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personelBilgileri);
        }


        // GET: PersonelBilgileris/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonelBilgileri personelBilgileri = db.PersonelBilgileris.Find(id);
            if (personelBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(personelBilgileri);
        }
        

        // GET: PersonelBilgileris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonelBilgileri personelBilgileri = db.PersonelBilgileris.Find(id);
            if (personelBilgileri == null)
            {
                return HttpNotFound();
            }
            return View(personelBilgileri);
        }

        // POST: PersonelBilgileris/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonelBilgileri personelBilgileri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personelBilgileri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personelBilgileri);
        }


        public ActionResult Delete(int? Id)
        {
            if (Id == null || Id == 0)
            {
                return HttpNotFound();
            }
            var t = db.PersonelBilgileris.Find(Id);

            db.PersonelBilgileris.Remove(t);
            db.SaveChanges();

            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
