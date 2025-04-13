using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.CRM.Entities
{
	public class ProductType : BaseEntity<ProductType>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal DefaultPrice { get; set; }
		public string ChargeCode { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime ExpiryDate { get; set; }
	}
}