using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Entities.Enums;


namespace MadrinHotelCRM.DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<AppUser, IdentityRole<string>, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<EkPaket> EkPaketler { get; set; }
        public DbSet<Etiket> Etiketler { get; set; }
        public DbSet<Fatura> Faturalar { get; set; }
        public DbSet<GenelTakip> GenelTakipler { get; set; }
        public DbSet<GeriBildirim> GeriBildirimler { get; set; }
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<MusteriEtiket> MusteriEtiketler { get; set; }
        public DbSet<MusteriEtkilesim> MusteriEtkilesimler { get; set; }
        public DbSet<Oda> Odalar { get; set; }
        public DbSet<OdaTarife> OdaTarifeleri { get; set; }
        public DbSet<OdaTipi> OdaTipleri { get; set; }
        public DbSet<Odeme> Odemeler { get; set; }
        public DbSet<Personel> Personeller { get; set; }
        public DbSet<Rezervasyon> Rezervasyonlar { get; set; }
        public DbSet<RezervasyonPaket> RezervasyonPaketler { get; set; }
        public DbSet<SistemLog> SistemLoglar { get; set; }
        public DbSet<Tarife> Tarifeler { get; set; }
        public DbSet<PersonelRezervasyon> PersonelRezervasyon { get; set; }
        public DbSet<Departman> Departmanlar { get; set; }
        public DbSet<MusteriRezervasyon> MusteriRezervasyonlar { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var adminRole = new IdentityRole<string>
            {
                Id = "1",
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            var personelRole = new IdentityRole<string>
            {
                Id = "2",
                Name = "Personel",
                NormalizedName = "PERSONEL"
            };

            modelBuilder.Entity<IdentityRole<string>>().HasData(adminRole, personelRole);

            // Composite Key Tanımlamaları
            modelBuilder.Entity<MusteriEtiket>()
                .HasKey(me => new { me.MusteriID, me.EtiketID });

            modelBuilder.Entity<OdaTarife>()
                .HasKey(ot => new { ot.OdaId, ot.TarifeId });

            modelBuilder.Entity<RezervasyonPaket>()
                .HasKey(rp => new { rp.RezervasyonId, rp.PaketId });

            modelBuilder.Entity<EkPaket>(entity =>
           {
               entity.HasKey(e => e.EkPaketId);

               entity.HasMany(e => e.RezervasyonPaketler)
                .WithOne(rp => rp.Paket)
                .HasForeignKey(rp => rp.PaketId)
                .OnDelete(DeleteBehavior.Cascade);
           });

            modelBuilder.Entity<Etiket>(entity =>
            {
                entity.HasKey(e => e.EtiketId);

                entity.HasMany(e => e.MusteriEtiketleri)
                .WithOne(me => me.Etiket)
                .HasForeignKey(me => me.EtiketID)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Fatura>(entity =>
            {
                entity.HasKey(f => f.FaturaId);

                entity.HasOne(f => f.Rezervasyon)
                .WithMany(r => r.Faturalar)
                .HasForeignKey(f => f.RezervasyonId)
               .OnDelete(DeleteBehavior.Cascade);


                entity.HasMany(f => f.Odemeler)
                .WithOne(o => o.Fatura)
                .HasForeignKey(o => o.FaturaId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<GenelTakip>(entity =>
            {
                entity.HasKey(g => g.GenelTakipId);

                entity.HasOne(g => g.Personel)
                .WithMany(p => p.GenelTakipler)
                .HasForeignKey(g => g.PersonelId)
                .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<GeriBildirim>(entity =>
            {
                entity.HasKey(g => g.GeriBildirimId);

                entity.HasOne(g => g.Musteri)
                .WithMany(m => m.GeriBildirimler)
                .HasForeignKey(g => g.MusteriId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Musteri>(entity =>
            {
                entity.HasKey(m => m.MusteriId);

                entity.HasMany(m => m.MusteriEtiketleri)
                 .WithOne(me => me.Musteri)
                 .HasForeignKey(me => me.MusteriID)
                 .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(m => m.MusteriEtkilesim)
                 .WithOne(me => me.Musteriler)
                 .HasForeignKey(me => me.MusteriEtkilesimId)
                 .OnDelete(DeleteBehavior.NoAction);




                entity.HasMany(m => m.Rezervasyonlar)
                 .WithOne(r => r.Musteri)
                 .HasForeignKey(r => r.MusteriId)
                 .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(m => m.GeriBildirimler)
                 .WithOne(gb => gb.Musteri)
                 .HasForeignKey(gb => gb.MusteriId)
                  .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<MusteriEtiket>(entity =>
            {

                entity.HasOne(me => me.Musteri)
                .WithMany(m => m.MusteriEtiketleri)
                .HasForeignKey(me => me.MusteriID)
                .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(me => me.Etiket)
                .WithMany(e => e.MusteriEtiketleri)
                .HasForeignKey(me => me.EtiketID)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<MusteriEtkilesim>(entity =>
                {

                    entity.HasOne(me => me.Personeller)
                    .WithMany(p => p.Etkilesimler)
                    .HasForeignKey(me => me.PersonelId)
                    .OnDelete(DeleteBehavior.Restrict);


                    entity.HasOne(me => me.Musteriler)
                      .WithMany(m => m.MusteriEtkilesim)
                      .HasForeignKey(me => me.MusteriEtkilesimId)
                      .OnDelete(DeleteBehavior.NoAction);

                });

            modelBuilder.Entity<Oda>(entity =>
                {

                    entity.HasOne(o => o.OdaTipi)
                        .WithMany(ot => ot.Odalar)
                   .HasForeignKey(o => o.OdaTipiId)
                   .OnDelete(DeleteBehavior.Restrict);


                    entity.HasMany(o => o.OdaTarifeleri)
                    .WithOne(ot => ot.Oda)
                    .HasForeignKey(ot => ot.OdaId)
                    .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity<OdaTarife>(entity =>
            {
                entity.HasKey(ot => new { ot.OdaId, ot.TarifeId });

                entity.HasOne(ot => ot.Oda)
                    .WithMany(o => o.OdaTarifeleri)
                    .HasForeignKey(ot => ot.OdaId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ot => ot.Tarife)
                    .WithMany(t => t.OdaTarifeleri)
                    .HasForeignKey(ot => ot.TarifeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<OdaTipi>(entity =>
            {
                entity.HasKey(ot => ot.OdaTipiId);

                entity.HasMany(ot => ot.Odalar)
                    .WithOne(o => o.OdaTipi)
                    .HasForeignKey(o => o.OdaTipiId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Odeme>(entity =>
            {
                entity.HasKey(o => o.OdemeId);

                entity.HasOne(o => o.Fatura)
                    .WithMany(f => f.Odemeler)
                    .HasForeignKey(o => o.FaturaId)
                    .OnDelete(DeleteBehavior.Cascade);

            });

            modelBuilder.Entity<Personel>(entity =>
            {
                entity.HasKey(p => p.PersonelId);

                entity.HasMany(p => p.Etkilesimler)
                    .WithOne(me => me.Personeller)
                    .HasForeignKey(me => me.PersonelId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(p => p.GenelTakipler)
                    .WithOne(gt => gt.Personel)
                    .HasForeignKey(gt => gt.PersonelId)
                    .OnDelete(DeleteBehavior.Restrict);
            });




            modelBuilder.Entity<Rezervasyon>(entity =>
            {
                entity.HasKey(r => r.RezervasyonId);


                entity.HasOne(r => r.Musteri)
                 .WithMany(m => m.Rezervasyonlar)
                 .HasForeignKey(r => r.MusteriId)
                 .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(r => r.OdaTipi)
                 .WithMany()
                 .HasForeignKey(r => r.OdaTipiId)
                 .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Tarife)
                .WithMany()
                .HasForeignKey(r => r.TarifeId)
                .OnDelete(DeleteBehavior.Restrict);


                entity.HasMany(r => r.RezervasyonPaketler)
                 .WithOne(rp => rp.Rezervasyon)
                 .HasForeignKey(rp => rp.RezervasyonId)
                 .OnDelete(DeleteBehavior.Cascade);


                entity.HasMany(r => r.Faturalar)
                 .WithOne(f => f.Rezervasyon)
                 .HasForeignKey(f => f.RezervasyonId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RezervasyonPaket>(entity =>
            {
                entity.HasKey(rp => new { rp.RezervasyonId, rp.PaketId });


                entity.HasOne(rp => rp.Rezervasyon)
                 .WithMany(r => r.RezervasyonPaketler)
                 .HasForeignKey(rp => rp.RezervasyonId)
                 .OnDelete(DeleteBehavior.Cascade);


                entity.HasOne(rp => rp.Paket)
                .WithMany(p => p.RezervasyonPaketler)
                .HasForeignKey(rp => rp.PaketId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Tarife>(entity =>
            {
                entity.HasKey(t => t.TarifeId);

                entity.HasMany(t => t.OdaTarifeleri)
                  .WithOne(ot => ot.Tarife)
                  .HasForeignKey(ot => ot.TarifeId)
                  .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<PersonelRezervasyon>(entity =>
            {
                // Birincil anahtar
                entity.HasKey(pr => pr.Id);

                // Personel ↔ PersonelRezervasyon (1 – n)
                entity.HasOne(pr => pr.Personel)
                      .WithMany(p => p.PersonelRezervasyonlar)
                      .HasForeignKey(pr => pr.PersonelId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Rezervasyon ↔ PersonelRezervasyon (1 – n)
                entity.HasOne(pr => pr.Rezervasyon)
                      .WithMany(r => r.PersonelRezervasyonlar)
                      .HasForeignKey(pr => pr.RezervasyonId)
                      .OnDelete(DeleteBehavior.Cascade);

            });

            //Departman Konfigürasyonu
            modelBuilder.Entity<Departman>(entity =>
            {
                entity.HasKey(d => d.DepartmanId);

                entity.Property(d => d.DepartmanAdi)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasMany(d => d.Personeller)
                      .WithOne(p => p.Departman)
                      .HasForeignKey(p => p.DepartmanId)
                      .OnDelete(DeleteBehavior.Restrict); // Departman silinse bile personeller kalır
            });

            //MusteriRezervasyon Konfigürasyonu
            modelBuilder.Entity<MusteriRezervasyon>(entity =>
            {
                entity.HasKey(mr => new { mr.MusteriId, mr.RezervasyonId }); // Composite key

                entity.Property(mr => mr.GirisYaptiMi)
                      .HasDefaultValue(false);

                entity.HasOne(mr => mr.Musteri)
                      .WithMany()
                      .HasForeignKey(mr => mr.MusteriId)
                      .OnDelete(DeleteBehavior.Restrict); // Müşteri silinse rezervasyonlar silinmesin .EF ve veritabanı kontrol eder, ilişkili veri varsa silinmesine izin vermez

                entity.HasOne(mr => mr.Rezervasyon)
                      .WithMany()
                      .HasForeignKey(mr => mr.RezervasyonId)
                      .OnDelete(DeleteBehavior.Restrict); // Rezervasyon silinse bağlantı kaybı olmasın

                entity.HasOne(mr => mr.Personel)
                      .WithMany()
                      .HasForeignKey(mr => mr.PersonelId)
                      .OnDelete(DeleteBehavior.Restrict); // Personel silinse loglar gitmesin
            });

            modelBuilder.Entity<EkPaket>().HasData(
                new EkPaket
                {
                    EkPaketId = 1,
                    PaketAdi = "Spa ve Masaj Paketi",
                    PaketAciklama = "Misafirlerimize özel 1 saatlik spa ve masaj hizmeti.",
                    Fiyat = 750.00m,
                    OlusturmaTarihi = new DateTime(2025, 5, 1)
                },
                new EkPaket
                {
                    EkPaketId = 2,
                    PaketAdi = "Havalimanı Transferi",
                    PaketAciklama = "Geliş ve dönüş için VIP transfer hizmeti.",
                    Fiyat = 1200.00m,
                    OlusturmaTarihi = new DateTime(2025, 5, 1)
                },
                new EkPaket
                {
                    EkPaketId = 3,
                    PaketAdi = "Romantik Akşam Yemeği",
                    PaketAciklama = "Deniz manzaralı restoranda 2 kişilik özel akşam yemeği.",
                    Fiyat = 950.00m,
                    OlusturmaTarihi = new DateTime(2025, 5, 1)
                },
                new EkPaket
                {
                    EkPaketId = 4,
                    PaketAdi = "Çocuk Bakımı Hizmeti",
                    PaketAciklama = "Eğitimli personel tarafından saatlik çocuk bakımı.",
                    Fiyat = 300.00m,
                    OlusturmaTarihi = new DateTime(2025, 5, 1)
                },
                new EkPaket
                {
                    EkPaketId = 5,
                    PaketAdi = "Yarım Gün Tekne Turu",
                    PaketAciklama = "Bodrum koylarını keşfedeceğiniz 4 saatlik tekne turu.",
                    Fiyat = 1800.00m,
                    OlusturmaTarihi = new DateTime(2025, 5, 1)
                });

            modelBuilder.Entity<Tarife>().HasData(
                new Tarife
                {
                    TarifeId = 1,
                    TarifeAdi = "Yaz Sezonu Standart Tarife",
                    Fiyat = 2800.00m,
                    BaslangicTarihi = new DateTime(2025, 6, 1),
                    BitisTarihi = new DateTime(2025, 8, 31),
                    IndirimOrani = 0
                },
                new Tarife
                {
                    TarifeId = 2,
                    TarifeAdi = "Kış Kampanyası",
                    Fiyat = 1800.00m,
                    BaslangicTarihi = new DateTime(2025, 1, 1),
                    BitisTarihi = new DateTime(2025, 2, 28),
                    IndirimOrani = 20
                },
                new Tarife
                {
                    TarifeId = 3,
                    TarifeAdi = "Sevgililer Günü Paketi",
                    Fiyat = 2200.00m,
                    BaslangicTarihi = new DateTime(2025, 2, 10),
                    BitisTarihi = new DateTime(2025, 2, 16),
                    IndirimOrani = 15
                },
                new Tarife
                {
                    TarifeId = 4,
                    TarifeAdi = "Bayram Özel Tarife",
                    Fiyat = 3200.00m,
                    BaslangicTarihi = new DateTime(2025, 4, 1),
                    BitisTarihi = new DateTime(2025, 4, 15),
                    IndirimOrani = 10
                },
                new Tarife
                {
                    TarifeId = 5,
                    TarifeAdi = "Sonbahar Erken Rezervasyon",
                    Fiyat = 2000.00m,
                    BaslangicTarihi = new DateTime(2025, 10, 1),
                    BitisTarihi = new DateTime(2025, 11, 30),
                    IndirimOrani = 25
                });

            modelBuilder.Entity<OdaTipi>().HasData(
                new OdaTipi
                {
                    OdaTipiId = 1,
                    OdaTurAd = "Standart Tek Kişilik",
                    Kapasite = 1,
                    Fiyat = 1200.00m,
                    OdaAciklama = "Konforlu tek kişilik yatak, şehir manzaralı, mini bar ve ücretsiz Wi-Fi."
                },
                new OdaTipi
                {
                    OdaTipiId = 2,
                    OdaTurAd = "Standart Çift Kişilik",
                    Kapasite = 2,
                    Fiyat = 1800.00m,
                    OdaAciklama = "Geniş çift kişilik yatak, klima, balkon ve televizyon."
                },
                new OdaTipi
                {
                    OdaTipiId = 3,
                    OdaTurAd = "Deluxe Oda",
                    Kapasite = 2,
                    Fiyat = 2500.00m,
                    OdaAciklama = "Deniz manzaralı, king size yatak, özel jakuzi, kahve makinesi."
                },
                new OdaTipi
                {
                    OdaTipiId = 4,
                    OdaTurAd = "Aile Odası",
                    Kapasite = 4,
                    Fiyat = 3200.00m,
                    OdaAciklama = "İki ayrı oda, geniş salon, çocuk yatağı, mutfak bölümü."
                },
                new OdaTipi
                {
                    OdaTipiId = 5,
                    OdaTurAd = "Balayı Süiti",
                    Kapasite = 2,
                    Fiyat = 4000.00m,
                    OdaAciklama = "Romantik dekorasyon, jakuzili banyo, özel teras, sürpriz ikramlar."
                });
            modelBuilder.Entity<Oda>().HasData(
               new Oda
               {
                   OdaId = 1,
                   OdaNumarasi = "101",
                   OdaTipiId = 1, // Standart Tek Kişilik
                   Durum = (OdaDurum)1 // Bos
               },
               new Oda
                 {
                     OdaId = 2,
                     OdaNumarasi = "102",
                     OdaTipiId = 2, // Standart Çift Kişilik
                     Durum = (OdaDurum)2 // Dolu
                 },
              new Oda
              {
                  OdaId = 3,
                  OdaNumarasi = "201",
                  OdaTipiId = 3, // Deluxe Oda
                  Durum = (OdaDurum)4 // Temizlikte
              },
              new Oda
              {
                  OdaId = 4,
                  OdaNumarasi = "301",
                  OdaTipiId = 4, // Aile Odası
                  Durum = (OdaDurum)5 // Rezervasyonlu
              },
             new Oda
             {
                 OdaId = 5,
                 OdaNumarasi = "401",
                 OdaTipiId = 5, // Balayı Süiti
                 Durum = (OdaDurum)1 // Bos
             });





        }
    }
}

