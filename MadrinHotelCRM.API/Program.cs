using MadrinHotelCRM.API.Middlewares;
using MadrinHotelCRM.ExtensionMethods;

namespace MadrinHotelCRM.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Serilog Extension
            builder.Host.AddSerilogExtension();

            // Extension Methods
            builder.Services
                .AddDbContextExtension(builder.Configuration)
                .AddIdentityExtension()
                .AddJwtExtension(builder.Configuration)
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
