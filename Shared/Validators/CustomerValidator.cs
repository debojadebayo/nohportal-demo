using FluentValidation;
using Shared.DTOs.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Company name is required.")
                .Length(2, 100).WithMessage("Company name must be between 2 and 100 characters.")
                .Must(n => n == null || !n.StartsWith(" ") && !n.EndsWith(" "))
                .WithMessage("Company name cannot start or end with spaces.");

            // UK phone number validation
            RuleFor(x => x.Telephone)
                .NotEmpty().WithMessage("Telephone number is required.")
                .Matches(@"^(\+44|0)(\d{9,10})$")
                .WithMessage("Please enter a valid UK telephone number.");

            // Better email validation
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("Please enter a valid email address.")
                .MaximumLength(100).WithMessage("Email must be at most 100 characters.")
                .Must(e => !e.Contains("example.com") && !e.Contains("test.com"))
                .WithMessage("Please provide an actual business email.");

            // Invoice email validation
            RuleFor(x => x.InvoiceEmail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Invoice email address is required.")
                .EmailAddress().WithMessage("Please enter a valid invoice email address.")
                .MaximumLength(100).WithMessage("Invoice email must be at most 100 characters.");

            // Address validation
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .Length(5, 200).WithMessage("Address must be between 5 and 200 characters.");

            // UK postcode validation
            RuleFor(x => x.Postcode)
                .NotEmpty().WithMessage("Postcode is required.")
                .Matches(@"^[A-Z]{1,2}\d[A-Z\d]? ?\d[A-Z]{2}$")
                .WithMessage("Please enter a valid UK postcode.");

            RuleFor(x => x.Site)
                .NotEmpty().WithMessage("Site location is required.")
                .Length(2, 100).WithMessage("Site must be between 2 and 100 characters.");

            // Industry validation with predefined values
            var validIndustries = new[] {
                "Healthcare", "Manufacturing", "Construction", "Education",
                "Finance", "Retail", "Technology", "Transportation", "Energy",
                "Agriculture", "Hospitality", "Other"
            };

            RuleFor(x => x.Industry)
                .NotEmpty().WithMessage("Industry is required.")
                .Must(i => validIndustries.Contains(i))
                .WithMessage($"Industry must be one of the following: {string.Join(", ", validIndustries)}");

            // OH Services validation
            RuleFor(x => x.OHServicesRequired)
                .NotEmpty().WithMessage("OH Services are required.")
                .Length(3, 200).WithMessage("OH Services must be between 3 and 200 characters.");

            // Number of employees validation
            RuleFor(x => x.NumberOfEmployees)
                .GreaterThan(0).WithMessage("Number of employees must be at least 1.")
                .LessThanOrEqualTo(100000).WithMessage("For companies with more than 100,000 employees, please contact us directly.");

            // Website validation with proper URI check
            RuleFor(x => x.Website)
                .NotEmpty().WithMessage("Website URL is required.")
                .MaximumLength(200).WithMessage("Website URL must be at most 200 characters.")
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out var uri) &&
                             (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
                .WithMessage("Website must be a valid URL starting with http:// or https://");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CustomerDto>.CreateWithOptions((CustomerDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}