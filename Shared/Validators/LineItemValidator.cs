using FluentValidation;
using Shared.DTOs.Billing;

namespace Shared.Validators
{
    public class LineItemValidator : AbstractValidator<LineItemDto>
    {
        public LineItemValidator()
        {
            // Invoice ID: required
            RuleFor(x => x.InvoiceId)
                .NotEmpty().WithMessage("Invoice ID is required.");

            // Schedule ID: required
            RuleFor(x => x.ScheduleId)
                .NotEmpty().WithMessage("Schedule ID is required.");

            // Product ID: required
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Product ID is required.");

            // Product Name: required
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product Name is required.")
                .MaximumLength(200).WithMessage("Product Name must be at most 200 characters.");

            // Product Charge Code: required
            RuleFor(x => x.ProductChargeCode)
                .NotEmpty().WithMessage("Product Charge Code is required.")
                .MaximumLength(50).WithMessage("Product Charge Code must be at most 50 characters.");

            // Unit Price: must be non-negative
            RuleFor(x => x.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Unit Price must be non-negative.");

            // Quantity: must be positive
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be at least 1.");

            // Line Total: must be non-negative and equal to Unit Price * Quantity
            RuleFor(x => x.LineTotal)
                .GreaterThanOrEqualTo(0).WithMessage("Line Total must be non-negative.");

            RuleFor(x => x)
                .Must(x => Math.Abs(x.LineTotal - (x.UnitPrice * x.Quantity)) < 0.01m)
                .WithMessage("Line Total must equal Unit Price multiplied by Quantity.");

            // Service Date: required, cannot be in future
            RuleFor(x => x.ServiceDate)
                .NotEmpty().WithMessage("Service Date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Service Date cannot be in the future.");

            // Description: optional, but if provided must not exceed limit
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description must be at most 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<LineItemDto>.CreateWithOptions((LineItemDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
