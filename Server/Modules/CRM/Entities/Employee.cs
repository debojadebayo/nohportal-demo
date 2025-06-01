using ComposedHealthBase.Server.Entities;


namespace Server.Modules.CRM.Entities
{
    public class Employee : BaseEntity<Employee>, IEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime? DOB { get; set; }
        public required string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public required string Postcode { get; set; }
        public required string Email { get; set; }
        public required string Telephone { get; set; }
        public required string JobRole { get; set; }
        public required string Department { get; set; }
        public required string LineManager { get; set; }
        public string Notes { get; set; } = string.Empty;
        public required Guid KeycloakId { get; set; }
        public HashSet<EmployeeDocument> Documents { get; set; } = new();
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
    }
}