# Client Configuration

This document describes the configuration options for the NationOH Client application.

## Configuration Files

The client uses JSON configuration files located in the `wwwroot` directory:

- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Development environment overrides
- `appsettings.Production.json` - Production environment overrides

## Configuration Sections

### Api Section

Configures the API client settings:

```json
{
  "Api": {
    "BaseUrl": "http://localhost:5003/",
    "AuthorizedUrls": ["http://localhost:5003"]
  }
}
```

- `BaseUrl`: The base URL for the API server
- `AuthorizedUrls`: Array of URLs that are authorized for authentication

### Oidc Section

Configures OpenID Connect authentication:

```json
{
  "Oidc": {
    "Authority": "http://localhost:8180/realms/NationOH",
    "ClientId": "nationoh_client",
    "ResponseType": "code",
    "DefaultScopes": ["nationoh_webapi-scope"],
    "RoleClaim": "role"
  }
}
```

- `Authority`: The OIDC authority URL
- `ClientId`: The client ID for authentication
- `ResponseType`: The OAuth response type (typically "code")
- `DefaultScopes`: Array of default scopes to request
- `RoleClaim`: The claim type used for roles

### Culture Section

Configures localization settings:

```json
{
  "Culture": {
    "DefaultCulture": "en-GB"
  }
}
```

- `DefaultCulture`: The default culture for the application (affects currency formatting, date formatting, etc.)

## Environment-Specific Configuration

The application automatically loads the appropriate configuration file based on the environment:

- **Development**: Uses `appsettings.Development.json` for local development
- **Production**: Uses `appsettings.Production.json` for production deployment

## Configuration Classes

The configuration is mapped to strongly-typed classes:

- `ApiOptions` - Maps to the "Api" section
- `OidcOptions` - Maps to the "Oidc" section  
- `CultureOptions` - Maps to the "Culture" section

These classes are located in the `Client.Configuration` namespace.
