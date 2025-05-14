using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.DTO.DTOModels
{
    internal class OdaTipiDTO
    {
        public int OdaTipiId { get; set; }
        public string OdaTurAd { get; set; }
        public int Kapasite { get; set; }
        public decimal Fiyat { get; set; }
        public string OdaAciklama { get; set; }
    }
}
