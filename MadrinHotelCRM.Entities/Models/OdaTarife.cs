using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    public class OdaTarife
    {
        public int OdaId { get; set; }
        public int TarifeId { get; set; }
        public Oda Oda { get; set; }
        public Tarife Tarife { get; set; }
    }

}
