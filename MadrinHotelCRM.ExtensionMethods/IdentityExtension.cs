using MadrinHotelCRM.DataAccess.Context;
using MadrinHotelCRM.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MadrinHotelCRM.ExtensionMethods
{
    public static class IdentityExtensions
    {
        public static IServiceCollection AddIdentityExtension(this IServiceCollection services)
        {
            services.AddDefaultIdentity<AppUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddRoles<IdentityRole<string>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultUI();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
                options.AddPolicy("Personel", policy => policy.RequireRole("Personel"));
            });

            return services;
        }
    }
}
