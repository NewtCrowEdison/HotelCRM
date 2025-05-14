using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.Entities.Models
{
    public class GeriBildirim
    {
        public int GeriBildirimId { get; set; }
        public int MusteriId { get; set; }
        public Musteri Musteri { get; set; }
        public DateTime Tarih { get; set; }
        public string Konu { get; set; }
        public string Mesaj { get; set; }
        public bool Durum { get; set; }
        
    }
  
}