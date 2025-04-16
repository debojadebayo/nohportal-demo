using FluentValidation;
using Shared.DTOs.Scheduling;

namespace Shared.Validators
{
    public class ClinicianValidator : AbstractValidator<ClinicianDto>
    {
        public ClinicianValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Telephone).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.ClinicianType).IsInEnum();
            RuleFor(x => x.RegulatorType).IsInEnum();
            RuleFor(x => x.LicenceNumber).NotEmpty();
        }
    }
}