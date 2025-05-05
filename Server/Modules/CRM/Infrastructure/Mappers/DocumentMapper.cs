using ComposedHealthBase.Server.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;
using System.Collections.Generic;
using System.Linq;

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
            FilePath = entity.FilePath,
            Description = entity.Description
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
            CreatedDate = dto.CreatedDate,
            ModifiedDate = dto.ModifiedDate,
            FilePath = dto.FilePath,
            Description = dto.Description
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
        entity.Id = dto.Id;
        entity.IsActive = dto.IsActive;
        entity.CreatedBy = dto.CreatedBy;
        entity.LastModifiedBy = dto.LastModifiedBy;
        entity.CreatedDate = dto.CreatedDate;
        entity.ModifiedDate = dto.ModifiedDate;
        entity.FilePath = dto.FilePath;
        entity.Description = dto.Description;
    }

    public void Map(Document entity, DocumentDto dto)
    {
        dto.Id = entity.Id;
        dto.IsActive = entity.IsActive;
        dto.CreatedBy = entity.CreatedBy;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedDate = entity.ModifiedDate;
        dto.FilePath = entity.FilePath;
        dto.Description = entity.Description;
    }

    public void Map(IEnumerable<DocumentDto> dtos, IEnumerable<Document> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < System.Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<Document> entities, IEnumerable<DocumentDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < System.Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}
