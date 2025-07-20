using FluentValidation;
using Shared.DTOs.Scheduling;
using Shared.Validators;

namespace Shared.Validators.Forms
{
    public class CaseReferralDetailsValidator : AbstractValidator<CaseReferralDetailsDto>
    {
        public CaseReferralDetailsValidator()
        {
            Include(new ReferralDetailsValidator());
            RuleFor(x => x.ReferralDate).NotNull().WithMessage("Referral date is required");
            RuleFor(x => x.ReferringManagerId).NotNull().WithMessage("Referring manager is required");
            RuleFor(x => x.CustomerId).NotNull().WithMessage("Customer is required");
            RuleFor(x => x.HrContactId).NotNull().WithMessage("HR contact is required");
            RuleFor(x => x.EmployeeId).NotNull().WithMessage("Employee is required");
            RuleFor(x => x.ReasonForReferral).NotEmpty().WithMessage("Reason for referral is required");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, property) =>
        {
            var ctx = ValidationContext<CaseReferralDetailsDto>.CreateWithOptions((CaseReferralDetailsDto)model, x => x.IncludeProperties(property));
            var result = await ValidateAsync(ctx);
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
