using MadrinHotelCRM.API.Middlewares;
using MadrinHotelCRM.ExtensionMethods;
using MadrinHotelCRM.DataAccess;
using MadrinHotelCRM.DataAccess.Context;
using MadrinHotelCRM.Repositories.Repositories.Concrete;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MadrinHotelCRM.Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace MadrinHotelCRM.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Serilog Logging
            builder.Host.AddSerilogExtension();

            // DbContext
            builder.Services.AddDbContextExtension(builder.Configuration);

            // Identity & JWT Authentication
            builder.Services
                .AddIdentityExtension()
                .AddJwtExtension(builder.Configuration);

            // AutoMapper, Repositories, Custom Services
            builder.Services
                .AddAutoMapperExtension()
                .AddRepositoryServices()
                .AddServiceCollectionExtension();

            // Swagger (Dev ortamında)
            builder.Services.AddSwaggerExtension();

            // MVC & Razor
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();

            // HttpClient (API'lerden veri almak için)
            builder.Services.AddHttpClient();

            var app = builder.Build();

            // Hata Yakalama
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Custom Middleware (örn: Kullanıcı takibi)
            app.UseMiddleware<TrackingMiddleware>();

            // Razor Pages & Controllerlar
            app.MapRazorPages();
            app.MapControllers();

            // Area Routing
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            // Default Route
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
