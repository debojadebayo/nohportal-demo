using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.CRM
{
    public class DocumentDto : BaseDto<DocumentDto>, IDto, IDocumentDto, ILazyLookup
    {
        public required string FilePath { get; set; }
        public string BlobName { get; set; } = string.Empty;
        public string BlobContainerName { get; set; } = string.Empty;
        public required string Name { get; set; }
        public string? Description { get; set; }
        public long CustomerId { get; set; }
        public long EmployeeId { get; set; }
        public string DisplayName => Name;
    }
}
