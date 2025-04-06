using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.Schedule.Entities
{
	public class Schedule : BaseEntity<Schedule>
	{
		public long NOHCustomerId { get; set; }
		public long ReferralId { get; set; }
		public long PatientId { get; set; }
		public long ClinicianId { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}