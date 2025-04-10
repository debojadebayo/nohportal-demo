using FluentValidation;
using Shared.DTOs.CRM;

namespace Shared.Validators
{
    public class EmployeeValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeValidator()
        {
            RuleFor(e => e.FirstName).NotEmpty().WithMessage("First Name is required.");
            RuleFor(e => e.LastName).NotEmpty().WithMessage("Last Name is required.");
            RuleFor(e => e.DOB).NotEmpty().WithMessage("Date of Birth is required.");
            RuleFor(e => e.Address1).NotEmpty().WithMessage("Address Line 1 is required.");
            RuleFor(e => e.Address2).NotEmpty().WithMessage("Address Line 2 is required.");
            RuleFor(e => e.Address3).NotEmpty().WithMessage("Address Line 3 is required.");
            RuleFor(e => e.Postcode).NotEmpty().WithMessage("Postcode is required.");
            RuleFor(e => e.Email).NotEmpty().EmailAddress().WithMessage("A valid Email is required.");
            RuleFor(e => e.Telephone).NotEmpty().WithMessage("Telephone is required.");
            RuleFor(e => e.Id).NotEmpty().WithMessage("Company ID is required.");
            RuleFor(e => e.CompanyName).NotEmpty().WithMessage("Company Name is required.");
            RuleFor(e => e.JobRole).NotEmpty().WithMessage("Job Role is required.");
            RuleFor(e => e.Department).NotEmpty().WithMessage("Department is required.");
            RuleFor(e => e.LineManager).NotEmpty().WithMessage("Line Manager is required.");
        }

        private async Task<bool> IsUniqueAsync(string email)
        {
            // Simulates a long running HTTP call
            await Task.Delay(2000);
            return email.ToLower() != "employee@test.com";
        }

        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<EmployeeDto>.CreateWithOptions((EmployeeDto)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return Array.Empty<string>();
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
}
