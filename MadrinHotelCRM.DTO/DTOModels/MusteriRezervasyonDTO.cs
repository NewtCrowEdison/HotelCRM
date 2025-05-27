using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class MusteriRezervasyonDTO
    {
        public int RezervasyonId { get; set; }
        public int MusteriId { get; set; }
        public int PersonelId { get; set; }
        public bool GirisYaptiMi { get; set; }
    }
}
