using MadrinHotelCRM.Repositories.Repositories.Concrete;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace MadrinHotelCRM.ExtensionMethods
{
    public static class RepositoryExtension
    {
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            // Generic repository DI kaydı
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // UnitOfWork DI kaydı
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
