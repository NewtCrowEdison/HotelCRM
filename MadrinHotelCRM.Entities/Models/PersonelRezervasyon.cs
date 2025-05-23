using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.Entities.Models
{
    public class PersonelRezervasyon
    {
        public int Id { get; set; }
        public int PersonelId { get; set; }
        public int RezervasyonId { get; set; }
        public DateTime? CheckInTarihi { get; set; }
        public DateTime? CheckOutTarihi { get; set; }
        public RezervasyonDurum Durum { get; set; } // "Onaylandı", "Beklemede", "İptal" vb.
        public string? Notlar { get; set; }
        public virtual Personel Personel { get; set; }
        public virtual Rezervasyon Rezervasyon { get; set; }
    }
}
