using FluentValidation;
using Shared.DTOs.CRM;

namespace Shared.Validators
{
    public class ProductValidator : AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(x => x.ProductType).NotNull();
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.EndTime).NotEmpty().GreaterThan(x => x.StartTime);
        }
    }
}