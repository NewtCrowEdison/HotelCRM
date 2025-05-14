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
        }
    }
}

