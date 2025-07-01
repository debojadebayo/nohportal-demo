namespace ComposedHealthBase.Shared.DTOs
{
    public interface IDocumentDto : IDto
    {
        string BlobName { get; set; }
        string BlobContainerName { get; set; }
        string Name { get; set; }
        string? Description { get; set; }
    }
}