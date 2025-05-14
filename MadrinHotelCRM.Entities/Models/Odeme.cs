using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.Entities.Models
{
    public class Odeme
    {
        public int OdemeId { get; set; }
        public int FaturaId { get; set; }
        public Fatura Fatura { get; set; }
        public DateTime OdemeTarihi { get; set; }
        public decimal ToplamTutar { get; set; }
        public OdemeYontemi OdemeYontemi { get; set; }
        public int IslemId { get; set; }
        
    }
}
  