using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
    public interface IReferralDetailsDto : IDto, IAuditDto
    {
        Guid? ReferralId { get; set; }
    }

}