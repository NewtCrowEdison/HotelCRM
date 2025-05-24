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

            //builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<MadrinHotelCRMMVCContext>();

            // Identity
            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole<string>>().AddEntityFrameworkStores<AppDbContext>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Personel", policy => policy.RequireRole("Personel"));
                
            });

           builder.Services.AddAuthentication(x =>
            {
                     x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                     x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x=>{
                x.RequireHttpsMetadata = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = "MadrinHotelCrm.com",
                    ValidateAudience = false,
                    ValidAudience = "",//kimler için 

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value ?? "")), 
                    ValidateLifetime = true
                };
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
