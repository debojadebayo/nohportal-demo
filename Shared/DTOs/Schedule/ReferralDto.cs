namespace Shared.DTOs.Schedule
{
	public class ReferralDto
	{
		public long ReferralId { get; set; }
		public long NOHCustomerId { get; set; }
		public long PatientId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string DOB { get; set; } = string.Empty;
		public string ReferralDetails { get; set; } = string.Empty;
	}
}