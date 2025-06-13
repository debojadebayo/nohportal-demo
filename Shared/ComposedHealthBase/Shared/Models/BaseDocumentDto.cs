using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace ComposedHealthBase.Shared.Models
{
    public class BaseDocumentDto : BaseDto<BaseDocumentDto>, IDto, IAuditDto, IDocumentDto, ILazyLookup
    {
        public required string FilePath { get; set; }
        public string BlobName { get; set; } = string.Empty;
        public string BlobContainerName { get; set; } = string.Empty;
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string DisplayName => $"{Name} - {Description ?? "No description"}";
    }
}
