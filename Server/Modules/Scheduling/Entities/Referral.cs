using ComposedHealthBase.Server.Entities;

namespace Server.Modules.Scheduling.Entities
{
	public class Referral : BaseEntity<Referral>, IEntity
	{
		public long CustomerId { get; set; }
		public long PatientId { get; set; }
		public string ReferralDetails { get; set; } = string.Empty;
		public string DocumentId { get; set; } = string.Empty;
	}
}