using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.CRM.Entities
{
	public class Product : BaseEntity<Product>
	{
		public ProductType ProductType { get; set; }
		public decimal Price { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
	}
}