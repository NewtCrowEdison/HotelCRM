using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class MusteriEkleViewModel
    {
        public MusteriDTO YeniMusteri { get; set; } = new MusteriDTO();
        public List<MusteriDTO> MusteriListesi { get; set; } = new();
        public int? GuncellenecekMusteriId { get; set; } // opsiyonel: güncelleme modunda kullanılır
    }
}
