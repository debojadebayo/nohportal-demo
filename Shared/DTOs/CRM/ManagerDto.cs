using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using System;

namespace Shared.DTOs.CRM
{
    public class ManagerDto : IDto, ILazyLookup
    {
        public long Id { get; set; }
        public long CustomerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;

        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long TenantId { get; set; }
        public string DisplayName => Name;
    }
}