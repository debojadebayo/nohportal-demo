using ComposedHealthBase.Server.Auth;
using ComposedHealthBase.Server.Entities;
using Shared.Enums;


namespace Server.Modules.Scheduling.Entities
{
	public class Referral : BaseEntity<Referral>, IEntity, IAuditEntity, IAnchor
	{
	   public required string ReferralDetails { get; set; }
	   public required long EmployeeDocumentId { get; set; }
	   public required string Title { get; set; }
	   public ReferralStatusEnum ReferralStatus { get; set; } = Shared.Enums.ReferralStatusEnum.Pending;
	   public HashSet<Schedule> CalendarItems { get; set; } = new HashSet<Schedule>();
		public long CustomerId
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
		public long EmployeeId
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