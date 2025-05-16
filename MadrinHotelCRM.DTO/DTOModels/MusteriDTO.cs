using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class MusteriDTO
    {
        public int MusteriId { get; set; }
        [MaxLength(11)]
        public string TcNo { get; set; }
        [MaxLength(50)]
        public string Ad { get; set; }
        [MaxLength(50)]
        public string Soyad { get; set; }
        [MaxLength(20)]
        public string TelNo { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Email { get; set; }
        public Cinsiyet Cinsiyet { get; set; }
        public string Adres { get; set; }
    }
}
