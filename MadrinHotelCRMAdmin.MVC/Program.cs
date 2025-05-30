// Program.cs (MadrinHotelCRMAdmin.MVC)
using System;
using System.Net.Http;
using System.Net.Http.Headers;

using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using MadrinHotelCRM.Business.Mapp;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MadrinHotelCRM.Repositories.Repositories.Concrete;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.DataAccess.Context;
using Microsoft.EntityFrameworkCore;


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

            // 4) HttpClient
            builder.Services
                .AddHttpClient("ApiClient", client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7225/"); // veya API portun neyse
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { UseCookies = false });

            // 5) AutoMapper ve Services
            builder.Services.AddAutoMapper(typeof(MapProfiles));
            builder.Services.AddScoped<IEkPaketService, EkPaketService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer("Server=.;Database=MadrinHotelCRMDb;Trusted_Connection=True;");
            });


            // 6) Uygulama Oluştur
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Giris}/{id?}");

            app.Run();

        }
    }
}
