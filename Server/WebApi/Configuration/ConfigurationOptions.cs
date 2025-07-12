using System.ComponentModel.DataAnnotations;

namespace WebApi.Configuration;

/// <summary>
/// Configuration options for JWT authentication
/// </summary>
public class JwtOptions
{
    public const string SectionName = "Jwt";

    [Required]
    [Url]
    public string MetadataAddress { get; set; } = string.Empty;

    [Required]
    public string Issuer { get; set; } = string.Empty;

    [Required]
    public string Audience { get; set; } = string.Empty;

    public bool RequireHttpsMetadata { get; set; } = true;

    public TimeSpan ClockSkew { get; set; } = TimeSpan.FromMinutes(5);

    public TokenValidationOptions TokenValidationParameters { get; set; } = new();
}

/// <summary>
/// Token validation configuration options
/// </summary>
public class TokenValidationOptions
{
    public bool ValidateIssuer { get; set; } = true;
    public bool ValidateAudience { get; set; } = true;
    public bool ValidateLifetime { get; set; } = true;
    public bool ValidateIssuerSigningKey { get; set; } = true;
    public TimeSpan ClockSkew { get; set; } = TimeSpan.FromMinutes(5);
}

/// <summary>
/// Configuration options for Keycloak admin client
/// </summary>
public class KeycloakAdminClientOptions
{
    public const string SectionName = "KeycloakAdminClient";

    [Required]
    [Url]
    public string KeycloakUrl { get; set; } = string.Empty;

    [Required]
    public string Realm { get; set; } = string.Empty;

    [Required]
    public string ClientId { get; set; } = string.Empty;

    [Required]
    public string ClientSecret { get; set; } = string.Empty;

    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

    [Range(1, 10)]
    public int RetryCount { get; set; } = 3;
}

/// <summary>
/// Configuration options for CORS settings
/// </summary>
public class CorsOptions
{
    public const string SectionName = "Cors";

    [Required]
    [Url]
    public string AllowedClientOrigin { get; set; } = string.Empty;

    [Required]
    [Url]
    public string AllowedServerOrigin { get; set; } = string.Empty;

    public string AllowedMethods { get; set; } = "GET, PUT, POST, DELETE, OPTIONS";

    public string AllowedHeaders { get; set; } = "*";

    public string ExposedHeaders { get; set; } = "*";

    [Range(0, 86400)]
    public int MaxAge { get; set; } = 3600;

    public bool AllowCredentials { get; set; } = true;
}

/// <summary>
/// Configuration options for database settings
/// </summary>
public class DatabaseOptions
{
    public const string SectionName = "Database";

    [Range(1, 300)]
    public int CommandTimeout { get; set; } = 30;

    public bool EnableRetryOnFailure { get; set; } = true;

    [Range(1, 10)]
    public int MaxRetryCount { get; set; } = 3;

    public TimeSpan MaxRetryDelay { get; set; } = TimeSpan.FromSeconds(5);

    public bool EnableSensitiveDataLogging { get; set; } = false;

    public bool EnableDetailedErrors { get; set; } = false;

    public bool EnableServiceProviderCaching { get; set; } = true;

    public bool EnableQuerySplitting { get; set; } = false;
}

/// <summary>
/// Configuration options for security settings
/// </summary>
public class SecurityOptions
{
    public const string SectionName = "Security";

    public bool RequireHttps { get; set; } = true;

    [Range(1024, 1073741824)] // 1KB to 1GB
    public long MaxRequestBodySize { get; set; } = 10485760; // 10MB

    public TimeSpan RequestTimeout { get; set; } = TimeSpan.FromMinutes(2);

    public bool EnableDataProtection { get; set; } = true;

    public bool EnableAntiforgery { get; set; } = true;
}

/// <summary>
/// Configuration options for performance settings
/// </summary>
public class PerformanceOptions
{
    public const string SectionName = "Performance";

    public bool EnableResponseCompression { get; set; } = true;

    public bool EnableResponseCaching { get; set; } = true;

    [Range(0, 3600)]
    public int DefaultCacheDuration { get; set; } = 300;

    [Range(1, 10000)]
    public int MaxConcurrentRequests { get; set; } = 100;

    [Range(1, 100000)]
    public int RequestQueueLimit { get; set; } = 1000;
}

