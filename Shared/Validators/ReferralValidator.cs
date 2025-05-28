using FluentValidation;
using Shared.DTOs.Scheduling;

namespace Shared.Validators
{
    public class ReferralValidator : AbstractValidator<ReferralDto>
    {
        public ReferralValidator()
        {
            RuleFor(x => x.TenantId).GreaterThan(0);
            RuleFor(x => x.SubjectId).GreaterThan(0);
            RuleFor(x => x.ReferralDetails).NotEmpty();
        }
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ReferralDto>.CreateWithOptions((ReferralDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}