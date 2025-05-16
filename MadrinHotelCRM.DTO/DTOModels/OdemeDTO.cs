using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.DTO.DTOModels
{
    internal class OdemeDTO
    {
        public int OdemeId { get; set; }
        public int FaturaId { get; set; }
        public DateTime OdemeTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
        public OdemeYontemi OdemeYontemi { get; set; }  // Enum olarak
        public int? IslemId { get; set; }  // Islem referansı nullable yapılabilir
    }
}
