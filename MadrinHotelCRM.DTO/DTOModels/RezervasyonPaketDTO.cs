using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    internal class RezervasyonPaketDTO
    {
        public int RezervasyonId { get; set; }
        public int PaketId { get; set; }
        public int Adet { get; set; }
    }
}
