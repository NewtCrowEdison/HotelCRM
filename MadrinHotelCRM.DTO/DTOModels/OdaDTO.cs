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
        [JsonConverter(typeof(JsonStringEnumConverter))]
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

