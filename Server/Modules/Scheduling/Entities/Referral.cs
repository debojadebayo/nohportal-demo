using ComposedHealthBase.Server.Auth;
using ComposedHealthBase.Server.Entities;


namespace Server.Modules.Scheduling.Entities
{
	public class Referral : BaseEntity<Referral>, IEntity, IAuditEntity, IAnchor
	{
		public required string ReferralDetails { get; set; }
		public required string DocumentId { get; set; }
		public required string Title { get; set; }
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