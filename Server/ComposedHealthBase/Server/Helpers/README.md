# ValidationHelper

The `ValidationHelper` class provides a centralized way to perform FluentValidation on DTOs across all command handlers using naming convention-based validator discovery.

## Overview

The helper automatically finds and executes the appropriate FluentValidation validator for a given DTO based on naming conventions, eliminating the need for manual validator registration or repeated validation code.

## Naming Convention

The validators must follow one of these naming conventions:

1. **Entity-based naming**: `{EntityType}Validator`
   - For `Manager` entity → `ManagerValidator`
   - For `Customer` entity → `CustomerValidator`

2. **DTO-based naming**: `{DtoTypeWithoutSuffix}Validator`
   - For `ManagerDto` → `ManagerValidator`
   - For `CustomerDto` → `CustomerValidator`

## Usage

### Method 1: Entity-Type Based Validation (Recommended)

Use this when you have access to both the entity type `T` and DTO type `TDto`:

```csharp
using ComposedHealthBase.Server.Helpers;

public async Task<Guid> Handle(TDto dto, ClaimsPrincipal user)
{
    // Validate the DTO using the entity type for validator discovery
    ValidationHelper.ValidateDto<T, TDto>(dto);
    
    // Continue with business logic...
    var newEntity = _mapper.Map(dto);
    // ...
}
```

### Method 2: DTO-Type Based Validation

Use this when you only have access to the DTO type:

```csharp
using ComposedHealthBase.Server.Helpers;

public void ValidateManagerDto(ManagerDto dto)
{
    // Validate the DTO using DTO type for validator discovery
    ValidationHelper.ValidateDto(dto);
    
    // Continue with business logic...
}
```

## Exception Handling

The `ValidationHelper` throws the following exceptions:

### `ValidationException`
Thrown when validation fails. Contains all validation errors from FluentValidation.

```csharp
try
{
    ValidationHelper.ValidateDto<Manager, ManagerDto>(managerDto);
}
catch (ValidationException ex)
{
    // Handle validation errors
    foreach (var error in ex.Errors)
    {
        Console.WriteLine($"{error.PropertyName}: {error.ErrorMessage}");
    }
}
```

### `InvalidOperationException`
Thrown when:
- The validator class cannot be found
- The validator instance cannot be created
- The assembly containing the DTO cannot be located

## Integration Examples

### CreateCommand Example

```csharp
public class CreateCommand<T, TDto, TContext> : ICreateCommand<T, TDto, TContext>
    where T : class, IEntity, IAuditEntity
    where TDto : IDto
{
    public async Task<Guid> Handle(TDto dto, ClaimsPrincipal user)
    {
        // Validate before processing
        ValidationHelper.ValidateDto<T, TDto>(dto);
        
        var newEntity = _mapper.Map(dto);
        _dbContext.Set<T>().Add(newEntity);
        await _dbContext.SaveChangesWithAuditAsync(user);
        return newEntity.Id;
    }
}
```

### UpdateCommand Example

```csharp
public class UpdateCommand<T, TDto, TContext> : IUpdateCommand<T, TDto, TContext>
    where T : class, IEntity, IAuditEntity
    where TDto : IDto
{
    public async Task Handle(TDto dto, ClaimsPrincipal user)
    {
        // Validate before processing
        ValidationHelper.ValidateDto<T, TDto>(dto);
        
        // Continue with update logic...
    }
}
```

## Validator Requirements

For the `ValidationHelper` to work correctly, ensure your validators:

1. **Follow the naming convention** (see above)
2. **Inherit from `AbstractValidator<TDto>`**
3. **Are located in the same assembly as the DTO** (typically the Shared assembly)
4. **Have a parameterless constructor**

### Example Validator

```csharp
using FluentValidation;
using Shared.DTOs.CRM;

namespace Shared.Validators
{
    public class ManagerValidator : AbstractValidator<ManagerDto>
    {
        public ManagerValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MinimumLength(2).WithMessage("First Name must be at least 2 characters.");
                
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.");
        }
    }
}
```

## Benefits

- **Centralized validation logic**: No need to duplicate validation code across commands
- **Automatic validator discovery**: No manual registration required
- **Type-safe**: Compile-time checking ensures validator compatibility
- **Consistent error handling**: Standardized exception types across all commands
- **Easy to maintain**: Changes to validation logic only need to be made in one place
- **Reusable**: Can be used in any command handler, API controller, or service

## Assembly Location

The `ValidationHelper` automatically looks for validators in the same assembly as the DTO being validated. This is typically the `Shared` assembly where both DTOs and validators are located.
