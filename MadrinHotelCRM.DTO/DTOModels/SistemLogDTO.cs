using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class SistemLogDTO
    {
        public int SistemLogId { get; set; }
        public DateTime ZamanDamgasi { get; set; }
        public LogSeviyesi LogSeviyesi { get; set; }
        public string Kaynak { get; set; }
        public string Mesaj { get; set; }
        public string? Istisna { get; set; }
        public string HttpYontemi { get; set; }
        public string Url { get; set; }
        public string LogJsonVerisi { get; set; }

    }
}
