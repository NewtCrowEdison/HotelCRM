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

        // NAV PROP
        [BindNever]               // MVC binder burayı es geçsin istiyorum
        [JsonIgnore]              // API’ye JSON’da da gelmesin
        public OdaTipiDTO? OdaTipi { get; set; }
        public string OdaTipiAdi => OdaTipi?.OdaTurAd ?? "Tanımsız";
    }

}
