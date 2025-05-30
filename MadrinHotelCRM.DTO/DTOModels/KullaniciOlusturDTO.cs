using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{

    public class KullaniciOlusturDTO
    {
        [Required] public string Email { get; set; }
        [Required] public string Sifre { get; set; }
        [Required] public string Rol { get; set; }

        [Required] public string Ad { get; set; }
        [Required] public string Soyad { get; set; }
        [Required] public string Telefon { get; set; }

        public int? DepartmanId { get; set; }
        public bool YabanciUyrukluMu { get; set; }
        public string? PasaportNo { get; set; }
        public string? TcKimlik { get; set; }
    }

}