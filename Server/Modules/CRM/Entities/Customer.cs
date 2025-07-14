using ComposedHealthBase.Server.Auth;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Shared.Interfaces;


namespace Server.Modules.CRM.Entities
{
    public class Customer : BaseEntity<Customer>, IEntity, IAuditEntity, ISearchTags, ITenant
    {
        public required string Name { get; set; }
        public required string Domain { get; set; }
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
        public HashSet<Product> Products { get; set; } = new();
        public HashSet<Employee> Employees { get; set; } = new();
        public HashSet<Manager> Managers { get; set; } = new();
        public Guid[] RelatedDocumentIds { get; set; } = Array.Empty<Guid>();
        public Guid CustomerId
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
        public string SearchTags { get; set; } = string.Empty;
    }
}