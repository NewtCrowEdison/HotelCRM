using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.DataAccess.Context
{
    public class AppDbContext : DbContext
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

   
                entity.HasOne(r => r.Oda)
                 .WithMany() 
                 .HasForeignKey(r => r.OdaId)
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


        }
    }
}

