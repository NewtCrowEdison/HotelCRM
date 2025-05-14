using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.DTO.DTOModels
{
    internal class FaturaDTO
    {
        public int FaturaId { get; set; }
        public int RezervasyonId { get; set; }
        public decimal ToplamTutar { get; set; }
        public FaturaDurum Durum { get; set; }
        public DateTime FaturaOlusturmaTarihi { get; set; }

    }
}
