using Microsoft.Extensions.DependencyInjection;

namespace MadrinHotelCRM.ExtensionMethods
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
    }
}


