//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;
//using MadrinHotelCRM.Entities.Enums;
//using Microsoft.AspNetCore.Mvc.ModelBinding;

//namespace MadrinHotelCRM.DTO.DTOModels
//{
//    public class OdaDTO
//    {
//        public int OdaId { get; set; }
//        public int OdaTipiId { get; set; }
//        public string OdaNumarasi { get; set; }
//        public OdaDurum Durum { get; set; }

//        //bunlar eklenecek

//        public string GorselUrl { get; set; }
//        public List<string> FotografGaleriListesi { get; set; } = new();
//        public string Ozellikler { get; set; } 

//        //  Oda bilgileri
//        public int OdaBoyutu { get; set; } // m² cinsinden
//        public int YatakSayisi { get; set; }
//        public string OdaAdi { get; set; } = string.Empty;

//        // NAV PROP
//        [BindNever]               // MVC binder burayı es geçsin istiyorum
//        [JsonIgnore]              // API’ye JSON’da da gelmesin
//        public OdaTipiDTO? OdaTipi { get; set; }
//        public string OdaTipiAdi => OdaTipi?.OdaTurAd ?? "Tanımsız";
//    }

//}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MadrinHotelCRM.Entities.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class OdaDTO
    {
        public int OdaId { get; set; }

        [Required]
        public int OdaTipiId { get; set; }

        [Required]
        public string OdaNumarasi { get; set; } = string.Empty;

        [Required]
        public OdaDurum Durum { get; set; }

        // Mevcut (kaydedilmiş) görsel URL’si
        public string? GorselUrl { get; set; }

        // Yeni yüklenen görseli Base64 olarak alacak alan
        // JSON içinde "data:image/png;base64,AAA..." formatında gönderilecek
        public string? GorselBase64 { get; set; }

        // Fotoğraf galeri URL listesi (varsa)
        public List<string> FotografGaleriListesi { get; set; } = new();

        // Eğer birden fazla yeni fotoğraf Base64 eklemek istersen:
        public List<string>? FotografGaleriBase64 { get; set; }

        // Düz metin özellikler
        public string? Ozellikler { get; set; }

        // Opsiyonel oda ek bilgileri
        public int? OdaBoyutu { get; set; }
        public int? YatakSayisi { get; set; }
        public string? OdaAdi { get; set; }

        // Navigation property, UI/MVC binder ve JSON’dan gizle
        [BindNever]
        [JsonIgnore]
        public OdaTipiDTO? OdaTipi { get; set; }

        public string? OdaTipiAdi { get; set; } //  set edilebilir

    }
}

