using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.CRM.Entities
{
	public class NOHCustomer : BaseEntity<NOHCustomer>
	{
		public string Name { get; set; }
		public string Contact { get; set; }
		public string Email { get; set; }
		public string HouseNumberOrName { get; set; }
		public string Street { get; set; }
		public string TownOrCity { get; set; }
		public string County { get; set; }
		public string Postcode { get; set; }
		public string Country { get; set; }
		public HashSet<Contract> Contracts { get; set; }
		public HashSet<Product> Products { get; set; }
		public bool IsActive { get; set; }
	}
}