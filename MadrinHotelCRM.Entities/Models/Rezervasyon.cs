using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;

namespace MadrinHotelCRM.Entities.Models
{
    public class Rezervasyon
    {
        public int RezervasyonId { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public int MusteriId { get; set; }
        public Musteri Musteri { get; set; }

        // MÜŞTERİ ODA  SEÇMEZ ODA TİPİ SEÇER ODA TİPİ SEÇİLDİKÇE ODALARDAN SEÇİLEN TİPTEKİ ODA SAYISI EKSİLTİLİR
        public int OdaTipiId { get; set; }
        public OdaTipi OdaTipi { get; set; }
        public int TarifeId { get; set; }
        public Tarife Tarife { get; set; }
        public DateTime OlusturmaTarihi { get; set; }
        public RezervasyonDurum Durum { get; set; }
        public DateTime OtelGiris { get; set; }
        public DateTime OtelCıkıs { get; set; }
        public DateTime? IptalTarihi { get; set; }
        public string? IptalNedeni { get; set; }

        public ICollection<RezervasyonPaket> RezervasyonPaketler { get; set; }
        public ICollection<Fatura> Faturalar { get; set; }

    }
}
