using MadrinHotelCRM.DataAccess;
using MadrinHotelCRM.DataAccess.Context;
using MadrinHotelCRM.Repositories.Repositories.Concrete;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MadrinHotelCRM.Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.Text;

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


           

            //  UnitOfWork ve Repositoryleri 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // MVC Controllerları 
            builder.Services.AddControllersWithViews();

            // API den gelen Controllerlar için servis
            builder.Services.AddHttpClient();

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

            //app.MapRazorPages();
            //app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

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
