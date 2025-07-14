using FluentValidation;
using System.Reflection;

namespace ComposedHealthBase.Server.Helpers
{
    /// <summary>
    /// Helper class for performing FluentValidation on DTOs using naming convention-based validator discovery.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Validates a DTO using its corresponding FluentValidation validator.
        /// The validator is expected to follow the naming convention: {EntityType}Validator
        /// where EntityType is derived from the entity type T that corresponds to the DTO.
        /// </summary>
        /// <typeparam name="T">The entity type that corresponds to the DTO</typeparam>
        /// <typeparam name="TDto">The DTO type to validate</typeparam>
        /// <param name="dto">The DTO instance to validate</param>
        /// <exception cref="ValidationException">Thrown when validation fails</exception>
        /// <exception cref="InvalidOperationException">Thrown when the validator cannot be found or created</exception>
        public static void ValidateDto<T, TDto>(TDto dto)
        {
            // Get the entity type name and construct the validator type name
            var entityTypeName = typeof(T).Name;
            var validatorTypeName = $"{entityTypeName}Validator";
            
            // Look for the validator in the same assembly as the DTO (typically Shared assembly)
            var sharedAssembly = Assembly.GetAssembly(typeof(TDto));
            if (sharedAssembly == null)
            {
                throw new InvalidOperationException($"Could not find assembly containing DTO type {typeof(TDto).Name}");
            }
            
            // Find the validator type
            var validatorType = sharedAssembly.GetTypes()
                .FirstOrDefault(t => t.Name == validatorTypeName && 
                                   t.IsClass && 
                                   !t.IsAbstract &&
                                   typeof(IValidator<TDto>).IsAssignableFrom(t));
            
            if (validatorType == null)
            {
                throw new InvalidOperationException(
                    $"Could not find validator '{validatorTypeName}' for entity type '{entityTypeName}' " +
                    $"and DTO type '{typeof(TDto).Name}' in assembly '{sharedAssembly.FullName}'. " +
                    $"Ensure the validator class exists and follows the naming convention.");
            }
            
            // Create an instance of the validator
            var validatorInstance = Activator.CreateInstance(validatorType) as IValidator<TDto>;
            if (validatorInstance == null)
            {
                throw new InvalidOperationException($"Could not create instance of validator '{validatorTypeName}'");
            }
            
            // Perform validation
            var validationResult = validatorInstance.Validate(dto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }
    }
}
