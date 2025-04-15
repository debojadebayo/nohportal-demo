using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;
namespace Shared.DTOs.CRM
{
	public class ProductTypeDto : BaseDto<ProductTypeDto>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal DefaultPrice { get; set; }
		public string ChargeCode { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}
