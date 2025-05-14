using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    public class MusteriEtiket
    {
        public int MusteriID { get; set; }
        public int EtiketID { get; set; }
        public Musteri Musteri { get; set; }
        public Etiket Etiket { get; set; }
    }
}
