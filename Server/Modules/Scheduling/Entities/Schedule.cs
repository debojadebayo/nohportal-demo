using ComposedHealthBase.Server.Entities;
using Shared.Enums;
using Server.Modules.CommonModule.Interfaces;

namespace Server.Modules.Scheduling.Entities
{
	public class Schedule : BaseEntity<Schedule>, IEntity, IFilterByEmployee, IFilterByCustomer
	{
		public long CustomerId { get; set; }
		public long ReferralId { get; set; }
		public long EmployeeId { get; set; }
		public long ClinicianId { get; set; }
		public long ProductId { get; set; }
		public ScheduleStatusEnum Status { get; set; }
		public DateTime? Start { get; set; }
		public DateTime? End { get; set; }
		public required string Title { get; set; }
		public required string Description { get; set; }
	}
}