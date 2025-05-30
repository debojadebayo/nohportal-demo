using ComposedHealthBase.Server.Entities;
using Server.Modules.CommonModule.Interfaces;

namespace Server.Modules.CRM.Entities
{
	public class Contract : BaseEntity<Contract>, IEntity, INOHEntity
	{
		public required string Reference { get; set; }
		public string? Notes { get; set; }
		public long RepresentativeId { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? EndTime { get; set; }
		public long CustomerId
		{
			get
			{
				return TenantId;
			}
			set
			{
				TenantId = value;
			}
		}
		public long EmployeeId
		{
			get
			{
				return SubjectId;
			}
			set
			{
				SubjectId = value;
			}
		}
	}
}