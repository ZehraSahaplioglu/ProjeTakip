using PROJETAKIP.Models.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PROJETAKIP.Controllers
{
    public class GenelBakisController : Controller
    {
        private ProjeTakipDBContext db = new ProjeTakipDBContext();
        
        public ActionResult Index()
        {
            int projeSayisi = db.PersonelProjeleris.Count();
            ViewBag.ProjeSayisi = projeSayisi;

            int tamamlanmisProje = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == true).Count();
            ViewBag.TamamlanmisProje = tamamlanmisProje;

            int tamamlanmamisProje = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == false).Count();
            ViewBag.TamamlanmamisProje = tamamlanmamisProje;

            var yuksekOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Yüksek Öncelikli").Count();
            ViewBag.YuksekOncelikli = yuksekOncelikliProjeler;

            var ortaOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Orta Öncelikli").Count();
            ViewBag.OrtaOncelikli = ortaOncelikliProjeler;

            var dusukOncelikliProjeler = db.PersonelProjeleris.Where(p => p.OncelikDurumu == "Düşük Öncelikli").Count();
            ViewBag.DusukOncelikli = dusukOncelikliProjeler;

            var basariliYuksek = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == true && p.OncelikDurumu == "Yüksek Öncelikli").Count();
            ViewBag.Basariliyuksek = basariliYuksek;

            var basariliOrta = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == true && p.OncelikDurumu == "Orta Öncelikli").Count();
            ViewBag.Basariliorta = basariliOrta;

            var basariliDusuk = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == true && p.OncelikDurumu == "Düşük Öncelikli").Count();
            ViewBag.Basarilidusuk = basariliDusuk;

            var personelProjeListesi = db.PersonelProjeleris.ToList();
            var personelTamamlanmisProjeSayisi = new Dictionary<int, int>();  //personelid ve tamamlanmışprojesayısı verisi için
            foreach(var personel in db.PersonelBilgileris.ToList())
            {
                int tamamlanmisProjeSayisi = 0;
                foreach (var proje in personel.PersonelProjeleris)
                {
                    if (proje.TamamlanmaDurumu == true)
                    {
                        tamamlanmisProjeSayisi++;
                    }
                }
                personelTamamlanmisProjeSayisi[personel.PersonelBilgileriId] = tamamlanmisProjeSayisi;
            }
            var siraliPersonelListesi = personelTamamlanmisProjeSayisi.OrderByDescending(x => x.Value);  //tamamlanmış proje sayısına göre personelleri sırala
            var encokTamamlananPersonelId = siraliPersonelListesi.First().Key;  //en çok tamamlanan sayısına sahip personel
            var encokTamamlananPersonel = db.PersonelBilgileris.FirstOrDefault(p => p.PersonelBilgileriId == encokTamamlananPersonelId);
            ViewBag.EnCokTamamlayanPersonelBilgisi = encokTamamlananPersonel.AdSoyad;
            int enCokProjeTamamlayanPersonelProjeSayisi = personelTamamlanmisProjeSayisi[encokTamamlananPersonelId];
            ViewBag.EnCokProjeTamamlayanPersonelProjeSayisi = enCokProjeTamamlayanPersonelProjeSayisi;


            return View();
        }
    }
}