using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    internal class Etiket
    {
        public int Id { get; set; }
        public string EtiketAdi { get; set; }
        public ICollection<MusteriEtiket> MusteriEtiketleri { get; set; }
    }
}
