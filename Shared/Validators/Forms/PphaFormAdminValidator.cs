using FluentValidation;
using Shared.DTOs.Scheduling;
using Shared.Validators;

namespace Shared.Validators.Forms
{
    // Validator specific to PphaFormAdminDto. Extend with more rules as needed.
    public class PphaFormAdminValidator : AbstractValidator<PphaFormAdminDto>
    {
        public PphaFormAdminValidator()
        {
            Include(new ReferralDetailsValidator());
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.DateOfBirth)
                .NotNull().WithMessage("Date of birth is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");

            // Employment
            RuleFor(x => x.JobTitle)
                .NotEmpty().WithMessage("Job title is required");
            RuleFor(x => x.CompanyOrganisation)
                .NotEmpty().WithMessage("Organisation is required");
        }
        
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, property) =>
        {
            var ctx = ValidationContext<PphaFormAdminDto>.CreateWithOptions((PphaFormAdminDto)model, x => x.IncludeProperties(property));
            var result = await ValidateAsync(ctx);
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
