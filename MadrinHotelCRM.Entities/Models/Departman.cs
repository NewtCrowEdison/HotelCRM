using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    public class Departman
    {
        public int DepartmanId { get; set; }
        public string DepartmanAdi { get; set; }

        // Navigation Property
        public ICollection<Personel> Personeller { get; set; } 
    }
}
