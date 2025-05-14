using MadrinHotelCRM.DataAccess.Context;
using MadrinHotelCRM.Repositories.Repositories.Concrete;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MadrinHotelCRM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //  DbContext Ayarý - appsettings.json'dan al
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            //  UnitOfWork ve Repository'leri ekle
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Service Katmaný Ekle(service katmaný eklendiðinde aktiflenecek)
           // builder.Services.AddScoped<MusteriService>();
           // builder.Services.AddScoped<RezervasyonService>(); 
          

            //  Controller'larý ekle
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
