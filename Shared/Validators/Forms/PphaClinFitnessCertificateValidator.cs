using FluentValidation;
using Shared.DTOs.Scheduling;
using Shared.Validators;

namespace Shared.Validators.Forms
{
    public class PphaClinFitnessCertificateValidator : AbstractValidator<PphaClinFitnessCertificateDto>
    {
        public PphaClinFitnessCertificateValidator()
        {
            Include(new ReferralDetailsValidator());
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.RoleAppliedFor).NotEmpty().WithMessage("Job title is required");
            RuleFor(x => x.Company).NotEmpty().WithMessage("Company/organisation is required");
            RuleFor(x => x.Department).NotEmpty().WithMessage("Department is required");
            RuleFor(x => x.ContactNumber).NotEmpty().WithMessage("Contact number is required");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, property) =>
        {
            var ctx = ValidationContext<PphaClinFitnessCertificateDto>.CreateWithOptions((PphaClinFitnessCertificateDto)model, x => x.IncludeProperties(property));
            var result = await ValidateAsync(ctx);
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
