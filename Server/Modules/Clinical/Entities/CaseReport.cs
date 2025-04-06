using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.Clinical.Entities
{
	public class CaseReport : BaseEntity<CaseReport>
	{
		public long ClinicianId { get; set; }
		public long NOHCustomerId { get; set; }
		public long PatientId { get; set; }
		public DateTime ReportTime { get; set; }
		public string Report { get; set; }
	}
}