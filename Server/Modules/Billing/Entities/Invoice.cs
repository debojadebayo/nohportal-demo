using ComposedHealthBase.Server.Entities;

namespace Server.Modules.Billing.Entities
{
    public class Invoice : BaseEntity<Invoice>, IEntity, IAuditEntity
    {
        public required string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TaxRate { get; set; } = 0.20m; // Default 20% VAT
        public string Status { get; set; } = "Draft"; // Draft, Sent, Paid, Overdue
        public string? Notes { get; set; }
        
        // Xero integration properties
        public string? XeroInvoiceId { get; set; }
        public bool PostedToXero { get; set; } = false;
        public DateTime? PostedToXeroAt { get; set; }
        
        // Filter criteria used to generate this invoice
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? ProductId { get; set; }
        
        // Line items for this invoice
        public HashSet<LineItem> LineItems { get; set; } = new();
        
        // Override the Customer property for Invoice context
        public Guid InvoiceCustomerId
        {
            get => TenantId;
            set => TenantId = value;
        }
    }
}
