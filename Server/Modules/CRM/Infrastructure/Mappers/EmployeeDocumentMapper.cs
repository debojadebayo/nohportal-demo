using ComposedHealthBase.Server.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

namespace Server.Modules.CRM.Infrastructure.Mappers
{
    public class EmployeeDocumentMapper : IMapper<EmployeeDocument, EmployeeDocumentDto>
    {
        public EmployeeDocumentDto Map(EmployeeDocument entity)
        {
            return new EmployeeDocumentDto
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
                EmployeeId = entity.EmployeeId
            };
        }

        public EmployeeDocument Map(EmployeeDocumentDto dto)
        {
            return new EmployeeDocument
            {
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                FilePath = dto.FilePath,
                Name = dto.Name,
                Description = dto.Description,
                BlobContainerName = dto.BlobContainerName,
                BlobName = dto.BlobName,
                EmployeeId = dto.EmployeeId
            };
        }

        public IEnumerable<EmployeeDocumentDto> Map(IEnumerable<EmployeeDocument> entities)
        {
            return entities.Select(Map);
        }

        public IEnumerable<EmployeeDocument> Map(IEnumerable<EmployeeDocumentDto> dtos)
        {
            return dtos.Select(Map);
        }

        public void Map(EmployeeDocumentDto dto, EmployeeDocument entity)
        {
            entity.IsActive = dto.IsActive;
            entity.FilePath = dto.FilePath;
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.BlobContainerName = dto.BlobContainerName;
            entity.BlobName = dto.BlobName;
            entity.EmployeeId = dto.EmployeeId;
        }

        public void Map(EmployeeDocument entity, EmployeeDocumentDto dto)
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
            dto.EmployeeId = entity.EmployeeId;
        }

        public void Map(IEnumerable<EmployeeDocumentDto> dtos, IEnumerable<EmployeeDocument> entities)
        {
            var dtosArray = dtos.ToArray();
            var entitiesArray = entities.ToArray();
            for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
            {
                Map(dtosArray[i], entitiesArray[i]);
            }
        }

        public void Map(IEnumerable<EmployeeDocument> entities, IEnumerable<EmployeeDocumentDto> dtos)
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
