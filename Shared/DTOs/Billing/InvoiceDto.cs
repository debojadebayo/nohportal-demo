using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using Shared.DTOs;

namespace Shared.DTOs.Billing
{
    public class InvoiceDto : BaseDto<InvoiceDto>, IDto, IAuditDto, ILazyLookup
    {
        public required string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxRate { get; set; }
        public string Status { get; set; } = "Draft";
        public string? Notes { get; set; }

        // Xero integration properties
        public string? XeroInvoiceId { get; set; }
        public bool PostedToXero { get; set; } = false;
        public DateTime? PostedToXeroAt { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? ProductId { get; set; }
        public ICollection<LineItemDto> LineItems { get; set; } = new List<LineItemDto>();

        public string DisplayName => $"Invoice {InvoiceNumber} - {TotalAmount:C} - {InvoiceDate:d}";
    }
}
