using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.Entities.Models
{
    public class Oda
    {
        [Key]
        public int OdaId { get; set; }
        public int OdaTipiId { get; set; }
        public string OdaNumarasi { get; set; }
        public OdaDurum Durum { get; set; }
        public OdaTipi OdaTipi { get; set; }
        public ICollection<OdaTarife> OdaTarifeleri { get; set; }
    }
}
