using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.DataAccess.Context;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;

namespace MadrinHotelCRM.Repositories.Repositories.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IGenericRepository<EkPaket> EkPaketler { get; }
        public IGenericRepository<Etiket> Etiketler { get; }
        public IGenericRepository<Fatura> Faturalar { get; }
        public IGenericRepository<GenelTakip> GenelTakipler { get; }
        public IGenericRepository<GeriBildirim> GeriBildirimler { get; }
        public IGenericRepository<Musteri> Musteriler { get; }
        public IGenericRepository<Oda> Odalar { get; }
        public IGenericRepository<OdaTarife> OdaTarifeleri { get; }
        public IGenericRepository<OdaTipi> OdaTipleri { get; }
        public IGenericRepository<Odeme> Odemeler { get; }
        public IGenericRepository<Personel> Personeller { get; }
        public IGenericRepository<Rezervasyon> Rezervasyonlar { get; }
        public IGenericRepository<RezervasyonPaket> RezervasyonPaketler { get; }
        public IGenericRepository<SistemLog> SistemLoglar { get; }
        public IGenericRepository<Tarife> Tarifeler { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            EkPaketler = new GenericRepository<EkPaket>(_context);
            Etiketler = new GenericRepository<Etiket>(_context);
            Faturalar = new GenericRepository<Fatura>(_context);
            GenelTakipler = new GenericRepository<GenelTakip>(_context);
            GeriBildirimler = new GenericRepository<GeriBildirim>(_context);
            Musteriler = new GenericRepository<Musteri>(_context);
            Odalar = new GenericRepository<Oda>(_context);
            OdaTarifeleri = new GenericRepository<OdaTarife>(_context);
            OdaTipleri = new GenericRepository<OdaTipi>(_context);
            Odemeler = new GenericRepository<Odeme>(_context);
            Personeller = new GenericRepository<Personel>(_context);
            Rezervasyonlar = new GenericRepository<Rezervasyon>(_context);
            RezervasyonPaketler = new GenericRepository<RezervasyonPaket>(_context);
            SistemLoglar = new GenericRepository<SistemLog>(_context);
            Tarifeler = new GenericRepository<Tarife>(_context);
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose() // veritabanını bağlantılarını açık bırakmayı engelliyor 
        {
            _context.Dispose();
        }
    }
}

