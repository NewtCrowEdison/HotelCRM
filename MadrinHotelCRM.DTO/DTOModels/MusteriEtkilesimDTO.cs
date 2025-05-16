using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class MusteriEtkilesimDTO
    {
        public int MusteriEtkilesimId { get; set; }
        public DateTime EtkilesimTarihi { get; set; }
        public KanalTipi Kanal { get; set; }
        public string Notlar { get; set; }
        public int MusteriID { get; set; }
        public int PersonelId { get; set; }

    }
}
