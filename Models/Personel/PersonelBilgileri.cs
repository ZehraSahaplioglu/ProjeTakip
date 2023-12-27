using PROJETAKIP.Models.ProjeTakip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PROJETAKIP.Models.Personel
{
    public class PersonelBilgileri
    {
        public PersonelBilgileri()  // many-to-many ilişkisi için gerekli
        {
            this.PersonelProjeleris = new HashSet<PersonelProjeleri>();
        }

        [Key]
        public int PersonelBilgileriId { get; set; }

        [DisplayName("Personel Görseli")]
        public string PersonelGorseli { get; set; }

        [DisplayName("E-Posta Adresi")]  //label kısmında bu gözükür
        public string Eposta { get; set; }

        [DisplayName("Şifre")]
        [StringLength(15, ErrorMessage = "Max uzunluk 15 karakterden fazla olamaz!")]
        public string Sifre { get; set; }

        [DisplayName("Yetki")]
        [StringLength(25, ErrorMessage = "Max uzunluk 25 karakterden fazla olamaz!")]
        public string Yetki { get; set; }

        [DisplayName("Ad Soyad")]
        [StringLength(50, ErrorMessage = "Max uzunluk 50 karakterden fazla olamaz!")]
        public string AdSoyad { get; set; }

        [DisplayName("TC Kimlik Numarası")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Geçerli bir TC Kimlik Numarası giriniz.")]
        public string TCNO { get; set; }

        [DisplayName("Departman")]
        [StringLength(25, ErrorMessage = "Max uzunluk 25 karakterden fazla olamaz!")]
        public string Departman { get; set; }

        [DisplayName("Görev")]
        [StringLength(25, ErrorMessage = "Max uzunluk 25 karakterden fazla olamaz!")]
        public string Gorev { get; set; }

        [DisplayName("Açıklama")]
        public string PozisyonAciklama { get; set; }

        [DisplayName("Telefon Numarası")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string TelNo { get; set; }

        [DisplayName("Adres Bilgisi")]
        public string Adres { get; set; }

        [DisplayName("Medeni Hali")]
        [StringLength(15, ErrorMessage = "Max uzunluk 15 karakterden fazla olamaz!")]
        public string MedeniHal { get; set; }

        [DisplayName("Yakın Bilgisi")]
        [StringLength(25, ErrorMessage = "Max uzunluk 25 karakterden fazla olamaz!")]
        public string YakinBilgisi { get; set; }

        [DisplayName("Yakın TC Kimlik Numarası")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Geçerli bir TC Kimlik Numarası giriniz.")]
        public string YakinTC { get; set; }

        [DisplayName("Yakın Ad Soyad")]
        [StringLength(50, ErrorMessage = "Max uzunluk 50 karakterden fazla olamaz!")]
        public string YakinAdSoyad { get; set; }

        [DisplayName("Yakın Telefon Numarası")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Geçerli bir telefon numarası giriniz.")]
        public string YakinTel { get; set; }

        [DisplayName("Doğum Tarihi")]
        public DateTime DogumTarihi { get; set; }

        [DisplayName("İşe Giriş Tarihi")]
        public DateTime? IseGirisTarihi { get; set; }

        public virtual ICollection<PersonelProjeleri> PersonelProjeleris { get; set; }  //many-to-many ilişkisi için
    }
}