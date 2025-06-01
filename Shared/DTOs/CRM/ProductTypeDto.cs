using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using Shared.DTOs;

namespace Shared.DTOs.CRM
{
    public class ProductTypeDto : BaseDto<ProductTypeDto>, IDto, ILazyLookup
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal DefaultPrice { get; set; }
        public required string ChargeCode { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string DisplayName => $"{Name} - {Description} - {DefaultPrice:C} - {StartTime?.ToString("d") ?? "N/A"} to {EndTime?.ToString("d") ?? "N/A"}";
    }
}
