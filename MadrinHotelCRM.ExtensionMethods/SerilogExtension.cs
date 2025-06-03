using Microsoft.Extensions.Hosting;
using Serilog;

namespace MadrinHotelCRM.ExtensionMethods
{
    public static class SerilogExtension
    {
        public static IHostBuilder AddSerilogExtension(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, configuration) =>
            {
                configuration.WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day);
            });

            return hostBuilder;
        }
    }
}
