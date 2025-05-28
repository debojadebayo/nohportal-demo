using FluentValidation;
using Shared.DTOs.CRM;
using System.Text.RegularExpressions;

namespace Shared.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidator()
        {
            // First Name: required, min 2, max 50, letters only
            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MinimumLength(2).WithMessage("First Name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("First Name must be at most 50 characters.")
                .Matches("^[a-zA-Z\\-' ]+$").WithMessage("First Name can only contain letters, spaces, hyphens, and apostrophes.");

            // Last Name: required, min 2, max 50, letters only
            RuleFor(e => e.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MinimumLength(2).WithMessage("Last Name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("Last Name must be at most 50 characters.")
                .Matches("^[a-zA-Z\\-' ]+$").WithMessage("Last Name can only contain letters, spaces, hyphens, and apostrophes.");

            // DOB: required, must be at least 16 years old
            RuleFor(e => e.DOB)
                .NotEmpty().WithMessage("Date of Birth is required.")
                .Must(dob => dob != null && dob <= DateTime.Today.AddYears(-18))
                .WithMessage("Employee must be at least 18 years old.");

            // Address1: required, min 5, max 100
            RuleFor(e => e.Address1)
                .NotEmpty().WithMessage("Address Line 1 is required.")
                .MinimumLength(5).WithMessage("Address Line 1 must be at least 5 characters.")
                .MaximumLength(100).WithMessage("Address Line 1 must be at most 100 characters.");

            // Address2: optional, max 100
            RuleFor(e => e.Address2)
                .MaximumLength(100).WithMessage("Address Line 2 must be at most 100 characters.");

            // Postcode: required, UK postcode regex
            RuleFor(e => e.Postcode)
                .NotEmpty().WithMessage("Postcode is required.")
                .Matches(@"^[A-Z]{1,2}\d[A-Z\d]? ?\d[A-Z]{2}$")
                .WithMessage("Postcode must be a valid UK postcode.");

            // Email: required, valid email, max 100
            RuleFor(e => e.Email)
                .NotEmpty().WithMessage("A valid Email is required.")
                .EmailAddress().WithMessage("A valid Email is required.")
                .MaximumLength(100).WithMessage("Email must be at most 100 characters.");

            // Telephone: required, UK phone regex
            RuleFor(e => e.Telephone)
                .NotEmpty().WithMessage("Telephone is required.")
                .Matches(@"^(\+44\s?7\d{3}|\(?07\d{3}\)?)\s?\d{3}\s?\d{3}$")
                .WithMessage("Telephone must be a valid UK mobile number.");

            // CustomerId: required
            RuleFor(e => e.TenantId)
                .NotEmpty().WithMessage("Company is required.");

            // JobRole: required, must be in allowed list
            RuleFor(e => e.JobRole)
                .NotEmpty().WithMessage("Job Role is required.");

            // Department: required, must be in allowed list
            RuleFor(e => e.Department)
                .NotEmpty().WithMessage("Department is required.");

            // LineManager: required, cannot be same as employee's own name
            RuleFor(e => e.LineManager)
                .NotEmpty().WithMessage("Line Manager is required.")
                .Must((e, lm) => !string.Equals($"{e.FirstName} {e.LastName}", lm, StringComparison.OrdinalIgnoreCase))
                .WithMessage("Line Manager cannot be the same as the employee.");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<EmployeeDto>.CreateWithOptions((EmployeeDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
