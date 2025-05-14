using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.DTO.DTOModels
{
    internal class RezervasyonDTO
    {
        public int RezervasyonId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int MusteriId { get; set; }
        public int OdaTipiId { get; set; }
        public int TarifeId { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public RezervasyonDurum Durum { get; set; }
        public DateTime OtelGiris { get; set; }
        public DateTime OtelCikis { get; set; }
        public DateTime? IptalTarihi { get; set; }
        public string IptalNedeni { get; set; }
    }
}
