using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class GenelTakipDTO
    {
        public int GenelTakipId { get; set; }
        public DateTime YaratilmaTarihi { get; set; }
        public DateTime DegistirilmeTarihi { get; set; }
        public DateTime SilinmeTarihi { get; set; }
        public string IslemTipi { get; set; }
        public string TabloAdi { get; set; }
        public int KayitId { get; set; }
        public int PersonelId { get; set; }
        public string EskiVeriJson { get; set; }
        public string YeniVeriJson { get; set; }

    }
}
