using FluentValidation;
using Shared.DTOs.Scheduling;

namespace Shared.Validators
{
    public class ScheduleValidator : AbstractValidator<ScheduleDto>
    {
        public ScheduleValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);
            RuleFor(x => x.ReferralId).GreaterThan(0);
            RuleFor(x => x.PatientId).GreaterThan(0);
            RuleFor(x => x.ClinicianId).GreaterThan(0);
            RuleFor(x => x.Start).NotEmpty();
            RuleFor(x => x.End).NotEmpty().GreaterThan(x => x.Start);
        }
    }
}