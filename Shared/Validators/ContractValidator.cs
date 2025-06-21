using FluentValidation;
using Shared.DTOs.CRM;

namespace Shared.Validators
{
    public class ContractValidator : AbstractValidator<ContractDto>
    {
        public ContractValidator()
        {
            RuleFor(x => x.Reference).NotEmpty();
            RuleFor(x => x.RepresentativeId).NotEmpty();
            RuleFor(x => x.StartTime).NotEmpty();
            RuleFor(x => x.EndTime).NotEmpty().GreaterThan(x => x.StartTime);
        }
    }
}