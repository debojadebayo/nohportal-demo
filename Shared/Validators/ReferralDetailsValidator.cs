using FluentValidation;
using Shared.DTOs.Scheduling;

namespace Shared.Validators
{
    public class ReferralDetailsValidator : AbstractValidator<IReferralDetailsDto>
    {
        public ReferralDetailsValidator()
        {
            RuleFor(x => x.ReferralId).NotNull().WithMessage("Referral ID is required");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<IReferralDetailsDto>.CreateWithOptions((IReferralDetailsDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
