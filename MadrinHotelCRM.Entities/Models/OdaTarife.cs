using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    public class OdaTarife
    {
        public int Id { get; set; }
        public int OdaId { get; set; }
        public int TarifeId { get; set; }
        public decimal Fiyat { get; set; }

        // Navigation Property
        public Oda Oda { get; set; }       // Oda ile bire bir ilişki 
        public Tarife Tarife { get; set; } // Tarife ile bire bir ilişki 
    }

}
