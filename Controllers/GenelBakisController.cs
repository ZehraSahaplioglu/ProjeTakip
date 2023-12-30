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

        public ActionResult GenelIstatistik()
        {
            var personeller = db.PersonelBilgileris.ToList();
            var personelProjeleri = db.PersonelProjeleris.ToList();
            var tamamlananProjeSayisi = new Dictionary<int, int>();
            var tamamlanmayanProjeSayisi = new Dictionary<int, int>();
            var toplamProjeSayisi = new Dictionary<int, int>();

            foreach ( var personel in personeller)
            {
                int tamamlananProje = 0;
                int tamamlanmayanProje = 0;
                int toplamProje = 0;

                foreach(var proje in personelProjeleri)
                {
                    if(proje.PersonelBilgileris.Contains(personel))
                    {
                        toplamProje++;

                        if (proje.TamamlanmaDurumu)
                        {
                            tamamlananProje++;
                        }
                        else
                        {
                            tamamlanmayanProje++;
                        }
                    }
                }

                tamamlananProjeSayisi[personel.PersonelBilgileriId] = tamamlananProje;
                tamamlanmayanProjeSayisi[personel.PersonelBilgileriId] = tamamlanmayanProje;
                toplamProjeSayisi[personel.PersonelBilgileriId] = toplamProje;
            }

            ViewBag.TamamlananProjeSayisi = tamamlananProjeSayisi;
            ViewBag.TamamlanmayanProjeSayisi = tamamlanmayanProjeSayisi;
            ViewBag.ToplamProjeSayisi = toplamProjeSayisi;

            int projeSayisi = db.PersonelProjeleris.Count();
            ViewBag.ProjeSayisi = projeSayisi;

            int personelSayisi = db.PersonelBilgileris.Count();
            ViewBag.PersonelSayisi = personelSayisi;

            int tamamlanmisProje = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == true).Count();
            ViewBag.TamamlanmisProje = tamamlanmisProje;

            int tamamlanmamisProje = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == false).Count();
            ViewBag.TamamlanmamisProje = tamamlanmamisProje;

            var tamamlanmayanYuksek = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == false && p.OncelikDurumu == "Yüksek Öncelikli").Count();
            ViewBag.TamamlanmayanYuksek = tamamlanmayanYuksek;

            var tamamlanmayanOrta = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == false && p.OncelikDurumu == "Orta Öncelikli").Count();
            ViewBag.TamamlanmayanOrta = tamamlanmayanOrta;

            var tamamlanmayanDusuk = db.PersonelProjeleris.Where(p => p.TamamlanmaDurumu == false && p.OncelikDurumu == "Düşük Öncelikli").Count();
            ViewBag.TamamlanmayanDusuk = tamamlanmayanDusuk;

            return View(personeller);
        }

    }
}