using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class IletisimFormViewModel
    {
        [Required]
        public string Ad { get; set; }

        [Required]
        public string Soyad { get; set; }

        [Required]
        public string Telefon { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string IletisimTuru { get; set; }  // Email / Telefon
        public string OtelSecimi { get; set; }
        public string TalepKonusu { get; set; }
        public string Mesaj { get; set; }
        public string AramaZamani { get; set; }

    }
}
