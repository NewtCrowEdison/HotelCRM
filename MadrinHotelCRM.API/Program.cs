using System;
using System.IO;
using System.Text.Json.Serialization;
using MadrinHotelCRM.API.Middlewares;
using MadrinHotelCRM.Business.Mapp;
using MadrinHotelCRM.ExtensionMethods;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;

namespace MadrinHotelCRM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //1) CORS � sadece AdminPanel MVC�den gelen istekler
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAdminPanel", policy =>
                {
                    policy
                      .WithOrigins("https://localhost:7196")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
                });
            });

            // 2) Ayarlar, Serilog, DbContext, Identity, AutoMapper, vs.
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var jwtSecret = builder.Configuration["AppSettings:Secret"];

            builder.Host.AddSerilogExtension();

            builder.Services
               .AddJwtExtension(jwtSecret)
               .AddAuthorization()           // ? buray� ekle
               .AddDbContextExtension(builder.Configuration)
               .AddIdentityExtension()
               .AddAutoMapperExtension()
               .AddRepositoryServices()
               .AddServiceCollectionExtension()
               .AddSwaggerExtension();

            // 3) JSON enum converter ile Controllers
            builder.Services
                .AddControllers();
         
            // 4) E�er Razor Pages da var ise
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // 5) Swagger (Development)
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // 6) Statik dosyalar� (wwwroot) servis etmek i�in 
            app.UseStaticFiles();

            app.UseHttpsRedirection();

            // 7) CORS :  Ap� adresi ile mvc adresi farkl� oldu�unda eri�im i�in izin vermemi sa�lad� 
            app.UseCors("AllowAdminPanel");

            // 8) G�venlik
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TrackingMiddleware>();

            // 9) Routing
            app.MapRazorPages();
            app.MapControllers();
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

