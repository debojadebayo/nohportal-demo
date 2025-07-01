namespace Shared.DTOs.Billing
{
    public class InvoiceFilterDto
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid? ProductId { get; set; }
    }
}
