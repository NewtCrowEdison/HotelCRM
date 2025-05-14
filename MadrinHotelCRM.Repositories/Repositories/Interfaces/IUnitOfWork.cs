using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.Repositories.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<EkPaket> EkPaketler { get; }
        IGenericRepository<Etiket> Etiketler { get; }
        IGenericRepository<Fatura> Faturalar { get; }
        IGenericRepository<GenelTakip> GenelTakipler { get; }
        IGenericRepository<GeriBildirim> GeriBildirimler { get; }
        IGenericRepository<Musteri> Musteriler { get; }
        IGenericRepository<Oda> Odalar { get; }
        IGenericRepository<OdaTarife> OdaTarifeleri { get; }
        IGenericRepository<OdaTipi> OdaTipleri { get; }
        IGenericRepository<Odeme> Odemeler { get; }
        IGenericRepository<Personel> Personeller { get; }
        IGenericRepository<Rezervasyon> Rezervasyonlar { get; }
        IGenericRepository<RezervasyonPaket> RezervasyonPaketler { get; }
        IGenericRepository<SistemLog> SistemLoglar { get; }
        IGenericRepository<Tarife> Tarifeler { get; }

        Task<int> CommitAsync();
    }
}

