using System;
using System.Threading;
using System.Threading.Tasks;
using MadrinHotelCRM.Entities.Enums;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MadrinHotelCRM.Services.Services
{
    public class OdaDurumHostedService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public OdaDurumHostedService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Her 1 dakikada bir kontrol edecek örnek
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    // Scoped servislere ancak burada, yeni bir scope içinde erişebiliriz
                    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                    var now = DateTime.UtcNow;

                    // 1) Başlangıç tarihi geçmiş ama dolu olmayan rezervasyonları DOLU yap
                    var gelecekRezler = await uow.Read<Rezervasyon>()
                        .FindAsync(r => r.BaslangicTarihi <= now
                                      && r.BitisTarihi >= now
                                      && r.Durum != RezervasyonDurum.İptalEdildi);
                    foreach (var r in gelecekRezler)
                    {
                        var oda = await uow.Read<Oda>().GetByIdAsync(r.OdaId);
                        if (oda != null && oda.Durum != OdaDurum.Dolu)
                        {
                            oda.Durum = OdaDurum.Dolu;
                            uow.Update<Oda>().Update(oda);
                        }
                    }

                    // 2) Bitiş tarihi geçmiş rezervasyonlardaki odaları BOŞ yap
                    var bitenRezler = await uow.Read<Rezervasyon>()
                        .FindAsync(r => r.BitisTarihi < now
                                      && r.Durum != RezervasyonDurum.İptalEdildi);
                    foreach (var r in bitenRezler)
                    {
                        var oda = await uow.Read<Oda>().GetByIdAsync(r.OdaId);
                        if (oda != null && oda.Durum != OdaDurum.Bos)
                        {
                            oda.Durum = OdaDurum.Bos;
                            uow.Update<Oda>().Update(oda);
                        }
                    }

                    await uow.CommitAsync();
                }

                // Bir sonraki çalıştırma öncesi bekle
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
