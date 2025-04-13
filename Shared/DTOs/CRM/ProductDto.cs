using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;

namespace Shared.DTOs.CRM
{
	public class ProductDto : BaseDto<ProductDto>
	{
		public ProductTypeDto ProductType { get; set; }
		public decimal Price { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}
