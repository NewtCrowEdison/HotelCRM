// Program.cs (MadrinHotelCRMAdmin.MVC)
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
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

            // 2) Session
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "MadrinHotelSession";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            // 3) Cookie-based Authentication
            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Giris";
                    options.Cookie.Name = "HotelCRMAuth";
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                });

            // 4) API’ya istek atmak için HttpClient
            builder.Services
                .AddHttpClient("ApiClient", client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7225/");
                    client.DefaultRequestHeaders.Accept
                          .Add(new MediaTypeWithQualityHeaderValue("application/json"));
                })
                // Biz cookie’leri manuel yöneteceðimiz için HttpClientHandler'da UseCookies = false
                .ConfigurePrimaryHttpMessageHandler(() =>
                    new HttpClientHandler { UseCookies = false }
                );

            var app = builder.Build();

            // 5) Middleware pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSession();        // önce Session
            app.UseAuthentication(); // sonra Auth
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Giris}/{id?}");

            app.Run();
        }
    }
}
