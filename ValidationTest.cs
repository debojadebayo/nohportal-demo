using System;
using System.Security.Claims;
using FluentValidation;
using Shared.DTOs.CRM;
using ComposedHealthBase.Server.Helpers;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Testing CreateSubjectCommand validation...");
            
            // Test with valid ManagerDto
            var validManager = new ManagerDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@company.com",
                Telephone = "123-456-7890",
                Department = "IT"
            };
            
            try
            {
                // Use the ValidationHelper instead of custom logic
                ValidationHelper.ValidateDto(validManager);
                Console.WriteLine("✓ Valid ManagerDto passed validation");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"✗ Valid ManagerDto failed validation: {ex.Message}");
            }
            
            // Test with invalid ManagerDto (missing required fields)
            var invalidManager = new ManagerDto
            {
                FirstName = "", // Invalid - empty
                LastName = "Doe",
                Email = "invalid-email", // Invalid format
                Telephone = "123-456-7890",
                Department = "IT"
            };
            
            try
            {
                // Use the ValidationHelper instead of custom logic
                ValidationHelper.ValidateDto(invalidManager);
                Console.WriteLine("✗ Invalid ManagerDto incorrectly passed validation");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"✓ Invalid ManagerDto correctly failed validation: {ex.Errors.Count} errors found");
                foreach (var error in ex.Errors)
                {
                    Console.WriteLine($"  - {error.PropertyName}: {error.ErrorMessage}");
                }
            }
        }
    }
}
