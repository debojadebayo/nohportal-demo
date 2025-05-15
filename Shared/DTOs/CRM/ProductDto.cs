using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;

namespace Shared.DTOs.CRM
{
	public class ProductDto : IDto
	{
		public long Id { get; set; }
		public bool IsActive { get; set; }
		public int CreatedBy { get; set; }
		public int LastModifiedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime ModifiedDate { get; set; }
		public required ProductTypeDto ProductType { get; set; }
		public decimal Price { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
	}
}
