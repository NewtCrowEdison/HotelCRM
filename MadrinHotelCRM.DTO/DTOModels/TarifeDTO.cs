using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    internal class TarifeDTO
    {
        public int TarifeId { get; set; }
        public string TarifeAdi { get; set; }
        public decimal Fiyat { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int IndirimOrani { get; set; }
    }
}
