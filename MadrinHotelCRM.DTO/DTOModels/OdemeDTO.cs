using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    internal class OdemeDTO
    {
        public int OdemeId { get; set; }
        public int FaturaId { get; set; }
        public DateTime OdemeTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
        public string OdemeYontemi { get; set; }
        public int IslemId { get; set; }
    }
}
