using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    public class Tarife
    {
        public int Id { get; set; }
        public string TarifeAdi { get; set; }
        public decimal Fiyat { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int IndirimOrani { get; set; }
        public ICollection<OdaTarife> OdaTarifeleri { get; set; }// OdaTarife ile bire çok ilişki
    }
}
