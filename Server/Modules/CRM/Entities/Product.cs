using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.CRM.Entities
{
	public class Product : BaseEntity<Product>
	{
		public string Name { get; set; }
		public ProductType ProductType { get; set; }
		public decimal Price { get; set; }
		public HashSet<NOHCustomer> NOHCustomers { get; set; }
	}
}