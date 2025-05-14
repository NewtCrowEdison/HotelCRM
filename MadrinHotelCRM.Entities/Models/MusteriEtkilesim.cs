using MadrinHotelCRM.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    public class MusteriEtkilesim
    {
        public int Id { get; set; }
        public DateTime EtkilesimTarihi { get; set; }
        public KanalTipi Kanal { get; set; }
        public string Notlar { get; set; }
        public int MusteriID { get; set; }
        public ICollection<Musteri> Musteriler { get; set; }
        public int PersonelId { get; set; }
        public Personel Personeller { get; set; }
    }
}
