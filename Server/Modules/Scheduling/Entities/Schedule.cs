using ComposedHealthBase.Server.Auth;
using ComposedHealthBase.Server.Entities;
using Shared.Enums;


namespace Server.Modules.Scheduling.Entities
{
	public class Schedule : BaseEntity<Schedule>, IEntity, IAuditEntity, IAnchorable
	{
		public Guid ReferralId { get; set; }
		public Guid ClinicianId { get; set; }
		public Guid ProductId { get; set; }
	   public ScheduleStatusEnum Status { get; set; }
	   public AppointmentStatusEnum AppointmentStatus { get; set; }
		public DateTime? Start { get; set; }
		public DateTime? End { get; set; }
		public required string Title { get; set; }
		public required string Description { get; set; }
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

		public Guid AnchorId => ReferralId;
	}
}