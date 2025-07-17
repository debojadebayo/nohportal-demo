using FluentValidation;
using Shared.DTOs.Scheduling;
using Shared.Validators;

namespace Shared.Validators.Forms
{
    public class PphaFormClinStudentValidator : AbstractValidator<PphaFormClinStudentDto>
    {
        public PphaFormClinStudentValidator()
        {
            Include(new ReferralDetailsValidator());
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required");
            RuleFor(x => x.DateOfBirth).NotNull().WithMessage("Date of birth is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email is required");
            RuleFor(x => x.CourseAppliedFor).NotEmpty().WithMessage("Course applied for is required");
            RuleFor(x => x.University).NotEmpty().WithMessage("University is required");
            RuleFor(x => x.ContactNumber).NotEmpty().WithMessage("Contact number is required");
            RuleFor(x => x.ProposedStartDate).NotNull().WithMessage("Proposed start date is required");

        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, property) =>
        {
            var ctx = ValidationContext<PphaFormClinStudentDto>.CreateWithOptions((PphaFormClinStudentDto)model, x => x.IncludeProperties(property));
            var result = await ValidateAsync(ctx);
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
