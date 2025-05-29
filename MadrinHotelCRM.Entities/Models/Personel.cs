using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.Entities.Models
{
    public class Personel
    {
        [Key]
        public int PersonelId { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public int ?DepartmanId { get; set; } // Departman ile iliþki için
        public bool YabanciUyrukluMu { get; set; }
        [MaxLength(20)]
        public string? PasaportNo { get; set;}
        [MaxLength(11)]
        public string? TcKimlik{ get; set; }
        public ICollection<MusteriEtkilesim> Etkilesimler { get; set; }
        public ICollection<GenelTakip> GenelTakipler { get; set; }
        public ICollection<PersonelRezervasyon> PersonelRezervasyonlar { get; set; }
        public Departman? Departman { get; set; } //sadece id sini saklamayarak iliþkiyi doðrudan nesne olarakta kullanabilmek için 

    }
}