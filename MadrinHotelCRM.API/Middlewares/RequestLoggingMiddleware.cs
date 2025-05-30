using System.Diagnostics;

namespace MadrinHotelCRM.API.Middlewares
{
    public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        var request = context.Request;

        var method = request.Method;
        var path = request.Path;
        var timestamp = DateTime.UtcNow;
        var ip = context.Connection.RemoteIpAddress?.ToString() ?? "Bilinmiyor";
        var user = context.User?.Identity?.Name ?? "Anonim";

        try
        {
            // Giriş logu
            _logger.LogInformation("➡️ [{Time}] {User} IP:{IP} - HTTP {Method} {Path}",
                timestamp, user, ip, method, path);

            await _next(context); 

            stopwatch.Stop();

            // Çıkış logu
            _logger.LogInformation(" [{Time}] {User} - HTTP {Method} {Path} responded {StatusCode} in {Elapsed}ms",
                DateTime.UtcNow, user, method, path, context.Response.StatusCode, stopwatch.ElapsedMilliseconds);
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            // Hata logu
            _logger.LogError(ex, " [{Time}] {User} - HTTP {Method} {Path} failed in {Elapsed}ms",
                DateTime.UtcNow, user, method, path, stopwatch.ElapsedMilliseconds);

            throw; // Hatayı tekrar fırlat, sistem davranışını bozma
        }
    }
}
}
