using FluentValidation;
using Shared.DTOs.Scheduling;
using Shared.Validators;

namespace Shared.Validators.Forms
{
    public class PphaFormClinWorkerValidator : AbstractValidator<PphaFormClinWorkerDto>
    {
        public PphaFormClinWorkerValidator()
        {
            Include(new ReferralDetailsValidator());
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.DateOfBirth).NotNull().WithMessage("Date of birth is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
            RuleFor(x => x.JobTitle).NotEmpty().WithMessage("Job title is required");
            RuleFor(x => x.CompanyOrganisation).NotEmpty().WithMessage("Company/organisation is required");
            RuleFor(x => x.Department).NotEmpty().WithMessage("Department is required");
            RuleFor(x => x.ContactNumber).NotEmpty().WithMessage("Contact number is required");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, property) =>
        {
            var ctx = ValidationContext<PphaFormClinWorkerDto>.CreateWithOptions((PphaFormClinWorkerDto)model, x => x.IncludeProperties(property));
            var result = await ValidateAsync(ctx);
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
