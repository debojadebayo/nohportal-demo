using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.CRM.Entities
{
	public class Contract : BaseEntity<Contract>
	{
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public HashSet<Product> Products { get; set; } = new HashSet<Product>();
	}
}