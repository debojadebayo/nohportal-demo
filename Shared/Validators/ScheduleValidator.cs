using FluentValidation;
using Shared.DTOs.Scheduling;

namespace Shared.Validators
{
    public class ScheduleValidator : AbstractValidator<ScheduleDto>
    {
        public ScheduleValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.ReferralId).NotEmpty();
            RuleFor(x => x.EmployeeId).NotEmpty();
            RuleFor(x => x.ClinicianId).NotEmpty();
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.EndTime).NotEmpty().GreaterThan(x => x.StartTime);
        }
    }
}