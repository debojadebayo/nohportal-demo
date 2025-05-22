using ComposedHealthBase.Server.Entities;
using Server.Modules.CommonModule.Interfaces;

namespace Server.Modules.CRM.Entities
{
	public class Document : BaseEntity<Document>, IEntity, IFilterByEmployee, IFilterByCustomer
	{
		public long CustomerId { get; set; }
		public long EmployeeId { get; set; }
		public required string FilePath { get; set; }
		public required string BlobContainerName { get; set; }
		public required string BlobName { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }
	}
}