using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    internal class EkPaket
    {
        public int PaketId { get; set; }

        [Required, MaxLength(100)]
        public string PaketAdi { get; set; }
        public string PaketAciklama { get; set; }
        public decimal Fiyat { get; set; }
        public DateTime? OlusturmaTarihi { get; set; }
        //public ICollection<RezervasyonPaket> RezervasyonPaketler { get; set; }
    }
}
