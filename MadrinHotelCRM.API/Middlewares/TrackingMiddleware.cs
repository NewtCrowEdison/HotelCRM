namespace MadrinHotelCRM.API.Middlewares
{
    public class TrackingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<TrackingMiddleware> _logger;

        public TrackingMiddleware(RequestDelegate next, ILogger<TrackingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
            {
                var path = context.Request.Path;
                var user = context.User?.Identity?.Name ?? "Bilinmiyor";
                var timestamp = DateTime.UtcNow;

                context.Request.EnableBuffering();

                string bodyAsText = "";
                using (var reader = new StreamReader(context.Request.Body, leaveOpen: true))
                {
                    bodyAsText = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }

                _logger.LogInformation("User: {User}, Method: {Method}, Path: {Path}, Body: {Body}, Time: {Time}",
                user, context.Request.Method, path, bodyAsText, timestamp); 

            }
            
            await _next(context);
        }

    }
}
