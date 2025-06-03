using MadrinHotelCRM.Business.Mapp;
using Microsoft.Extensions.DependencyInjection;

namespace MadrinHotelCRM.ExtensionMethods
{
    public static class AutoMapperExtensions
    {
        public static IServiceCollection AddAutoMapperExtension(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MapProfiles));
            return services;
        }
    }
}
