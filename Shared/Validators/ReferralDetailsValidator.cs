using FluentValidation;
using Shared.DTOs.Scheduling;

namespace Shared.Validators
{
    public class ReferralDetailsValidator : AbstractValidator<ReferralDetailsDto>
    {
        public ReferralDetailsValidator()
        {
            RuleFor(x => x.ReferralDate).NotNull().WithMessage("Referral date is required");
            RuleFor(x => x.ReferringManagerId).NotNull().WithMessage("Referring manager is required");
            RuleFor(x => x.CustomerId).NotNull().WithMessage("Customer is required");
            RuleFor(x => x.HrContactId).NotNull().WithMessage("HR contact is required");
            RuleFor(x => x.EmployeeId).NotNull().WithMessage("Employee is required");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ReferralDetailsDto>.CreateWithOptions((ReferralDetailsDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
