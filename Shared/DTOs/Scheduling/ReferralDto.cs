using Shared.DTOs;

namespace Shared.DTOs.Scheduling
{
	public class ReferralDto : BaseDto<ReferralDto>
	{
		public long CustomerId { get; set; }
		public long PatientId { get; set; }
		public string ReferralDetails { get; set; } = string.Empty;
		public string DocumentId { get; set; } = string.Empty;
	}
}