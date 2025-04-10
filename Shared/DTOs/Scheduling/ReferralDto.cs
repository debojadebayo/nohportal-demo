using ComposedHealthBase.Shared.DTOs;

namespace Shared.DTOs.Scheduling
{
	public class ReferralDto : BaseDto<ReferralDto>, IDto
	{
		public long NOHCustomerId { get; set; }
		public long PatientId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string DOB { get; set; } = string.Empty;
		public string ReferralDetails { get; set; } = string.Empty;
	}
}