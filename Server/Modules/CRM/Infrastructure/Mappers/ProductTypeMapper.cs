using NationOH.Server.Base.Infrastructure.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

public class ProductTypeMapper : IMapper<ProductType, ProductTypeDto>
{
    public ProductTypeDto Map(ProductType entity)
    {
        return new ProductTypeDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            DefaultPrice = entity.DefaultPrice,
            ChargeCode = entity.ChargeCode,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            IsActive = entity.IsActive,
            CreatedBy = entity.CreatedBy,
            LastModifiedBy = entity.LastModifiedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedDate = entity.ModifiedDate
        };
    }

    public ProductType Map(ProductTypeDto dto)
    {
        return new ProductType
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            DefaultPrice = dto.DefaultPrice,
            ChargeCode = dto.ChargeCode,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            IsActive = dto.IsActive,
            CreatedBy = dto.CreatedBy,
            LastModifiedBy = dto.LastModifiedBy,
            CreatedDate = dto.CreatedDate,
            ModifiedDate = dto.ModifiedDate
        };
    }

    public IEnumerable<ProductTypeDto> Map(IEnumerable<ProductType> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<ProductType> Map(IEnumerable<ProductTypeDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(ProductTypeDto dto, ProductType entity)
    {
        entity.Name = dto.Name;
        entity.Description = dto.Description;
        entity.DefaultPrice = dto.DefaultPrice;
        entity.ChargeCode = dto.ChargeCode;
        entity.StartTime = dto.StartTime;
        entity.EndTime = dto.EndTime;
        entity.IsActive = dto.IsActive;
        entity.CreatedBy = dto.CreatedBy;
        entity.LastModifiedBy = dto.LastModifiedBy;
        entity.CreatedDate = dto.CreatedDate;
        entity.ModifiedDate = dto.ModifiedDate;
    }

    public void Map(ProductType entity, ProductTypeDto dto)
    {
        dto.Name = entity.Name;
        dto.Description = entity.Description;
        dto.DefaultPrice = entity.DefaultPrice;
        dto.ChargeCode = entity.ChargeCode;
        dto.StartTime = entity.StartTime;
        dto.EndTime = entity.EndTime;
        dto.IsActive = entity.IsActive;
        dto.CreatedBy = entity.CreatedBy;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedDate = entity.ModifiedDate;
    }

    public void Map(IEnumerable<ProductTypeDto> dtos, IEnumerable<ProductType> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<ProductType> entities, IEnumerable<ProductTypeDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}