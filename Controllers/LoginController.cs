using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PROJETAKIP.Models.DataContext;
using PROJETAKIP.Models.Personel;

namespace PROJETAKIP.Controllers
{
    public class LoginController : Controller
    {
        private ProjeTakipDBContext db = new ProjeTakipDBContext();


        //Login kısmı:
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Index(PersonelBilgileri admin)
        {
            var bilgiler = db.PersonelBilgileris.FirstOrDefault(x => x.AdSoyad==admin.AdSoyad && x.Sifre==admin.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.AdSoyad, false);
                Session["Kullanici"] = bilgiler.AdSoyad.ToString();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
 
        }


        // Logout kısmı:

        public ActionResult Logout()
        {
            Session["Kullanici"] = null;
            Session.Abandon();
            return RedirectToAction("Index");
        }

    }
}
