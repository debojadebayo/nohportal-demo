using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Shared.Interfaces;

namespace Server.Modules.CRM.Entities
{
    public class Employee : BaseEntity<Employee>, IEntity, IAuditEntity, ISubjectEntity, ISearchTags, ISubject
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Username { get; set; }
        public required string Telephone { get; set; }
        public required string Email { get; set; }
        public string? AvatarImage { get; set; }
        public string? AvatarTitle { get; set; }
        public string? AvatarDescription { get; set; }
        public Guid KeycloakId { get; set; } = Guid.Empty; // This is used to link the employee to a Keycloak user
        public DateTime? DOB { get; set; }
        public required string Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public required string Postcode { get; set; }
        public required string JobRole { get; set; }
        public required string Department { get; set; }
        public required string LineManager { get; set; }
        public string Notes { get; set; } = string.Empty;
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