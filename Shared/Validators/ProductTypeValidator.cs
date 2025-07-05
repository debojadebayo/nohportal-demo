using FluentValidation;
using Shared.DTOs.CRM;

namespace Shared.Validators
{
    public class ProductTypeValidator : AbstractValidator<ProductTypeDto>
    {
        public ProductTypeValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.DefaultPrice).GreaterThanOrEqualTo(0);
            RuleFor(x => x.ChargeCode).NotEmpty();
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.EndTime).NotEmpty().GreaterThan(x => x.StartTime);
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ProductTypeDto>.CreateWithOptions((ProductTypeDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}