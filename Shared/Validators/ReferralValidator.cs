using FluentValidation;
using Shared.DTOs.Scheduling;

namespace Shared.Validators
{
    public class ReferralValidator : AbstractValidator<ReferralDto>
    {
        public ReferralValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);
            RuleFor(x => x.PatientId).GreaterThan(0);
            RuleFor(x => x.ReferralDetails).NotEmpty();
        }
    }
}