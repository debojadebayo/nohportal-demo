using ComposedHealthBase.Server.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

namespace Server.Modules.CRM.Infrastructure.Mappers
{
    public class CustomerDocumentMapper : IMapper<CustomerDocument, CustomerDocumentDto>
    {
        public CustomerDocumentDto Map(CustomerDocument entity)
        {
            return new CustomerDocumentDto
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
                BlobName = entity.BlobName,
                CustomerId = entity.CustomerId
            };
        }

        public CustomerDocument Map(CustomerDocumentDto dto)
        {
            return new CustomerDocument
            {
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                FilePath = dto.FilePath,
                Name = dto.Name,
                Description = dto.Description,
                BlobContainerName = dto.BlobContainerName,
                BlobName = dto.BlobName,
                CustomerId = dto.CustomerId
            };
        }

        public IEnumerable<CustomerDocumentDto> Map(IEnumerable<CustomerDocument> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<CustomerDocument> Map(IEnumerable<CustomerDocumentDto> dtos)
        {
            return dtos.Select(Map);
        }

        public void Map(CustomerDocumentDto dto, CustomerDocument entity)
        {
            entity.IsActive = dto.IsActive;
            entity.FilePath = dto.FilePath;
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.BlobContainerName = dto.BlobContainerName;
            entity.BlobName = dto.BlobName;
            entity.CustomerId = dto.CustomerId;
        }

        public void Map(CustomerDocument entity, CustomerDocumentDto dto)
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
            dto.CustomerId = entity.CustomerId;
        }

        public void Map(IEnumerable<CustomerDocumentDto> dtos, IEnumerable<CustomerDocument> entities)
        {
            var dtosArray = dtos.ToArray();
            var entitiesArray = entities.ToArray();
            for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
            {
                Map(dtosArray[i], entitiesArray[i]);
            }
        }

        public void Map(IEnumerable<CustomerDocument> entities, IEnumerable<CustomerDocumentDto> dtos)
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
