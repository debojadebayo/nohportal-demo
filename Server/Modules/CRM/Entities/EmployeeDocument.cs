using ComposedHealthBase.Server.Entities;
using Server.Modules.CommonModule.Interfaces;

namespace Server.Modules.CRM.Entities
{
	public class EmployeeDocument : BaseEntity<EmployeeDocument>, IEntity, INOHEntity, IDocument
	{
		public required string FilePath { get; set; }
		public required string BlobContainerName { get; set; }
		public required string BlobName { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }
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