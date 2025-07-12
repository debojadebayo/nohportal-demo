using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using Shared.DTOs;
using Shared.Enums;

namespace Shared.DTOs.Scheduling
{
    public class ReferralDto : BaseDto<ReferralDto>, IDto, IAuditDto, ILazyLookup
    {
        public string ReferralDetails { get; set; } = string.Empty;
        public Guid EmployeeDocumentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public ReferralStatusEnum ReferralStatus { get; set; } = Shared.Enums.ReferralStatusEnum.Pending;
        public string DisplayName => $"{Title} - {ReferralDetails}";
        public Guid CustomerId { get; set; }
        public Guid EmployeeId { get; set; }
        public List<Guid> RelatedDocumentIds { get; set; } = new List<Guid>();
    }
}