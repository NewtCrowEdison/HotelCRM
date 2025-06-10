using MadrinHotelCRM.Business.Mapp;
using MadrinHotelCRM.DataAccess.Context;
using MadrinHotelCRM.Entities.Models;
using MadrinHotelCRM.ExtensionMethods;
using MadrinHotelCRM.Repositories.Repositories.Concrete;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MadrinHotelCRMAdmin.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 1) MVC
            builder.Services.AddControllersWithViews();

            // 2) Cache (Session'in bellek tabanlı store'u için)
            builder.Services.AddDistributedMemoryCache();

            // 3) Session
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "MadrinHotelSession";
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
            });

            // 4) Authentication + Authorization
            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Giris";
                    options.Cookie.Name = "HotelCRMAuth";
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                });
            builder.Services.AddAuthorization(); // [Authorize] kullanacaksan eklemekte fayda var

            // 5) HttpClient
            builder.Services
                .AddHttpClient("ApiClient", client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7225/");
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { UseCookies = false });

            // builder.Services.AddHttpClientExtension(builder.Configuration);

            // 6) AutoMapper ve DI’lar
            builder.Services.AddAutoMapper(typeof(MapProfiles));
            builder.Services.AddScoped<IEkPaketService, EkPaketService>();
            builder.Services.AddServiceCollectionExtension();
        

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Giriş yapan kullanıcının ad ve soyad bilgilerini gösterebilmek için 
            builder.Services.AddIdentity<AppUser, IdentityRole>()
              .AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            //builder.Services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer("Server=.;Database=MadrinHotelCRMDb;Trusted_Connection=True;"));
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString, opt =>
                {
                    opt.EnableRetryOnFailure();
                }));

            var app = builder.Build();

            // Ortam ayarları
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Middleware sırası çok önemli!
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSession();          // <-- önce Session
            app.UseAuthentication();   // <-- sonra AuthN
            app.UseAuthorization();    // <-- en son AuthZ

            // Route’lar
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Giris}/{id?}");

            app.Run();
        }
    }
}