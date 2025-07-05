using FluentValidation;
using Shared.DTOs.Billing;

namespace Shared.Validators
{
    public class InvoiceFilterValidator : AbstractValidator<InvoiceFilterDto>
    {
        public InvoiceFilterValidator()
        {
            // From Date: required, cannot be in future
            RuleFor(x => x.FromDate)
                .NotEmpty().WithMessage("From Date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("From Date cannot be in the future.");

            // To Date: required, cannot be in future, must be after From Date
            RuleFor(x => x.ToDate)
                .NotEmpty().WithMessage("To Date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("To Date cannot be in the future.")
                .GreaterThanOrEqualTo(x => x.FromDate).WithMessage("To Date must be after or equal to From Date.");

            // Customer ID: required
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer selection is required.");

            // Product ID: optional, but if provided must not be empty
            RuleFor(x => x.ProductId)
                .NotEqual(Guid.Empty).WithMessage("Product ID cannot be empty.")
                .When(x => x.ProductId.HasValue);

            // Date range validation - ensure reasonable range
            RuleFor(x => x)
                .Must(x => (x.ToDate - x.FromDate).TotalDays <= 365)
                .WithMessage("Date range cannot exceed 365 days.")
                .When(x => x.FromDate != default && x.ToDate != default);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<InvoiceFilterDto>.CreateWithOptions((InvoiceFilterDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
