using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using WebApi.Configuration;

namespace WebApi.Services;

/// <summary>
/// Service for validating configuration settings at startup
/// </summary>
public class ConfigurationValidationService
{
    private readonly ILogger<ConfigurationValidationService> _logger;
    private readonly IConfiguration _configuration;

    public ConfigurationValidationService(
        ILogger<ConfigurationValidationService> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    /// <summary>
    /// Validates all configuration sections and logs any issues
    /// </summary>
    /// <returns>True if all validations pass, false otherwise</returns>
    public bool ValidateConfiguration()
    {
        var isValid = true;
        var validationResults = new List<ValidationResult>();

        // Validate connection strings
        isValid &= ValidateConnectionStrings();

        // Validate JWT configuration
        isValid &= ValidateConfiguration<JwtOptions>(JwtOptions.SectionName, validationResults);

        // Validate Keycloak configuration
        isValid &= ValidateConfiguration<KeycloakAdminClientOptions>(KeycloakAdminClientOptions.SectionName, validationResults);

        // Validate CORS configuration
        isValid &= ValidateConfiguration<CorsOptions>(CorsOptions.SectionName, validationResults);

        // Validate database configuration
        isValid &= ValidateConfiguration<DatabaseOptions>(DatabaseOptions.SectionName, validationResults);

        // Validate security configuration
        isValid &= ValidateConfiguration<SecurityOptions>(SecurityOptions.SectionName, validationResults);

        // Validate performance configuration
        isValid &= ValidateConfiguration<PerformanceOptions>(PerformanceOptions.SectionName, validationResults);

        // Validate Scalar configuration
        isValid &= ValidateConfiguration<ScalarOptions>(ScalarOptions.SectionName, validationResults);

        // Log validation results
        if (validationResults.Any())
        {
            _logger.LogError("Configuration validation failed with {Count} errors:", validationResults.Count);
            foreach (var result in validationResults)
            {
                _logger.LogError("- {ErrorMessage}", result.ErrorMessage);
            }
        }

        if (isValid)
        {
            _logger.LogInformation("Configuration validation completed successfully");
        }
        else
        {
            _logger.LogError("Configuration validation failed. Please check your configuration files.");
        }

        return isValid;
    }

    /// <summary>
    /// Validates a specific configuration section
    /// </summary>
    private bool ValidateConfiguration<T>(string sectionName, List<ValidationResult> validationResults)
        where T : class, new()
    {
        var section = _configuration.GetSection(sectionName);
        if (!section.Exists())
        {
            validationResults.Add(new ValidationResult($"Configuration section '{sectionName}' is missing"));
            return false;
        }

        var options = new T();
        section.Bind(options);

        var validationContext = new ValidationContext(options);
        var sectionValidationResults = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(options, validationContext, sectionValidationResults, validateAllProperties: true);

        if (!isValid)
        {
            foreach (var result in sectionValidationResults)
            {
                validationResults.Add(new ValidationResult($"[{sectionName}] {result.ErrorMessage}"));
            }
        }

        return isValid;
    }

    /// <summary>
    /// Validates connection strings
    /// </summary>
    private bool ValidateConnectionStrings()
    {
        var isValid = true;
        var connectionStrings = _configuration.GetSection("ConnectionStrings");

        if (!connectionStrings.Exists())
        {
            _logger.LogError("ConnectionStrings section is missing");
            return false;
        }

        // Validate required connection strings
        var requiredConnectionStrings = new[] { "DefaultConnection" };

        foreach (var connectionStringName in requiredConnectionStrings)
        {
            var connectionString = connectionStrings[connectionStringName];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                _logger.LogError("Connection string '{ConnectionStringName}' is missing or empty", connectionStringName);
                isValid = false;
            }
        }

        return isValid;
    }
}

/// <summary>
/// Extension methods for configuration validation
/// </summary>
public static class ConfigurationValidationExtensions
{
    /// <summary>
    /// Adds configuration validation services
    /// </summary>
    public static IServiceCollection AddConfigurationValidation(this IServiceCollection services)
    {
        services.AddSingleton<ConfigurationValidationService>();
        return services;
    }

    /// <summary>
    /// Validates configuration and throws an exception if validation fails
    /// </summary>
    public static void ValidateConfigurationOnStartup(this IServiceProvider serviceProvider)
    {
        var validationService = serviceProvider.GetRequiredService<ConfigurationValidationService>();
        var isValid = validationService.ValidateConfiguration();

        if (!isValid)
        {
            throw new InvalidOperationException("Configuration validation failed. Please check your configuration files and logs for details.");
        }
    }

    /// <summary>
    /// Configures strongly-typed configuration options with validation
    /// </summary>
    public static IServiceCollection ConfigureWithValidation<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName)
        where T : class
    {
        services.Configure<T>(configuration.GetSection(sectionName));
        services.AddOptions<T>()
            .Bind(configuration.GetSection(sectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
}

/// <summary>
/// Hosted service that validates configuration at startup
/// </summary>
public class ConfigurationValidationHostedService : IHostedService
{
    private readonly ConfigurationValidationService _validationService;
    private readonly ILogger<ConfigurationValidationHostedService> _logger;

    public ConfigurationValidationHostedService(
        ConfigurationValidationService validationService,
        ILogger<ConfigurationValidationHostedService> logger)
    {
        _validationService = validationService;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting configuration validation...");
        
        var isValid = _validationService.ValidateConfiguration();
        
        if (!isValid)
        {
            _logger.LogCritical("Configuration validation failed. Application startup will be aborted.");
            throw new InvalidOperationException("Configuration validation failed");
        }

        _logger.LogInformation("Configuration validation completed successfully");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
