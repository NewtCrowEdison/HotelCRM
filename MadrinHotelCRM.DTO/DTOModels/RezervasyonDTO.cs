using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class RezervasyonDTO
    {
        public int RezervasyonId { get; set; }

        // Ekle: OdaId
        public int OdaId { get; set; }

        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }

        public int MusteriId { get; set; }
        public int TarifeId { get; set; }

        public DateTime OlusturmaTarihi { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RezervasyonDurum Durum { get; set; }

        public DateTime? IptalTarihi { get; set; }
        public string? IptalNedeni { get; set; }

        // Navigation
        public MusteriDTO? Musteri { get; set; }

        public int YetiskinSayisi { get; set; }
        public int CocukSayisi { get; set; }
    }
}
