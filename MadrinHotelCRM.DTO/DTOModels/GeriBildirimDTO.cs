using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    internal class GeriBildirimDTO
    {
        public int GeriBildirimId { get; set; }
        public int MusteriId { get; set; }
        public DateTime Tarih { get; set; }
        [MaxLength(100)]
        public string Konu { get; set; }
        public string Mesaj { get; set; }
        public bool Durum { get; set; }
    }
}
