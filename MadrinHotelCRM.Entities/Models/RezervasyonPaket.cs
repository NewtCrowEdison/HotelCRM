using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    public class RezervasyonPaket
    {
        public int RezervasyonId { get; set; }
        public Rezervasyon Rezervasyon { get; set; }
        public int PaketId { get; set; }
        public EkPaket Paket { get; set; }
        public int Adet { get; set; }
    }
}
