using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MadrinHotelCRM.ExtensionMethods
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Adds the dependency injection services to the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        public static IServiceCollection AddServiceCollectionExtension(this IServiceCollection services)
        {
            services.AddScoped<IEtiketService, EtiketService>();
            services.AddScoped<IEtkilesimService, EtkilesimService>();
            services.AddScoped<IFaturaService, FaturaService>();
            services.AddScoped<IGenelTakipService, GenelTakipService>();
            services.AddScoped<IGeriBildirimService, GeriBildirimService>();
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IMusteriAnalizService, MusteriAnalizService>();
            services.AddScoped<IMusteriService, MusteriService>();
            services.AddScoped<IOdaDurumService, OdaDurumService>();
            services.AddScoped<IOdaService, OdaService>();
            services.AddScoped<IOdaTipiService, OdaTipiService>();
            services.AddScoped<IOdemeService, OdemeService>();
            services.AddScoped<IPaketService, PaketService>();
            services.AddScoped<IPersonelService, PersonelService>();
            services.AddScoped<IRaporlamaService, RaporlamaService>();
            services.AddScoped<IRezervasyonService, RezervasyonService>();
            services.AddScoped<IRezervasyonYonetimService, RezervasyonYonetimService>();
            services.AddScoped<ITarifeService, TarifeService>();
            services.AddScoped<IOdaTarifeService, OdaTarifeService>();
            services.AddScoped<IMusteriRezervasyonService, MusteriRezervasyonService>();
            services.AddScoped<IDepartmanService, DepartmanService>();
            services.AddScoped<IAuthorizationService, AuthorizationService>();
            services.AddHostedService<OdaDurumHostedService>();

            return services;
        }
    }
}
