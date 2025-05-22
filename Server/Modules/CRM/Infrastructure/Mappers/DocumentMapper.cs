using ComposedHealthBase.Server.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

namespace Server.Modules.CRM.Infrastructure.Mappers
{
    public class DocumentMapper : IMapper<Document, DocumentDto>
    {
        public DocumentDto Map(Document entity)
        {
            return new DocumentDto
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                CreatedBy = entity.CreatedBy,
                LastModifiedBy = entity.LastModifiedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedDate = entity.ModifiedDate,
                CustomerId = entity.CustomerId,
                EmployeeId = entity.EmployeeId,
                FilePath = entity.FilePath,
                Name = entity.Name,
                Description = entity.Description,
                BlobContainerName = entity.BlobContainerName,
                BlobName = entity.BlobName
            };
        }

        public Document Map(DocumentDto dto)
        {
            return new Document
            {
                Id = dto.Id,
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                LastModifiedBy = dto.LastModifiedBy,
                // CreatedDate and ModifiedDate are typically handled by the BaseEntity or database
                // and should not be set from the DTO when creating/updating an entity.
                // If you need to set them, uncomment the lines below and ensure business logic allows it.
                // CreatedDate = dto.CreatedDate,
                // ModifiedDate = dto.ModifiedDate,
                CustomerId = dto.CustomerId,
                EmployeeId = dto.EmployeeId,
                FilePath = dto.FilePath,
                Name = dto.Name,
                Description = dto.Description,
                BlobContainerName = dto.BlobContainerName,
                BlobName = dto.BlobName
            };
        }

        public IEnumerable<DocumentDto> Map(IEnumerable<Document> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<Document> Map(IEnumerable<DocumentDto> dtos)
        {
            return dtos.Select(Map);
        }

        public void Map(DocumentDto dto, Document entity)
        {
            entity.Id = dto.Id; // Be cautious mapping Id back if it's auto-generated
            entity.IsActive = dto.IsActive;
            // CreatedBy, LastModifiedBy, CreatedDate, ModifiedDate are usually managed by the system/BaseEntity logic
            // and not directly mapped from DTO in an update operation.
            // If you have specific needs to update them from a DTO, uncomment and adjust as necessary.
            // entity.CreatedBy = dto.CreatedBy;
            // entity.LastModifiedBy = dto.LastModifiedBy;
            // entity.CreatedDate = dto.CreatedDate;
            // entity.ModifiedDate = dto.ModifiedDate;
            entity.CustomerId = dto.CustomerId;
            entity.EmployeeId = dto.EmployeeId;
            entity.FilePath = dto.FilePath;
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.BlobContainerName = dto.BlobContainerName;
            entity.BlobName = dto.BlobName;
        }

        public void Map(Document entity, DocumentDto dto)
        {
            dto.Id = entity.Id;
            dto.IsActive = entity.IsActive;
            dto.CreatedBy = entity.CreatedBy;
            dto.LastModifiedBy = entity.LastModifiedBy;
            dto.CreatedDate = entity.CreatedDate;
            dto.ModifiedDate = entity.ModifiedDate;
            dto.CustomerId = entity.CustomerId;
            dto.EmployeeId = entity.EmployeeId;
            dto.FilePath = entity.FilePath;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.BlobContainerName = entity.BlobContainerName;
            dto.BlobName = entity.BlobName;
        }

        public void Map(IEnumerable<DocumentDto> dtos, IEnumerable<Document> entities)
        {
            var dtosArray = dtos.ToArray();
            var entitiesArray = entities.ToArray();
            for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
            {
                Map(dtosArray[i], entitiesArray[i]);
            }
        }

        public void Map(IEnumerable<Document> entities, IEnumerable<DocumentDto> dtos)
        {
            var dtosArray = dtos.ToArray();
            var entitiesArray = entities.ToArray();
            for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
            {
                Map(entitiesArray[i], dtosArray[i]);
            }
        }
    }
}
