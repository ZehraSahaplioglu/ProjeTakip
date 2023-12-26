using PROJETAKIP.Models.Personel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROJETAKIP.Models.ProjeTakip
{
    public class PersonelProjeleri
    {
        public PersonelProjeleri() 
        {
            this.PersonelBilgileris = new HashSet<PersonelBilgileri>();
        }


        [Key]
        public int PersonelProjeId { get; set; }

        [DisplayName("Proje Başlığı")]
        [StringLength(50, ErrorMessage = "Max uzunluk 50 karakterden fazla olamaz!")]
        public string ProjeBaslik {  get; set; }

        [DisplayName("Proje Açıklaması")]
        public string ProjeAciklama { get; set; }

        [DisplayName("Proje Oluşturma Tarihi")]
        public DateTime OlusturmaTarihi { get; set; }

        [DisplayName("Öncelik Durumu")]
        [StringLength(25, ErrorMessage = "Max uzunluk 25 karakterden fazla olamaz!")]
        public string OncelikDurumu { get; set; }

        [DisplayName("Proje Tamamlanma Oranı")]
        public int  TamamlanmaOrani { get; set; }

        [DisplayName("Projenin Tamamlanma Tarihi")]
        public DateTime? TamamlanmaTarihi { get; set; }

        [DisplayName("Projenin Tamamlanma Durumu")]
        public bool TamamlanmaDurumu { get; set; }

        public virtual ICollection<PersonelBilgileri> PersonelBilgileris { get; set; }
    }
}