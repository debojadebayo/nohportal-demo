using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using Shared.DTOs;

namespace Shared.DTOs.CRM
{
	public class ProductDto : BaseDto<ProductDto>, IDto, IAuditDto, ILazyLookup
	{
		public required ProductTypeDto ProductType { get; set; }
		public decimal Price { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public string DisplayName => $"{ProductType.Name} - {Price:C} - {StartTime?.ToString("d") ?? "N/A"} to {EndTime?.ToString("d") ?? "N/A"}";
		public long CustomerId { get; set; }
	}
}
