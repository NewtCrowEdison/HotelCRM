// Program.cs
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

            // 2) API�ya istek atmak i�in HttpClient
            builder.Services
                .AddHttpClient("ApiClient", client =>
                {
                    client.BaseAddress = new Uri("https://localhost:5001/"); // API�nizin URL�si
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

            // 3) Middleware pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // E�er MVC�de kendi cookie/auth kullan�yorsan:
            // app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Admin}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
