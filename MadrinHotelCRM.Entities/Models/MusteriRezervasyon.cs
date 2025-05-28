using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    public class MusteriRezervasyon
    {
        public int RezervasyonId { get; set; }
        public int MusteriId { get; set; }
        public int PersonelId { get; set; }
        public bool GirisYaptiMi { get; set; }

        // Navigation Properties
        public Rezervasyon Rezervasyon { get; set; }
        public Musteri Musteri { get; set; }
        public Personel Personel { get; set; }
    }
}
