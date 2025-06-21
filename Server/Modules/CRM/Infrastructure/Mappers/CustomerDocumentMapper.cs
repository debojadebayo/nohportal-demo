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
                Name = entity.Name,
                Description = entity.Description,
                BlobContainerName = entity.BlobContainerName,
                BlobName = entity.BlobName,
                CustomerDocumentType = entity.CustomerDocumentType
            };
        }

        public CustomerDocument Map(CustomerDocumentDto dto)
        {
            return new CustomerDocument
            {
                Id = dto.Id,
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                Name = dto.Name,
                Description = dto.Description,
                BlobContainerName = dto.BlobContainerName,
                BlobName = dto.BlobName,
                SearchTags = $"{dto.Name} {dto.Description}".ToLower(),
                CustomerDocumentType = dto.CustomerDocumentType
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
            entity.Id = dto.Id;
            entity.IsActive = dto.IsActive;
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.BlobContainerName = dto.BlobContainerName;
            entity.BlobName = dto.BlobName;
            entity.SearchTags = $"{dto.Name} {dto.Description}".ToLower();
            entity.CustomerDocumentType = dto.CustomerDocumentType;
        }

        public void Map(CustomerDocument entity, CustomerDocumentDto dto)
        {
            dto.Id = entity.Id;
            dto.IsActive = entity.IsActive;
            dto.CreatedBy = entity.CreatedBy;
            dto.LastModifiedBy = entity.LastModifiedBy;
            dto.CreatedDate = entity.CreatedDate;
            dto.ModifiedDate = entity.ModifiedDate;
            dto.Name = entity.Name;
            dto.Description = entity.Description;
            dto.BlobContainerName = entity.BlobContainerName;
            dto.BlobName = entity.BlobName;
            dto.CustomerDocumentType = entity.CustomerDocumentType;
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
