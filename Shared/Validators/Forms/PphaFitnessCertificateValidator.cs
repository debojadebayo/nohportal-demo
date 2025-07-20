using FluentValidation;
using Shared.DTOs.Scheduling;
using Shared.Validators;

namespace Shared.Validators.Forms
{
    public class PphaFitnessCertificateValidator : AbstractValidator<PphaFitnessCertificateDto>
    {
        public PphaFitnessCertificateValidator()
        {
            Include(new ReferralDetailsValidator());
            RuleFor(x => x.Name).NotEmpty().WithMessage("Employee name is required");
            RuleFor(x => x.DateOfBirth).NotNull().WithMessage("Date of birth is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
            RuleFor(x => x.ContactNumber).NotEmpty().WithMessage("Contact number is required");
            RuleFor(x => x.Company).NotEmpty().WithMessage("Company/organisation is required");
            RuleFor(x => x.Department).NotEmpty().WithMessage("Department is required");
            RuleFor(x => x.RoleAppliedFor).NotEmpty().WithMessage("Job title is required");
            RuleFor(x => x.DateOfAssessment).NotNull().WithMessage("Date of assessment is required");
        }
        
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, property) =>
        {
            var ctx = ValidationContext<PphaFitnessCertificateDto>.CreateWithOptions((PphaFitnessCertificateDto)model, x => x.IncludeProperties(property));
            var result = await ValidateAsync(ctx);
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
