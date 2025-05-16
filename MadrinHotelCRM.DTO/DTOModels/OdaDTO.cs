using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class OdaDTO
    {
        public int OdaId { get; set; }
        public int OdaTipiId { get; set; }
        public string OdaNumarasi { get; set; }
        public OdaDurum Durum { get; set; }  // Enum olarak kullanıldı
        public OdaTipiDTO OdaTipi { get; set; }  //odaTipiId odatipiDto referansı 
    }
}
