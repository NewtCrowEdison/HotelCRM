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

            // 2) Cache (Session store)
            builder.Services.AddDistributedMemoryCache();

            // 3) Session
            builder.Services.AddSession(opts =>
            {
                opts.Cookie.Name = "MadrinHotelSession";
                opts.IdleTimeout = TimeSpan.FromMinutes(30);
                opts.Cookie.HttpOnly = true;
            });

            // 4) EF Core DbContext
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlServer(connectionString, sql => sql.EnableRetryOnFailure()));

            // 5) Identity Core (UserManager, RoleManager, SignInManager)
            builder.Services
                .AddIdentityCore<AppUser>(options =>
                {
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 6;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;
                    options.Lockout.AllowedForNewUsers = true;

                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddSignInManager()               // <-- SignInManager’ı ekliyoruz
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // 6) Authentication + Authorization (Custom Cookie)
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.LoginPath = "/Auth/Giris";
                    options.AccessDeniedPath = "/Auth/AccessDenied";
                    options.Cookie.Name = "HotelCRMAuth";
                    options.ExpireTimeSpan = TimeSpan.FromDays(1);
                });

            builder.Services.AddAuthorization(opts =>
            {
                opts.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                //opts.AddPolicy("PersonelOnly", policy => policy.RequireRole("Personel"));
            });

            // 7) HttpClient
            builder.Services
                .AddHttpClient("ApiClient", client =>
                {
                    client.BaseAddress = new Uri("https://localhost:7225/");
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                })
                .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { UseCookies = false });

            // 8) AutoMapper & DI
            builder.Services.AddAutoMapper(typeof(MapProfiles));
            builder.Services.AddScoped<IEkPaketService, EkPaketService>();
            builder.Services.AddServiceCollectionExtension();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            var app = builder.Build();

            // 9) Environment
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // 10) Middleware sırası
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            // 11) Default route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=Giris}/{id?}");

            app.Run();
        }
    }
}
