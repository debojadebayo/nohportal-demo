using FluentValidation;
using Shared.DTOs.Clinical;
using Shared.Enums;

namespace Shared.Validators
{
    public class CaseNoteValidator : AbstractValidator<CaseNoteDto>
    {
        public CaseNoteValidator()
        {
            RuleFor(x => x.CaseNotes)
                .NotEmpty().WithMessage("Case notes are required.")
                .MinimumLength(10).WithMessage("Case notes must be at least 10 characters long.")
                .MaximumLength(10000).WithMessage("Case notes must be at most 10000 characters long.");

            RuleFor(x => x.AppointmentType)
                .IsInEnum().WithMessage("Please select a valid appointment type.");

            RuleFor(x => x.FitForWorkStatus)
                .IsInEnum().WithMessage("Please select a valid fit for work status.");

            RuleFor(x => x.RecommendedAdjustments)
                .MaximumLength(2000).WithMessage("Recommended adjustments must be at most 2000 characters.");

            RuleFor(x => x.FollowUpDate)
                .GreaterThan(DateTime.Today).WithMessage("Follow up date must be in the future.")
                .When(x => x.IsFollowUpNeeded && x.FollowUpDate.HasValue);

            RuleFor(x => x.FollowUpReasonForReferral)
                .NotEmpty().WithMessage("Follow up reason is required when follow up is needed.")
                .MaximumLength(1000).WithMessage("Follow up reason must be at most 1000 characters.")
                .When(x => x.IsFollowUpNeeded);

            RuleFor(x => x.FollowUpDate)
                .NotNull().WithMessage("Follow up date is required when follow up is needed.")
                .When(x => x.IsFollowUpNeeded);

            RuleFor(x => x.ClinicianId)
                .NotEmpty().WithMessage("Clinician selection is required.");
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CaseNoteDto>.CreateWithOptions((CaseNoteDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
