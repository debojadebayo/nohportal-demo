using FluentValidation;
using Shared.DTOs.CRM;

namespace Shared.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerDto>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(x => x.Name)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Length(1, 15);

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.InvoiceEmail)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Address)
                .NotEmpty()
                .Length(1, 200);

            RuleFor(x => x.Postcode)
                .NotEmpty()
                .Length(1, 20);

            RuleFor(x => x.Site)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(x => x.Industry)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(x => x.OHServicesRequired)
                .NotEmpty()
                .Length(1, 200);

            RuleFor(x => x.NumberOfEmployees)
                .GreaterThan(0)
                .LessThanOrEqualTo(10000);

            RuleFor(x => x.Website)
                .NotEmpty()
                .Length(1, 200)
                .Must(value => Uri.IsWellFormedUriString(value, UriKind.Absolute))
                .WithMessage("Website must be a valid URL.");
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