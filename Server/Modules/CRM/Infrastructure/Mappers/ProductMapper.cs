using NationOH.Server.Base.Infrastructure.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

public class ProductMapper : IMapper<Product, ProductDto>
{
    public ProductDto Map(Product entity)
    {
        return new ProductDto
        {
            Id = entity.Id,
            ProductType = new ProductTypeMapper().Map(entity.ProductType),
            Price = entity.Price,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            IsActive = entity.IsActive,
            CreatedBy = entity.CreatedBy,
            LastModifiedBy = entity.LastModifiedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedDate = entity.ModifiedDate
        };
    }

    public Product Map(ProductDto dto)
    {
        return new Product
        {
            Id = dto.Id,
            ProductType = new ProductTypeMapper().Map(dto.ProductType),
            Price = dto.Price,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            IsActive = dto.IsActive,
            CreatedBy = dto.CreatedBy,
            LastModifiedBy = dto.LastModifiedBy,
            CreatedDate = dto.CreatedDate,
            ModifiedDate = dto.ModifiedDate
        };
    }

    public IEnumerable<ProductDto> Map(IEnumerable<Product> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<Product> Map(IEnumerable<ProductDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(ProductDto dto, Product entity)
    {
        entity.ProductType = new ProductTypeMapper().Map(dto.ProductType);
        entity.Price = dto.Price;
        entity.StartTime = dto.StartTime;
        entity.EndTime = dto.EndTime;
        entity.IsActive = dto.IsActive;
        entity.CreatedBy = dto.CreatedBy;
        entity.LastModifiedBy = dto.LastModifiedBy;
        entity.CreatedDate = dto.CreatedDate;
        entity.ModifiedDate = dto.ModifiedDate;
    }

    public void Map(Product entity, ProductDto dto)
    {
        dto.ProductType = new ProductTypeMapper().Map(entity.ProductType);
        dto.Price = entity.Price;
        dto.StartTime = entity.StartTime;
        dto.EndTime = entity.EndTime;
        dto.IsActive = entity.IsActive;
        dto.CreatedBy = entity.CreatedBy;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedDate = entity.ModifiedDate;
    }

    public void Map(IEnumerable<ProductDto> dtos, IEnumerable<Product> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<Product> entities, IEnumerable<ProductDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}