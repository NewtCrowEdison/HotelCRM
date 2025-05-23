using MadrinHotelCRM.DataAccess;
using MadrinHotelCRM.DataAccess.Context;
using MadrinHotelCRM.Repositories.Repositories.Concrete;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MadrinHotelCRM.Entities.Models;

namespace MadrinHotelCRM.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Madrin Hotel CRM MVC Başlatılıyor...");
            var builder = WebApplication.CreateBuilder(args);

            //  DbContext 
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Identity
            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole<string>>().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Personel", policy => policy.RequireRole("Personel"));
            });


            //  UnitOfWork ve Repositoryleri 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            //  Service Katmanı  (service eklenince açılacak)
            // builder.Services.AddScoped<MusteriService>();
            // builder.Services.AddScoped<RezervasyonService>();

            // MVC Controllerları 
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            //  Exception Handling
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.MapRazorPages();
            app.UseAuthentication();
            app.UseAuthorization();

            //  MVC Routing
            app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}