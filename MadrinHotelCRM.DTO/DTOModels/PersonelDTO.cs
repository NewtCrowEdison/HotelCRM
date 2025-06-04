using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class PersonelDTO
    {
        public int PersonelId { get; set; }

        [Required, MaxLength(50)]
        public string Ad { get; set; }

        [Required, MaxLength(50)]
        public string Soyad { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(20), Phone]
        public string Telefon { get; set; }
        public bool YabanciUyrukluMu { get; set; }
        [MaxLength(20)]
        public string? PasaportNo { get; set; }
        [MaxLength(11)]
        public string? TcKimlik { get; set; }
        public int? DepartmanId { get; set; }
        public string? Password { get; set; }

        public string KullaniciId { get; set; }
        public string? PasswordHash { get; set; }

    }
}