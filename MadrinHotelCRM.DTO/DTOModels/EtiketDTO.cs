using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    internal class EtiketDTO
    {
        public int EtiketId { get; set; }
        [Required]
        public string EtiketAdi { get; set; }
    }
}
