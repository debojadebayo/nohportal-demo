using ComposedHealthBase.Server.Entities;
using Server.Modules.CommonModule.Interfaces;

namespace Server.Modules.CRM.Entities
{
	public class Customer : BaseEntity<Customer>, IEntity, INOHEntity
	{
		public required string Name { get; set; }
		public required string Telephone { get; set; }
		public int NumberOfEmployees { get; set; }
		public required string Site { get; set; }
		public required string OHServicesRequired { get; set; }
		public required string Address { get; set; }
		public required string Industry { get; set; }
		public required string Postcode { get; set; }
		public required string Website { get; set; }
		public required string Email { get; set; }
		public required string InvoiceEmail { get; set; }
		public string Notes { get; set; } = string.Empty;
		public HashSet<Contract> Contracts { get; set; } = new();
		public HashSet<Document> Documents { get; set; } = new();
		public required Guid KeycloakId { get; set; }
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