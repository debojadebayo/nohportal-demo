using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Shared.Interfaces;

namespace Server.Modules.CRM.Entities
{
    public class Manager : BaseEntity<Manager>, IEntity, IAuditEntity, ISearchTags, ISubject
    {
        public required string Username { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? UserName { get; set; }
        public required string Telephone { get; set; }
        public required string Email { get; set; }
        public string? AvatarImage { get; set; }
        public string? AvatarTitle { get; set; }
        public string? AvatarDescription { get; set; }
        public required string Department { get; set; }
        public HashSet<Employee> Employees { get; set; } = new();
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
        public string? RoleName { get; set; }
    }
}