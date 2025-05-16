using MadrinHotelCRM.DataAccess.Context;
using MadrinHotelCRM.Repositories.Repositories.Concrete;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MadrinHotelCRM.API.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace MadrinHotelCRM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //  DbContext Ayar� - appsettings.json'dan al
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

            //  UnitOfWork ve Repository'leri ekle
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Service Katman� Ekle(service katman� eklendi�inde aktiflenecek)
           // builder.Services.AddScoped<MusteriService>();
           // builder.Services.AddScoped<RezervasyonService>(); 
          

            //  Controller'lar� ekle
            builder.Services.AddControllers();

            // Swagger/OpenAPI ekle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            //  Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
