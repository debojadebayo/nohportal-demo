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

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<ClinicianDto>.CreateWithOptions((ClinicianDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}