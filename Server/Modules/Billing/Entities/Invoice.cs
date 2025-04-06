using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.Billing.Entities
{
	public class Invoice : BaseEntity<Invoice>
	{
		public string CustomerId { get; set; }
		public DateTime InvoiceDate { get; set; }
		public DateTime ExpiryDate { get; set; }
		public decimal TotalAmount { get; set; }
		public string Notes { get; set; }
		public HashSet<InvoiceItem> Items { get; set; } = new();
	}
}