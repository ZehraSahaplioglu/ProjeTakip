using PROJETAKIP.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJETAKIP.Controllers
{
    public class ProjeRaporlariController : Controller
    {
        public ProjeTakipDBContext db = new ProjeTakipDBContext();

        public ActionResult TamamlanmisOncelikGruplari()
        {
            return View();
        }

        public ActionResult VisualizeTamamlanmisDurumGruplari()
        {
            return Json(OncelikGrupTipi(), JsonRequestBehavior.AllowGet);  //grafik döndüreceği için json kullandık
        }

        //grafik oluşturacağımız class:
        public List<ClassOncelikDurumAnaliz> OncelikGrupTipi()
        {
            ;
            List<ClassOncelikDurumAnaliz> snf = new List<ClassOncelikDurumAnaliz>();
            using (var c = new ProjeTakipDBContext())
                snf = c.PersonelProjeleris.Where(x => x.TamamlanmaDurumu == true).GroupBy(p => p.OncelikDurumu).Select(x => new ClassOncelikDurumAnaliz
                {
                    onceliktipi = x.Key,
                    oncelikadeti = x.Count(),
                }).ToList();
            return snf;
        }


        public ActionResult TamamlanmamisOncelikGruplari()
        {
            return View();
        }

        public ActionResult VisualizeTamamlanmamisDurumGruplari()
        {
            return Json(TamamlanmamisOncelikGrupTipi(), JsonRequestBehavior.AllowGet);  //grafik döndüreceği için json kullandık
        }

        //grafik oluşturacağımız class:
        public List<ClassOncelikDurumAnaliz> TamamlanmamisOncelikGrupTipi()
        {
            ;
            List<ClassOncelikDurumAnaliz> snf = new List<ClassOncelikDurumAnaliz>();
            using (var c = new ProjeTakipDBContext())
                snf = c.PersonelProjeleris.Where(x => x.TamamlanmaDurumu == false).GroupBy(p => p.OncelikDurumu).Select(x => new ClassOncelikDurumAnaliz
                {
                    onceliktipi = x.Key,
                    oncelikadeti = x.Count(),
                }).ToList();
            return snf;
        }
    }
}