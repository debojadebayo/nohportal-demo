using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;

namespace Shared.DTOs.Scheduling
{
	public class ReferralDto : IDto
	{
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public int CreatedBy { get; set; }
		public int LastModifiedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public long CustomerId { get; set; }
		public long EmployeeId { get; set; }
		public string ReferralDetails { get; set; } = string.Empty;
		public string DocumentId { get; set; } = string.Empty;
		public string Title { get; set; } = string.Empty;
	}
}