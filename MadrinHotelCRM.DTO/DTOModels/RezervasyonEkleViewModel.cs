using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class RezervasyonEkleViewModel
    {
        public RezervasyonDTO YeniRezervasyon { get; set; }
        public List<RezervasyonDTO> RezervasyonListesi { get; set; }
        public List<OdaDTO> OdaListesi { get; set; }
        public List<TarifeDTO> TarifeListesi { get; set; }
        public List<MusteriDTO> MusteriListesi { get; set; }
        public int? GuncellenecekRezervasyonId { get; set; }

        public List<EkPaketDTO> EkPaketListesi { get; set; } = new List<EkPaketDTO>();
        public List<FaturaDTO> FaturaListesi { get; set; } = new();

        // Tarihler:
        public DateTime? FiltreBaslangic { get; set; }
        public DateTime? FiltreBitis { get; set; }
    }
}
