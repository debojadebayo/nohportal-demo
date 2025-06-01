using ComposedHealthBase.Server.Entities;


namespace Server.Modules.CRM.Entities
{
	public class ProductType : BaseEntity<ProductType>, IEntity
	{
		public required string Name { get; set; }
		public required string Description { get; set; }
		public decimal DefaultPrice { get; set; }
		public required string ChargeCode { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
	}
}