using System.Collections.Generic;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class PersonelEkleViewModel
    {
        public KullaniciOlusturDTO Kullanici { get; set; }
        public List<PersonelDTO> PersonelListesi { get; set; }
        public int? GuncellenecekPersonelId { get; set; } // Güncelleme işlemimiz için  kim olduğunu anlamamızı sağlar
    }
}
