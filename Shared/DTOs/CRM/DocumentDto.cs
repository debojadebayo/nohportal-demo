using ComposedHealthBase.Shared.DTOs;

namespace Shared.DTOs.CRM
{
    public class DocumentDto : IDto
    {
        public long Id { get; set; }
        public bool IsActive { get; set; }
        public long CreatedBy { get; set; }
        public long LastModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public long TenantId { get; set; }
        public long CustomerId { get; set; }
        public long EmployeeId { get; set; }
        public required string FilePath { get; set; }
        public string BlobName { get; set; } = string.Empty;
        public string BlobContainerName { get; set; } = string.Empty;
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}
