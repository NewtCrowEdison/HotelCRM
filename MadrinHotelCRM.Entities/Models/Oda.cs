//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using MadrinHotelCRM.Entities.Enums;

//namespace MadrinHotelCRM.Entities.Models
//{
//    public class Oda
//    {
//        [Key]
//        public int OdaId { get; set; }
//        public int OdaTipiId { get; set; }
//        public string OdaNumarasi { get; set; }
//        public OdaDurum Durum { get; set; }
//        public OdaTipi OdaTipi { get; set; }
//        public ICollection<OdaTarife> OdaTarifeleri { get; set; }
//    }
//}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.Entities.Models
{
    public class Oda
    {
        [Key]
        public int OdaId { get; set; }

        public int OdaTipiId { get; set; }

        public string OdaNumarasi { get; set; }

        public OdaDurum Durum { get; set; }

        public OdaTipi OdaTipi { get; set; }

        public ICollection<OdaTarife> OdaTarifeleri { get; set; }
        public ICollection<Rezervasyon> Rezervasyonlar { get; set; }  

        public string? GorselUrl { get; set; }

        public string? OdaAdi { get; set; }

        public int? OdaBoyutu { get; set; }

        public int? YatakSayisi { get; set; }

        public string? Ozellikler { get; set; }

        public string? FotografGaleriListesiJson { get; set; }

        [NotMapped]
        public List<string> FotografGaleriListesi
        {
            get => string.IsNullOrEmpty(FotografGaleriListesiJson)
                ? new List<string>()
                : System.Text.Json.JsonSerializer.Deserialize<List<string>>(FotografGaleriListesiJson);
            set => FotografGaleriListesiJson = System.Text.Json.JsonSerializer.Serialize(value);
        }
    }
}

