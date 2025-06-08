using ComposedHealthBase.Server.Auth;
using ComposedHealthBase.Server.Entities;
using Shared.Enums;


namespace Server.Modules.Scheduling.Entities
{
	public class Schedule : BaseEntity<Schedule>, IEntity, IAnchorable
	{
		public long ReferralId { get; set; }
		public long ClinicianId { get; set; }
		public long ProductId { get; set; }
		public ScheduleStatusEnum Status { get; set; }
		public DateTime? Start { get; set; }
		public DateTime? End { get; set; }
		public required string Title { get; set; }
		public required string Description { get; set; }
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

		public long AnchorId
		{
			get
			{
				return ReferralId;
			}
			set
			{
				ReferralId = value;
			}
		}
	}
}