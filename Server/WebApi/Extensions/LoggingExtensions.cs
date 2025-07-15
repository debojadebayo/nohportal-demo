using Server.WebApi.Middleware;

namespace Server.WebApi.Extensions
{
    public static class LoggingExtensions
    {
        public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLoggingMiddleware>();
        }

        public static IServiceCollection AddApplicationLogging(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure logging filters
            services.Configure<LoggerFilterOptions>(options =>
            {
                // Add custom filters if needed
                options.MinLevel = LogLevel.Information;
            });

            return services;
        }
    }
}
