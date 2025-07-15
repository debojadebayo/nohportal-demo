using System.Diagnostics;

namespace Server.WebApi.Middleware
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

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var requestId = Activity.Current?.Id ?? context.TraceIdentifier;

            // Skip logging for health checks and static files
            if (ShouldSkipLogging(context.Request.Path))
            {
                await _next(context);
                return;
            }

            _logger.LogInformation("Starting request {Method} {Path} - RequestId: {RequestId}",
                context.Request.Method,
                context.Request.Path,
                requestId);

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Request {Method} {Path} failed - RequestId: {RequestId}, Duration: {Duration}ms",
                    context.Request.Method,
                    context.Request.Path,
                    requestId,
                    stopwatch.ElapsedMilliseconds);
                throw;
            }
            finally
            {
                stopwatch.Stop();
                
                var logLevel = context.Response.StatusCode >= 400 ? LogLevel.Warning : LogLevel.Information;
                _logger.Log(logLevel, 
                    "Completed request {Method} {Path} - Status: {StatusCode}, Duration: {Duration}ms, RequestId: {RequestId}",
                    context.Request.Method,
                    context.Request.Path,
                    context.Response.StatusCode,
                    stopwatch.ElapsedMilliseconds,
                    requestId);
            }
        }

        private static bool ShouldSkipLogging(PathString path)
        {
            return path.StartsWithSegments("/health") ||
                   path.StartsWithSegments("/favicon.ico") ||
                   path.StartsWithSegments("/_framework") ||
                   path.StartsWithSegments("/css") ||
                   path.StartsWithSegments("/js") ||
                   path.StartsWithSegments("/images");
        }
    }
}
