# Configuration Setup Guide

## Quick Start

This guide will help you set up and configure the NationOH WebAPI application for different environments.

## Prerequisites

- .NET 9.0 SDK
- PostgreSQL database
- Keycloak authentication server
- 3. **Performance**
   - Enable response compression and caching
   - Configure appropriate rate limits
   - Monitor resource usage

4. **API Documentation**
   - Configure Scalar for development and staging
   - Disable Scalar in production for security
   - Include XML comments for better documentation

5. **Deployment**Storage (for production) or Azurite (for development)

## Development Setup

### 1. Database Setup
Ensure your PostgreSQL database is running with the following connection details:
- Host: `app.database` (or `localhost` for local development)
- Port: `5432`
- Database: `postgres`
- Username: `postgres`
- Password: `123`

### 2. Keycloak Setup
Configure Keycloak with:
- Realm: `NationOH`
- Client ID: `nationoh_webapi`
- Admin Client: `admin-cli`

### 3. Development Configuration
The `appsettings.Development.json` is pre-configured for local development with:
- Debug logging enabled
- HTTPS not required
- Scalar enabled
- Rate limiting disabled

### 4. Run the Application
```bash
cd Server/WebApi
dotnet run
```

## Staging Setup

### 1. Update Connection Strings
Replace empty connection strings in `appsettings.Staging.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=your-staging-db;Port=5432;Database=nationoh_staging;Username=your-user;Password=your-password;",
    "AzureBlobStorage": "DefaultEndpointsProtocol=https;AccountName=your-storage;AccountKey=your-key;EndpointSuffix=core.windows.net",
    "Redis": "your-redis-connection-string"
  }
}
```

### 2. Configure Authentication
Update JWT and Keycloak settings:

```json
{
  "Jwt": {
    "MetadataAddress": "https://your-keycloak-staging.com/realms/NationOH/.well-known/openid-configuration",
    "Issuer": "https://your-keycloak-staging.com/realms/NationOH"
  },
  "KeycloakAdminClient": {
    "KeycloakUrl": "https://your-keycloak-staging.com",
    "ClientSecret": "your-staging-client-secret"
  }
}
```

### 3. Update CORS Settings
Configure allowed origins for staging:

```json
{
  "Cors": {
    "AllowedClientOrigin": "https://your-staging-client.com",
    "AllowedServerOrigin": "https://your-staging-api.com"
  }
}
```

### 4. Deploy to Staging
```bash
dotnet publish -c Release -o ./publish
# Deploy to your staging environment
```

## Production Setup

### 1. Security First
Never store secrets in configuration files in production. Use:
- Azure Key Vault for secrets
- Environment variables for container deployments
- Managed identities when possible

### 2. Azure Key Vault Setup
Configure Key Vault in `appsettings.Production.json`:

```json
{
  "KeyVault": {
    "VaultUri": "https://your-keyvault.vault.azure.net/",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "TenantId": "your-tenant-id"
  }
}
```

### 3. Application Insights
Configure monitoring:

```json
{
  "ApplicationInsights": {
    "ConnectionString": "InstrumentationKey=your-key;IngestionEndpoint=https://your-region.in.applicationinsights.azure.com/"
  }
}
```

### 4. Production Hardening
- Enable HTTPS enforcement
- Configure proper CORS origins
- Disable Scalar
- Enable rate limiting
- Set appropriate logging levels

## Environment Variables

You can override any configuration using environment variables:

```bash
# Connection strings
export ConnectionStrings__DefaultConnection="your-connection-string"
export ConnectionStrings__AzureBlobStorage="your-storage-connection"

# JWT configuration
export Jwt__Issuer="https://your-keycloak.com/realms/NationOH"
export Jwt__MetadataAddress="https://your-keycloak.com/realms/NationOH/.well-known/openid-configuration"

# Keycloak
export KeycloakAdminClient__KeycloakUrl="https://your-keycloak.com"
export KeycloakAdminClient__ClientSecret="your-secret"

# CORS
export Cors__AllowedClientOrigin="https://your-client.com"
export Cors__AllowedServerOrigin="https://your-api.com"
```

## Docker Configuration

For containerized deployments, use environment variables or secrets:

```dockerfile
# In your Dockerfile or docker-compose.yml
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ConnectionStrings__DefaultConnection="your-connection-string"
ENV Jwt__Issuer="https://your-keycloak.com/realms/NationOH"
```

## Configuration Validation

The application includes built-in configuration validation. To enable it, add to your `Program.cs`:

```csharp
// Add configuration validation
builder.Services.AddConfigurationValidation();

// Configure strongly-typed options with validation
builder.Services.ConfigureWithValidation<JwtOptions>(builder.Configuration, JwtOptions.SectionName);
builder.Services.ConfigureWithValidation<CorsOptions>(builder.Configuration, CorsOptions.SectionName);

// Add validation hosted service
builder.Services.AddHostedService<ConfigurationValidationHostedService>();
```

## Troubleshooting

### Common Issues

1. **Database Connection Failures**
   - Check connection string format
   - Verify database server is accessible
   - Ensure credentials are correct

2. **Authentication Issues**
   - Verify Keycloak is running and accessible
   - Check JWT configuration matches Keycloak realm settings
   - Ensure client secrets are correct

3. **CORS Errors**
   - Verify allowed origins in CORS configuration
   - Check that client URLs match exactly (including protocol and port)

4. **Configuration Validation Errors**
   - Check application logs for detailed validation messages
   - Ensure all required configuration sections are present
   - Verify data types and ranges for configuration values

### Debug Mode

Enable debug logging to troubleshoot configuration issues:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "WebApi.Services.ConfigurationValidationService": "Debug"
    }
  }
}
```

### Health Checks

Use health check endpoints to verify configuration:
- `GET /health` - Overall application health
- `GET /health/ready` - Application readiness
- `GET /health/live` - Application liveness

## Best Practices

1. **Environment-Specific Configuration**
   - Use separate configuration files for each environment
   - Never commit secrets to version control
   - Use configuration validation to catch issues early

2. **Security**
   - Always use HTTPS in production
   - Implement proper CORS policies
   - Use strong authentication and authorization

3. **Monitoring**
   - Configure Application Insights for production
   - Set up health checks
   - Implement structured logging

4. **Performance**
   - Enable response compression and caching
   - Configure appropriate rate limits
   - Monitor resource usage

5. **Deployment**
   - Use infrastructure as code (IaC)
   - Implement proper CI/CD pipelines
   - Test configurations in staging before production

## Support

For additional support:
1. Check the application logs for detailed error messages
2. Review the Configuration.md file for detailed configuration reference
3. Use the built-in configuration validation to identify issues
4. Consult the ASP.NET Core documentation for advanced scenarios
