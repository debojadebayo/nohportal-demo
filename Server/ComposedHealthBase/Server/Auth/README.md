# Generic Authorization System Documentation

## Overview

This document describes the generic permission-based authorization system implemented for the BaseEndpoints. The system provides a clean, low-bloat way to secure all CRUD operations with permission-based authorization that integrates with the existing `IRolePermissionCacheService`.

## Features

- **Automatic Authorization**: All BaseEndpoints automatically get permission-based authorization applied
- **Generic Permission Mapping**: Permissions are automatically generated based on operation and entity type
- **Role-Based**: Uses existing role system with cached permission lookups
- **Low Bloat**: Minimal code changes required for implementation
- **Extensible**: Easy to add custom permissions and authorization logic

## Permission String Format

Permissions follow the format: `{Operation}{EntityType}`

Examples:
- `ViewSchedule` - Permission to view Schedule entities
- `CreateEmployee` - Permission to create Employee entities  
- `UpdateCustomer` - Permission to update Customer entities
- `DeleteInvoice` - Permission to delete Invoice entities

## Operations Mapping

| Endpoint Operation | Permission Operation | Example |
|-------------------|---------------------|---------|
| GET /GetAll | View | `ViewSchedule` |
| GET /GetById | View | `ViewEmployee` |
| POST /GetByIds | View | `ViewCustomer` |
| GET /Search | View | `ViewManager` |
| POST /Create | Create | `CreateCaseNote` |
| PUT /Update | Update | `UpdateContract` |
| POST /Delete | Delete | `DeleteInvoice` |

## Architecture Components

### 1. Authorization Handler

**File**: `Auth/AuthorizationHandlers/PermissionAuthorizationHandler.cs`

The main authorization handler that:
- Receives permission requirements
- Gets user roles from `IUserContextService`
- Checks permissions using `IRolePermissionCacheService`
- Grants or denies access based on role permissions

### 2. Permission Requirement

**File**: `Auth/Requirements/PermissionRequirement.cs`

Defines the authorization requirement that specifies which permission is needed.

### 3. Permission Constants

**File**: `Auth/Constants/PermissionConstants.cs`

Contains:
- Operation constants (Create, View, Update, Delete)
- `PermissionHelper` utility class for generating permission strings

### 4. Authorization Extensions

**File**: `Auth/Extensions/AuthorizationExtensions.cs`

Extension methods for registering permission policies in the DI container.

### 5. BaseEndpoints Integration

**File**: `Endpoints/BaseEndpoints.cs`

Modified to automatically apply authorization policies to all CRUD endpoints.

## Implementation Details

### BaseModule Registration

The authorization system is registered in `BaseModule.cs`:

```csharp
// Register the permission authorization handler
services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

// Register authorization policies
services.AddAuthorization(options =>
{
    // Add permission-based policies for common entities
    options.AddPermissionPoliciesForEntities(
        "Schedule", "Employee", "Customer", "Manager", 
        "CaseNote", "Contract", "Invoice", "Referral",
        "Clinician", "Product", "ProductType", "Document"
    );
});
```

### Automatic Endpoint Authorization

BaseEndpoints automatically applies authorization:

```csharp
// View permissions for GET operations
var viewPermission = PermissionHelper.GeneratePermission<T>(PermissionOperations.View);

group.MapGet("/GetAll", (...) => GetAll(...))
    .RequireAuthorization($"Permission:{viewPermission}");

// Create permissions for POST operations
var createPermission = PermissionHelper.GeneratePermission<T>(PermissionOperations.Create);

group.MapPost("/Create", (...) => Create(...))
    .RequireAuthorization($"Permission:{createPermission}");
```

## Usage Examples

### 1. Using RequirePermission Attribute

```csharp
[RequirePermission("ViewEmployee")]
public IActionResult GetEmployees() 
{
    return Ok();
}
```

### 2. Using Authorization Policy

```csharp
[Authorize(Policy = "Permission:CreateSchedule")]
public IActionResult CreateSchedule() 
{
    return Ok();
}
```

