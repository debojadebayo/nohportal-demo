using ComposedHealthBase.Server.Entities;

namespace Server.Modules.Scheduling.Entities
{
	public class Schedule : BaseEntity<Schedule>, IEntity
	{
		public long CustomerId { get; set; }
		public long ReferralId { get; set; }
		public long PatientId { get; set; }
		public long ClinicianId { get; set; }
		public DateTime Start { get; set; }
		public DateTime? End { get; set; }
	}
}