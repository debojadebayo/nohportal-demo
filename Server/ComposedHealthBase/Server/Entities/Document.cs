namespace ComposedHealthBase.Server.Entities
{
	public class Document : BaseEntity<Document>, IEntity, IDocument
	{
		public required string FilePath { get; set; }
		public required string BlobContainerName { get; set; }
		public required string BlobName { get; set; }
		public required string Name { get; set; }
		public string? Description { get; set; }
	}
}