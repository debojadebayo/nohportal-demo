using ComposedHealthBase.Server.BaseModule.Entities;
using Server.Modules.Clinical.Entities;

namespace Server.Modules.Clinical.Entities
{
	public class Patient : ApplicationUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string EmailAddress { get; set; }
		public string PhoneNumber { get; set; }
		public long NOHCustomerId { get; set; }
		public HashSet<Clinician> Clinicians { get; set; } = new();
		public HashSet<CaseReport> CaseReports { get; set; } = new();
	}
}