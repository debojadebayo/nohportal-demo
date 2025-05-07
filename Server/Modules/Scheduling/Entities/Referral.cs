using ComposedHealthBase.Server.Entities;
using Server.Modules.CommonModule.Interfaces;

namespace Server.Modules.Scheduling.Entities
{
	public class Referral : BaseEntity<Referral>, IEntity, IFilterByEmployee, IFilterByCustomer
	{
		public long CustomerId { get; set; }
		public long EmployeeId { get; set; }
		public string ReferralDetails { get; set; } = string.Empty;
		public string DocumentId { get; set; } = string.Empty;
	}
}