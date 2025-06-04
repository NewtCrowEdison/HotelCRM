using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;

namespace MadrinHotelCRM.ExtensionMethods
{
    public static class HttpClientExtension
    {
        public static IServiceCollection AddHttpClientExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            if (string.IsNullOrEmpty(baseUrl))
                throw new ArgumentNullException("ApiSettings:BaseUrl appsettings.json içinde tanımlanmalı.");

            services.AddHttpClient("ApiClient", client =>
            {
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            });

            services.AddScoped<IHttpClientService, HttpClientService>();

            return services;
        }
    }
}
