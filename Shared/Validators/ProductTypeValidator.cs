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
    }
}