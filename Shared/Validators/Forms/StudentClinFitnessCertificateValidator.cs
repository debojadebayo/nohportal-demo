using FluentValidation;
using Shared.DTOs.Scheduling;
using Shared.Validators;

namespace Shared.Validators.Forms
{
    public class StudentClinFitnessCertificateValidator : AbstractValidator<StudentClinFitnessCertificateDto>
    {
        public StudentClinFitnessCertificateValidator()
        {
            Include(new ReferralDetailsValidator());
            RuleFor(x => x.StudentName).NotEmpty().WithMessage("Student name is required");
            RuleFor(x => x.CourseAppliedFor).NotEmpty().WithMessage("Course is required");
            RuleFor(x => x.University).NotEmpty().WithMessage("University is required");
            RuleFor(x => x.DateOfAssessment).NotNull().WithMessage("Date of assessment is required");
            RuleFor(x => x.ClinicianName).NotEmpty().WithMessage("Clinician name is required");
        }
        
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, property) =>
        {
            var ctx = ValidationContext<StudentClinFitnessCertificateDto>.CreateWithOptions((StudentClinFitnessCertificateDto)model, x => x.IncludeProperties(property));
            var result = await ValidateAsync(ctx);
            return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
        };
    }

}
