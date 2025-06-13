using ComposedHealthBase.Server.Entities;

namespace Server.Modules.CRM.Entities
{
    public class CustomerDocument : BaseEntity<CustomerDocument>, IEntity, IAuditEntity, IDocument
    {
        public long CustomerId
        {
            get { return TenantId; }
            set { TenantId = value; }
        }
        public required string FilePath { get; set; }
        public required string BlobContainerName { get; set; }
        public required string BlobName { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}