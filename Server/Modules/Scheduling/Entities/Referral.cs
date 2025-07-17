using ComposedHealthBase.Server.Auth;
using ComposedHealthBase.Server.Entities;
using Shared.DTOs.Scheduling;
using Shared.Enums;
using System.Text.Json;


namespace Server.Modules.Scheduling.Entities
{
    public class Referral : BaseEntity<Referral>, IEntity, IAuditEntity, IAnchor
    {
        public required string ReferralDetails { get; set; }
        public required string Title { get; set; }
        public ReferralStatusEnum ReferralStatus { get; set; } = Shared.Enums.ReferralStatusEnum.Pending;
        public ReferralTypeEnum ReferralType { get; set; } = ReferralTypeEnum.CaseReferral;
        
        // JSON field to store all referral form details
        public JsonDocument? Details { get; set; }
        public HashSet<Schedule> CalendarItems { get; set; } = new HashSet<Schedule>();
        public Guid[] RelatedDocumentIds { get; set; } = Array.Empty<Guid>();
        public Guid CustomerId
        {
            get
            {
                return TenantId;
            }
            set
            {
                TenantId = value;
            }
        }
        public Guid EmployeeId
        {
            get
            {
                return SubjectId;
            }
            set
            {
                SubjectId = value;
            }
        }
    }
}