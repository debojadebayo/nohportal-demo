using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using Shared.DTOs;

namespace Shared.DTOs.Billing
{
    public class LineItemDto : BaseDto<LineItemDto>, IDto, IAuditDto, ILazyLookup
    {
        public Guid InvoiceId { get; set; }
        public Guid ScheduleId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductChargeCode { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal LineTotal { get; set; }
        public DateTime ServiceDate { get; set; }
        public string? Description { get; set; }
        public string DisplayName => $"{ProductName} - {ServiceDate:d} - {LineTotal:C}";
    }
}
