using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.Scheduling.Entities
{
	public class Clinician : BaseEntity<Clinician>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Telephone { get; set; }
		public string Email { get; set; }
		public ClinicianTypeEnum ClinicianType { get; set; }
		public RegulatorTypeEnum RegulatorType { get; set; }
		public string LicenceNumber { get; set; }
		public string ProfilePictureUrl { get; set; }
	}
}