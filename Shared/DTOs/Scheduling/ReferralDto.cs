using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using Shared.DTOs;

namespace Shared.DTOs.Scheduling
{
	public class ReferralDto : BaseDto<ReferralDto>, IDto, ILazyLookup
	{
		public string ReferralDetails { get; set; } = string.Empty;
		public string DocumentId { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
		public string DisplayName => $"{Title} - {ReferralDetails}";
		public long CustomerId { get; set; }
		public long EmployeeId { get; set; }
	}
}