using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.Entities.Models
{
    public class Oda
    {
        public int Id { get; set; }
        public int OdaTipiId { get; set; }
        public string OdaNumarasi { get; set; }
        public OdaDurum Durum { get; set; }
        public OdaTipi OdaTipi { get; set; }
        public ICollection<OdaTarife> OdaTarifeleri { get; set; }
    }
}
