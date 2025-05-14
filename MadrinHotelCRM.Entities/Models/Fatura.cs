using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.Entities.Models
{
    public class Fatura
    {
        public int FaturaId { get; set; }
        public int RezervasyonId { get; set; }
        public Rezervasyon Rezervasyon { get; set; }
        public decimal ToplamTutar { get; set; }
        public FaturaDurum Durum{ get; set; }
        public DateTime FaturaOlusturmaTarihi { get; set; }

        public ICollection<Odeme> Odemeler { get; set; }
    }
}
