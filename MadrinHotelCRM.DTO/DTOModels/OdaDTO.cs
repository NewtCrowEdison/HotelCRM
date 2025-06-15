using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MadrinHotelCRM.DTO.DTOModels
{
    public class OdaDTO
    {
        public int OdaId { get; set; }
        public int OdaTipiId { get; set; }
        public string OdaNumarasi { get; set; }
        public OdaDurum Durum { get; set; }

        //bunlar eklenecek

        //public string GorselUrl { get; set; }
        //public List<string> FotografGaleriListesi { get; set; } = new();
        //public List<string> Ozellikler { get; set; } = new();
        ////  Oda bilgileri
        //public int OdaBoyutu { get; set; } // m² cinsinden
        //public int YatakSayisi { get; set; }
        //public string OdaAdi { get; set; } = string.Empty;

        // NAV PROP
        [BindNever]               // MVC binder burayı es geçsin istiyorum
        [JsonIgnore]              // API’ye JSON’da da gelmesin
        public OdaTipiDTO? OdaTipi { get; set; }
        public string OdaTipiAdi => OdaTipi?.OdaTurAd ?? "Tanımsız";
    }

}
