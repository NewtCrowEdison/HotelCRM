using MadrinHotelCRM.DataAccess.Context;
using MadrinHotelCRM.Repositories.Repositories.Concrete;
using MadrinHotelCRM.Repositories.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using MadrinHotelCRM.Services.Interfaces;
using MadrinHotelCRM.Services.Services;
using MadrinHotelCRM.Business.Mapp;


namespace MadrinHotelCRM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            //  DbContext Ayarı - appsettings.json'dan al
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Identity
            builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDbContext>();

            // AutoMapper: MapProfiles sınıfını kullanacak
            builder.Services.AddAutoMapper(typeof(MapProfiles));

            //  UnitOfWork ve Repository'leri ekle
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Generic repository DI kaydı
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Service Katmanı Ekle(service katmanı eklendiğinde aktiflenecek)
            builder.Services.AddScoped<IEtiketService, EtiketService>();
            builder.Services.AddScoped<IEtkilesimService, EtkilesimService>();
            builder.Services.AddScoped<IFaturaService, FaturaService>();
            builder.Services.AddScoped<IGenelTakipService, GenelTakipService>();
            builder.Services.AddScoped<IGeriBildirimService, GeriBildirimService>();
            builder.Services.AddScoped<ILogService, LogService>();
            builder.Services.AddScoped<IMusteriAnalizService, MusteriAnalizService>();
            builder.Services.AddScoped<IMusteriService, MusteriService>();
            builder.Services.AddScoped<IOdaDurumService, OdaDurumService>();
            builder.Services.AddScoped<IOdaService, OdaService>();
            builder.Services.AddScoped<IOdaTipiService, OdaTipiService>();
            builder.Services.AddScoped<IOdemeService, OdemeService>();
            builder.Services.AddScoped<IPaketService, PaketService>();
            builder.Services.AddScoped<IPersonelService, PersonelService>();
            builder.Services.AddScoped<IRaporlamaService, RaporlamaService>();
            builder.Services.AddScoped<IRezervasyonService, RezervasyonService>();
            builder.Services.AddScoped<IRezervasyonYonetimService, RezervasyonYonetimService>();
            builder.Services.AddScoped<ITarifeService, TarifeService>();
            builder.Services.AddScoped<IOdaTarifeService, OdaTarifeService>();
            
            //  Controller'lari ekle
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
