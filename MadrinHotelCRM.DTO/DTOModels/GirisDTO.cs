using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class GirisDTO
    {
        public string Email { get; set; }
        public string Sifre { get; set; }
        public bool BeniHatirla { get; set; }
    }
}
