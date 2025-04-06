using ComposedHealthBase.Server.BaseModule.Entities;

namespace Server.Modules.Schedule.Entities
{
	public class Referral : BaseEntity<Referral>
	{
		public long NOHCustomerId { get; set; }
		public long PatientId { get; set; }
		public string ReferralDetails { get; set; }
	}
}