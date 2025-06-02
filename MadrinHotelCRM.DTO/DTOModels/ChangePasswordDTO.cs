using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class ChangePasswordDTO
    {
        public string KullaniciId { get; set; }
        public string EskiSifre { get; set; }
        public string YeniSifre { get; set; }
        public string YeniSifreTekrar { get; set; }
    }
}
