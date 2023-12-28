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


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonelProjeleri personelProjeleri = db.PersonelProjeleris.Find(id);
            if (personelProjeleri == null)
            {
                return HttpNotFound();
            }
            return View(personelProjeleri);
        }

        

        // GET: PersonelProjeleris/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonelProjeleri personelProjeleri = db.PersonelProjeleris.Find(id);
            if (personelProjeleri == null)
            {
                return HttpNotFound();
            }
            return View(personelProjeleri);
        }

        // POST: PersonelProjeleris/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PersonelProjeId,ProjeBaslik,ProjeAciklama,OlusturmaTarihi,OncelikDurumu,TamamlanmaOrani,TamamlanmaTarihi,TamamlanmaDurumu")] PersonelProjeleri personelProjeleri)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personelProjeleri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personelProjeleri);
        }

        // GET: PersonelProjeleris/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonelProjeleri personelProjeleri = db.PersonelProjeleris.Find(id);
            if (personelProjeleri == null)
            {
                return HttpNotFound();
            }
            return View(personelProjeleri);
        }

        // POST: PersonelProjeleris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonelProjeleri personelProjeleri = db.PersonelProjeleris.Find(id);
            db.PersonelProjeleris.Remove(personelProjeleri);
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
