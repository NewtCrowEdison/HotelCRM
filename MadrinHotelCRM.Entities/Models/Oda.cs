using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    public class Oda
    {
        public int Id { get; set; }
        public int OdaTipiId { get; set; }
        public string OdaNumarasi { get; set; }
        public string Durum { get; set; }
        //navigation property
        public OdaTipi OdaTipi { get; set; }  // OdaTipi ile bire çok ilişki
        public ICollection<OdaTarife> OdaTarifeleri { get; set; } // OdaTarife ile bire çok ilişki 
    }
}
