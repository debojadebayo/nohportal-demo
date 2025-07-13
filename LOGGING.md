# Logging Configuration

This document explains the improved logging setup for the NationOH server.

## Overview

The logging configuration has been optimized to:
- Reduce Entity Framework verbosity in development
- Provide meaningful application logs
- Enable easy debugging when needed
- Structure logs for better readability

## Current Configuration

### Development Environment
- **Application logs**: `Information` level
- **Entity Framework**: `Warning` level (reduced from Debug)
- **Database connections**: Disabled (was extremely verbose)
- **Migration logs**: `Information` level (important events)
- **Module logs**: `Information` level
- **Request logs**: Available when needed

### Production Environment
- **Application logs**: `Information` level
- **Framework logs**: `Warning` level
- **JSON formatted logs** for structured logging
- **Reduced verbosity** for performance

## Key Improvements

1. **Reduced EF Core Noise**: Database connection and command logs are now disabled by default
2. **Structured Logging**: Better categorization of log sources
3. **Application Focus**: Your application logs (`Server.Modules.*`, `ComposedHealthBase.*`) are clearly visible
4. **Request Tracking**: Added request/response logging with timing
5. **Module Lifecycle**: Track module loading and configuration

## Log Categories

| Category | Development | Production | Purpose |
|----------|-------------|------------|---------|
| `Server.Modules.*` | Information | Information | Your application modules |
| `ComposedHealthBase.*` | Information | Information | Base framework logs |
| `Microsoft.EntityFrameworkCore` | Warning | Warning | EF Core (reduced noise) |
| `Microsoft.AspNetCore` | Warning | Warning | ASP.NET Core framework |
| Database connections | None | None | Disabled (too verbose) |
| Database commands | None | None | Disabled (too verbose) |

## Dynamic Configuration

Use the provided script to change logging levels when debugging:

```bash
# Enable verbose logging (includes SQL queries)
./logging-config.sh --verbose

# Reset to normal levels
./logging-config.sh --normal

# Minimal logging (warnings/errors only)
./logging-config.sh --minimal

# Enable database debugging
./logging-config.sh --database
```

## What You'll See Now

Instead of hundreds of EF Core debug messages, you'll see:

```
14:23:15.123 [Information] Starting NationOH Server application...
14:23:15.125 [Information] Environment: Development
14:23:15.130 [Information] Registering 5 modules: AuthModule, BillingModule, ClinicalModule, CRMModule, SchedulingModule
14:23:15.245 [Information] Configuring base module services...
14:23:15.250 [Information] Initializing role permission cache...
14:23:15.890 [Information] Role permission cache initialized successfully
14:23:15.895 [Information] Configuring Auth module services...
14:23:15.920 [Information] Running Auth module database migrations...
14:23:16.100 [Information] Auth module database setup completed
14:23:16.150 [Information] Module configuration and endpoint mapping completed
14:23:16.200 [Information] NationOH Server configured and ready to start
14:23:16.205 [Information] Now listening on: http://[::]:8080
```

## Debugging Database Issues

When you need to debug database problems:

1. **Enable verbose logging**: `./logging-config.sh --verbose`
2. **Reproduce the issue**
3. **Check logs** for SQL queries and timing
4. **Reset logging**: `./logging-config.sh --normal`

## Request Logging

The system now includes request logging that shows:
- HTTP method and path
- Status codes
- Response times
- Request IDs for correlation

Example:
```
14:25:30.123 [Information] Starting request GET /api/customers - RequestId: 0HN4...
14:25:30.245 [Information] Completed request GET /api/customers - Status: 200, Duration: 122ms, RequestId: 0HN4...
```

## Troubleshooting

If you need to see more details temporarily:

1. **Check current settings**: Look at `Server/WebApi/appsettings.Development.json`
2. **Use the script**: `./logging-config.sh --verbose` for detailed debugging
3. **Monitor specific categories**: Add specific loggers to appsettings if needed
4. **Reset when done**: `./logging-config.sh --normal` to avoid log spam

## Best Practices

1. **Keep normal logging for development** - it's much cleaner
2. **Enable verbose only when debugging** - too much noise otherwise
3. **Use structured logging** - include relevant context in log messages
4. **Monitor response times** - request logging shows performance issues
5. **Check module loading** - startup logs show configuration problems
