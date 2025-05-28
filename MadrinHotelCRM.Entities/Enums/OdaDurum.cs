using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MadrinHotelCRM.Entities.Enums
{
    public enum OdaDurum
    {
        /// <summary>
        /// daha da okunabilir hale gelmesi için display attribute ekledik
        /// </summary>
        [Display(Name = "Boþ")]
        Bos = 1,

        [Display(Name = "Dolu")]
        Dolu = 2,

        [Display(Name = "Bakýmda")]
        Bakýmda = 3,

        [Display(Name = "Temizlikte")]
        Temizlikte = 4,

        [Display(Name = "Rezerve Edildi")]
        Rezervasyonlu = 5,

        [Display(Name = "Opsiyonlu")]
        Opsionlu = 6
    }
  
}