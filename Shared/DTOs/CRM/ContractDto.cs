using Shared.DTOs;

namespace Shared.DTOs.CRM
{
	public class ContractDto : BaseDto<ContractDto>
	{
		public string Reference { get; set; }
		public string Notes { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public HashSet<ProductDto> Products { get; set; } = new HashSet<ProductDto>();
	}
}
