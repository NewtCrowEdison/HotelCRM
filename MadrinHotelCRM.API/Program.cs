using MadrinHotelCRM.API.Middlewares;
using MadrinHotelCRM.Business.Mapp;
using MadrinHotelCRM.ExtensionMethods;

namespace MadrinHotelCRM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var jwtSecret = builder.Configuration["AppSettings:Secret"];

            // Serilog Extension
            builder.Host.AddSerilogExtension();

            // Extension Methods
            builder.Services
           .AddJwtExtension(jwtSecret)
           .AddDbContextExtension(builder.Configuration)
           .AddIdentityExtension()
           .AddAutoMapperExtension()
           .AddRepositoryServices()
           .AddServiceCollectionExtension()
           .AddSwaggerExtension();

            // MVC + Razor + Controllers
            builder.Services.AddControllers();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Middleware & Pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<TrackingMiddleware>();

            app.MapRazorPages();
            app.MapControllers();
            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
