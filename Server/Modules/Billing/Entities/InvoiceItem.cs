using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.Billing.Entities
{
	public class InvoiceItem : BaseEntity<InvoiceItem>
	{
		public string CustomerId { get; set; }
		public DateTime InvoiceItemDate { get; set; }
		public decimal TotalAmount { get; set; }
		public string Notes { get; set; }
	}
}