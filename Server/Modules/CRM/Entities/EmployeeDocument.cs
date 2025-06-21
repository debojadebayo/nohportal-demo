using ComposedHealthBase.Server.Entities;
using Shared.Enums;

namespace Server.Modules.CRM.Entities
{
    public class EmployeeDocument : BaseEntity<EmployeeDocument>, IEntity, IAuditEntity, IDocument, ISearchTags
    {
        public Guid EmployeeId
        {
            get { return SubjectId; }
            set { SubjectId = value; }
        }
        public required string FilePath { get; set; }
        public required string BlobContainerName { get; set; }
        public required string BlobName { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
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
        public Guid DocumentGuid { get; set; }
        public EmployeeDocumentTypeEnum EmployeeDocumentType { get; set; } = EmployeeDocumentTypeEnum.Other;
    }
}