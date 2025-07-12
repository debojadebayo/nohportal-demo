using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;
using Shared.Interfaces;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.CRM
{
    public class EmployeeDto : BaseDto<EmployeeDto>, IDto, IAuditDto, INotesTab, ILazyLookup, ISubject
    {
        public string? Username { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime? DOB { get; set; }
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string Postcode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public string JobRole { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string LineManager { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public List<Guid> RelatedDocumentIds { get; set; } = new List<Guid>();
        public string DisplayName => $"{FirstName} {LastName}";
        public Guid CustomerId
        {
            get => base.TenantId;
            set => base.TenantId = value;
        }
        public string? RoleName { get; set; }
    }
}
