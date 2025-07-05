using FluentValidation;
using Shared.DTOs.Billing;

namespace Shared.Validators
{
    public class InvoiceValidator : AbstractValidator<InvoiceDto>
    {
        public InvoiceValidator()
        {
            // Invoice Number: required, format validation
            RuleFor(x => x.InvoiceNumber)
                .NotEmpty().WithMessage("Invoice Number is required.")
                .MaximumLength(50).WithMessage("Invoice Number must be at most 50 characters.")
                .Matches(@"^INV-\d{4}-\d{6}$").WithMessage("Invoice Number must be in format INV-YYYY-NNNNNN");

            // Invoice Date: required, cannot be in future
            RuleFor(x => x.InvoiceDate)
                .NotEmpty().WithMessage("Invoice Date is required.")
                .LessThanOrEqualTo(DateTime.Today).WithMessage("Invoice Date cannot be in the future.");

            // Due Date: optional, but if provided must be after invoice date
            RuleFor(x => x.DueDate)
                .GreaterThan(x => x.InvoiceDate).WithMessage("Due Date must be after Invoice Date.")
                .When(x => x.DueDate.HasValue);

            // Amounts: must be non-negative
            RuleFor(x => x.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Total Amount must be non-negative.");

            RuleFor(x => x.NetAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Net Amount must be non-negative.");

            RuleFor(x => x.TaxAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Tax Amount must be non-negative.");

            // Tax Rate: must be between 0 and 1 (representing percentage as decimal)
            RuleFor(x => x.TaxRate)
                .InclusiveBetween(0, 1).WithMessage("Tax Rate must be between 0% and 100%.");

            // Status: required, must be valid value
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.")
                .Must(s => new[] { "Draft", "Sent", "Paid", "Overdue", "Cancelled", "Finalized" }.Contains(s))
                .WithMessage("Status must be one of: Draft, Sent, Paid, Overdue, Cancelled, Finalized");

            // Customer ID: required
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("Customer is required.");

            // Date range validation
            RuleFor(x => x.ToDate)
                .GreaterThanOrEqualTo(x => x.FromDate).WithMessage("To Date must be after or equal to From Date.");

            // Notes: optional, but if provided must not exceed limit
            RuleFor(x => x.Notes)
                .MaximumLength(1000).WithMessage("Notes must be at most 1000 characters.")
                .When(x => !string.IsNullOrEmpty(x.Notes));

            // Line Items: at least one required for non-draft invoices
            RuleFor(x => x.LineItems)
                .NotEmpty().WithMessage("At least one line item is required.")
                .When(x => x.Status != "Draft");

            // Amount calculations consistency
            RuleFor(x => x)
                .Must(x => Math.Abs(x.TotalAmount - (x.NetAmount + x.TaxAmount)) < 0.01m)
                .WithMessage("Total Amount must equal Net Amount plus Tax Amount.")
                .When(x => x.LineItems.Any());

            RuleFor(x => x)
                .Must(x => Math.Abs(x.TaxAmount - (x.NetAmount * x.TaxRate)) < 0.01m)
                .WithMessage("Tax Amount must equal Net Amount multiplied by Tax Rate.")
                .When(x => x.LineItems.Any());
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<InvoiceDto>.CreateWithOptions((InvoiceDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
