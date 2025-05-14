using ComposedHealthBase.Server.Entities;

namespace Server.Modules.CRM.Entities
{
	public class Contract : BaseEntity<Contract>, IEntity
	{
		public required string Reference { get; set; }
		public string? Notes { get; set; }
		public long RepresentativeId { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public HashSet<Product> Products { get; set; } = new HashSet<Product>();
	}
}