### 3. Programmatic Permission Checking

```csharp
public async Task<IActionResult> UpdateContract(
    [FromServices] IAuthorizationService authorizationService,
    ClaimsPrincipal user)
{
    var authResult = await authorizationService.AuthorizeAsync(
        user, null, "Permission:UpdateContract");

    if (!authResult.Succeeded)
        return Forbid();

    // Proceed with action
    return Ok();
}
```

### 4. Dynamic Permission Generation

```csharp
public async Task<IActionResult> DeleteEntity<TEntity>(
    [FromServices] IAuthorizationService authorizationService,
    ClaimsPrincipal user)
{
    var permission = PermissionHelper.GeneratePermission<TEntity>(PermissionOperations.Delete);
    
    var authResult = await authorizationService.AuthorizeAsync(
        user, null, $"Permission:{permission}");

    if (!authResult.Succeeded)
        return Forbid();

    return Ok();
}
```

## Permission Setup

### Database Setup

Ensure your permission system database has entries for the required permissions:

```sql
-- Example permissions for Schedule entity
INSERT INTO Permissions (Name) VALUES ('ViewSchedule');
INSERT INTO Permissions (Name) VALUES ('CreateSchedule');
INSERT INTO Permissions (Name) VALUES ('UpdateSchedule');
INSERT INTO Permissions (Name) VALUES ('DeleteSchedule');

-- Link permissions to roles
INSERT INTO RolePermissions (RoleId, PermissionId) 
SELECT r.Id, p.Id 
FROM Roles r, Permissions p 
WHERE r.Name = 'manager' AND p.Name IN ('ViewSchedule', 'CreateSchedule', 'UpdateSchedule');
```

### Role Permission Cache

The system relies on `IRolePermissionCacheService` to efficiently check permissions. Ensure this service is:

1. Properly implemented
2. Registered in DI container
3. Initialized at application startup
4. Refreshed when permissions change

## Security Considerations

1. **Principle of Least Privilege**: Users should only have permissions they need
2. **Permission Granularity**: Each operation on each entity type has its own permission
3. **Role-Based Access**: Permissions are granted through roles, not directly to users
4. **Caching**: Permission lookups are cached for performance
5. **Fail Secure**: Unknown permissions default to denied access

## Performance

- **Cached Lookups**: Uses `IRolePermissionCacheService` for fast permission checks
- **Pre-registered Policies**: Common permissions are pre-registered at startup
- **Minimal Overhead**: Authorization adds minimal performance impact

## Troubleshooting

### Common Issues

1. **403 Forbidden Errors**: Check if user has required role and permission
2. **500 Internal Server Error**: Verify `IRolePermissionCacheService` is working
3. **Permission Not Found**: Ensure permission is registered in database and cache

### Debugging

1. Check user claims and roles in JWT token
2. Verify permission exists in database
3. Confirm role has permission in `RolePermissions` table
4. Test permission cache service directly
5. Enable authorization logging in ASP.NET Core

## Extending the System

### Adding New Entity Types

1. Add entity permissions to database
2. Register policies in `BaseModule.cs`:

```csharp
options.AddPermissionPoliciesForEntity("YourNewEntity");
```

### Custom Authorization Logic

Create custom authorization handlers for complex scenarios:

```csharp
public class CustomAuthorizationHandler : AuthorizationHandler<CustomRequirement>
{
    protected override Task HandleRequirementAsync(...)
    {
        // Custom logic here
    }
}
```

### Custom Requirements

```csharp
public class CustomRequirement : IAuthorizationRequirement
{
    public string CustomProperty { get; set; }
}
```

## Migration from Existing System

1. Map existing permissions to new format
2. Update role assignments
3. Test each endpoint with appropriate roles
4. Remove old authorization attributes/middleware
5. Update client-side permission checks

This system provides a robust, scalable foundation for authorization that can grow with your application needs while maintaining security and performance.
