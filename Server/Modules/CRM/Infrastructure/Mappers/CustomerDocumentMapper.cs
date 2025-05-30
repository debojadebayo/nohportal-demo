using ComposedHealthBase.Server.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

namespace Server.Modules.CRM.Infrastructure.Mappers
{
    public class CustomerDocumentMapper : IMapper<CustomerDocument, DocumentDto>
    {
        public DocumentDto Map(CustomerDocument entity)
        {
            return new DocumentDto
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                CreatedBy = entity.CreatedBy,
                LastModifiedBy = entity.LastModifiedBy,
                CreatedDate = entity.CreatedDate,
                ModifiedDate = entity.ModifiedDate,
                FilePath = entity.FilePath,
                Name = entity.Name,
                Description = entity.Description,
                BlobContainerName = entity.BlobContainerName,
                BlobName = entity.BlobName
            };
        }

        public CustomerDocument Map(DocumentDto dto)
        {
            return new CustomerDocument
            {
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                FilePath = dto.FilePath,
                Name = dto.Name,
                Description = dto.Description,
                BlobContainerName = dto.BlobContainerName,
                BlobName = dto.BlobName
            };
        }

        public IEnumerable<DocumentDto> Map(IEnumerable<CustomerDocument> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<CustomerDocument> Map(IEnumerable<DocumentDto> dtos)
        {
            return dtos.Select(Map);
        }

        public void Map(DocumentDto dto, CustomerDocument entity)
        {
            entity.IsActive = dto.IsActive;
            entity.FilePath = dto.FilePath;
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.BlobContainerName = dto.BlobContainerName;
            entity.BlobName = dto.BlobName;
        }

        public void Map(CustomerDocument entity, DocumentDto dto)
        {
            dto.Id = entity.Id;
            dto.IsActive = entity.IsActive;
            dto.CreatedBy = entity.CreatedBy;
            dto.LastModifiedBy = entity.LastModifiedBy;
            dto.CreatedDate = entity.CreatedDate;
            dto.ModifiedDate = entity.ModifiedDate;
            dto.FilePath = entity.FilePath;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.BlobContainerName = entity.BlobContainerName;
            dto.BlobName = entity.BlobName;
        }

        public void Map(IEnumerable<DocumentDto> dtos, IEnumerable<CustomerDocument> entities)
        {
            var dtosArray = dtos.ToArray();
            var entitiesArray = entities.ToArray();
            for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
            {
                Map(dtosArray[i], entitiesArray[i]);
            }
        }

        public void Map(IEnumerable<CustomerDocument> entities, IEnumerable<DocumentDto> dtos)
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
