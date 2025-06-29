﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Models
{
    public class OdaTipi
    {
        [Key]
        public int OdaTipiId { get; set; }
        public string OdaTurAd { get; set; }
        public int Kapasite { get; set; }
        public decimal Fiyat { get; set; }
        public string OdaAciklama { get; set; }
        public ICollection<Oda> Odalar { get; set; }
    }
}
