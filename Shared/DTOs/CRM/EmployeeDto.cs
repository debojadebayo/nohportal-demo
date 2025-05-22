using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs;

namespace Shared.DTOs.CRM
{
    public class EmployeeDto : IDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long TenantId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DOB { get; set; }
        public string Address1 { get; set; } = string.Empty;
        public string? Address2 { get; set; }
        public string? Address3 { get; set; }
        public string Postcode { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telephone { get; set; } = string.Empty;
        public long CustomerId { get; set; }
        public string JobRole { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string LineManager { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
