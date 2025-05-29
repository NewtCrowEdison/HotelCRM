// Program.cs
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using MadrinHotelCRM.Business.Mapp;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MadrinHotelCRMAdmin.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1) MVC
            builder.Services.AddControllersWithViews();

            //  2) AutoMapper Profili tanımı
            builder.Services.AddAutoMapper(typeof(MapProfiles));
            //ek paket servisi view componentinde kullanmak için servis ekleme
            builder.Services.AddScoped<IEkPaketService, EkPaketService>();

            // 3) API’ya istek atmak için HttpClient
            builder.Services
                .AddHttpClient("ApiClient", client =>
                {
                    client.BaseAddress = new Uri("https://localhost:5001/"); // API’nizin URL’si
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .ConfigurePrimaryHttpMessageHandler(() =>
                    new HttpClientHandler
                    {
                        UseCookies = true,
                        CookieContainer = new CookieContainer()
                    }
                );

            var app = builder.Build();

            // 4) Middleware pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Eğer MVC’de kendi cookie/auth kullanıyorsan:
            // app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Giris}/{id?}");

            app.Run();
        }
    }
}
