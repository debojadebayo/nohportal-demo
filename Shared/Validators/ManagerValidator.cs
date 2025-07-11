using FluentValidation;
using Shared.DTOs.CRM;

namespace Shared.Validators
{
    public class ManagerValidator : AbstractValidator<ManagerDto>
    {
        public ManagerValidator()
        {
            // First Name: required, min 2, max 50, letters only
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MinimumLength(2).WithMessage("First Name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("First Name must be at most 50 characters.")
                .Matches("^[a-zA-Z\\-' ]+$").WithMessage("First Name can only contain letters, spaces, hyphens, and apostrophes.");

            // Last Name: required, min 2, max 50, letters only
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MinimumLength(2).WithMessage("Last Name must be at least 2 characters.")
                .MaximumLength(50).WithMessage("Last Name must be at most 50 characters.")
                .Matches("^[a-zA-Z\\-' ]+$").WithMessage("Last Name can only contain letters, spaces, hyphens, and apostrophes.");

            // Email: required, valid email, max 100
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.")
                .MaximumLength(100).WithMessage("Email must be at most 100 characters.")
                .Must(e => !e.Contains("example.com") && !e.Contains("test.com"))
                .WithMessage("Please provide an actual business email.");

            // UK phone number validation
            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("Telephone number is required.")
                .Matches(@"^(\+44|0)(\d{9,10})$")
                .WithMessage("Please enter a valid UK telephone number.");

            // Department: required, min 2, max 100
            RuleFor(x => x.Department)
                .NotEmpty().WithMessage("Department is required.")
                .MinimumLength(2).WithMessage("Department must be at least 2 characters.")
                .MaximumLength(100).WithMessage("Department must be at most 100 characters.");

            // CustomerId: required (will be set automatically but validate it exists)
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer ID is required.");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ManagerDto>.CreateWithOptions((ManagerDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