/// <summary>
/// Configuration options for health checks
/// </summary>
public class HealthCheckOptions
{
    public const string SectionName = "HealthChecks";

    public bool Enabled { get; set; } = true;

    public bool DetailedErrors { get; set; } = false;

    public HealthCheckEndpoints Endpoints { get; set; } = new();

    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);
}

/// <summary>
/// Health check endpoint configuration
/// </summary>
public class HealthCheckEndpoints
{
    public string Health { get; set; } = "/health";
    public string Ready { get; set; } = "/health/ready";
    public string Live { get; set; } = "/health/live";
}

/// <summary>
/// Configuration options for Scalar API documentation
/// </summary>
public class ScalarOptions
{
    public const string SectionName = "Scalar";

    public bool Enabled { get; set; } = false;

    public string RoutePrefix { get; set; } = "scalar";

    [Required]
    public string Title { get; set; } = "API";

    [Required]
    public string Version { get; set; } = "v1";

    public string Description { get; set; } = string.Empty;

    public bool IncludeXmlComments { get; set; } = true;

    public string Theme { get; set; } = "default";

    public bool ShowModels { get; set; } = true;

    public bool ShowDownloadButton { get; set; } = true;

    public bool EnableTryItOut { get; set; } = false;

    public bool ShowRequestHeaders { get; set; } = false;

    public bool ShowResponseHeaders { get; set; } = false;
}

/// <summary>
/// Configuration options for rate limiting
/// </summary>
public class RateLimitingOptions
{
    public const string SectionName = "RateLimiting";

    public bool EnableRateLimiting { get; set; } = false;

    public GlobalLimiterOptions GlobalLimiter { get; set; } = new();

    public Dictionary<string, PolicyLimiterOptions> Policies { get; set; } = new();
}

/// <summary>
/// Global rate limiter configuration
/// </summary>
public class GlobalLimiterOptions
{
    [Range(1, 100000)]
    public int PermitLimit { get; set; } = 1000;

    public TimeSpan Window { get; set; } = TimeSpan.FromMinutes(1);

    [Range(0, 10000)]
    public int QueueLimit { get; set; } = 100;
}

/// <summary>
/// Policy-specific rate limiter configuration
/// </summary>
public class PolicyLimiterOptions
{
    [Range(1, 10000)]
    public int PermitLimit { get; set; } = 100;

    public TimeSpan Window { get; set; } = TimeSpan.FromMinutes(1);

    [Range(0, 1000)]
    public int QueueLimit { get; set; } = 10;
}

/// <summary>
/// Configuration options for feature flags
/// </summary>
public class FeatureOptions
{
    public const string SectionName = "Features";

    public bool EnableAuditLogs { get; set; } = true;
    public bool EnablePerformanceCounters { get; set; } = true;
    public bool EnableDetailedExceptions { get; set; } = false;
    public bool EnableCors { get; set; } = true;
    public bool EnableScalar { get; set; } = false;
    public bool EnableDeveloperExceptionPage { get; set; } = false;
    public bool EnableDatabaseDeveloperPageExceptionFilter { get; set; } = false;
}

/// <summary>
/// Configuration options for development-specific settings
/// </summary>
public class DevelopmentOptions
{
    public const string SectionName = "Development";

    public bool SeedData { get; set; } = false;
    public bool ResetDatabaseOnStartup { get; set; } = false;
    public bool EnableHotReload { get; set; } = true;
    public bool ShowDetailedErrors { get; set; } = true;
    public bool EnableBrowserLink { get; set; } = true;
}

/// <summary>
/// Configuration options for Azure Key Vault
/// </summary>
public class KeyVaultOptions
{
    public const string SectionName = "KeyVault";

    [Url]
    public string VaultUri { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;

    public string TenantId { get; set; } = string.Empty;
}

/// <summary>
/// Configuration options for Application Insights
/// </summary>
public class ApplicationInsightsOptions
{
    public const string SectionName = "ApplicationInsights";

    public string ConnectionString { get; set; } = string.Empty;

    public bool EnableRequestTracking { get; set; } = true;

    public bool EnableDependencyTracking { get; set; } = true;

    public bool EnablePerformanceCounters { get; set; } = true;

    public bool EnableHeartbeat { get; set; } = true;

    public double SamplingPercentage { get; set; } = 100.0;
}
