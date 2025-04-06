using ComposedHealthBase.Server.BaseModule.Entities;
using Server.Modules.Clinical.Enums;
using Server.Modules.Clinical.Entities;

namespace Server.Modules.Clinical.Entities
{
	public class Clinician : ApplicationUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public ClinicianTypeEnum RegulatoryType { get; set; }
		public int RegulatoryNumber { get; set; }
		public HashSet<CaseReport> CaseReports { get; set; } = new();
		public HashSet<Patient> Patients { get; set; } = new();
	}
}