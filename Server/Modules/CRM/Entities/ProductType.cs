using ComposedHealthBase.Server.Entities;

namespace Server.Modules.CRM.Entities
{
	public class ProductType : BaseEntity<ProductType>, IEntity
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal DefaultPrice { get; set; }
		public string ChargeCode { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
	}
}