using ComposedHealthBase.Server.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

public class ProductMapper : IMapper<Product, ProductDto>
{
    private readonly IMapper<ProductType, ProductTypeDto> _productTypeMapper;
    public ProductMapper(IMapper<ProductType, ProductTypeDto> productTypeMapper)
    {
        _productTypeMapper = productTypeMapper;
    }
    public ProductDto Map(Product entity)
    {
        return new ProductDto
        {
            Id = entity.Id,
            ProductType = _productTypeMapper.Map(entity.ProductType),
            Price = entity.Price,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            IsActive = entity.IsActive,
            CreatedBy = entity.CreatedBy,
            LastModifiedBy = entity.LastModifiedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedDate = entity.ModifiedDate,
            CustomerId = entity.CustomerId,
        };
    }

    public Product Map(ProductDto dto)
    {
        return new Product
        {
            ProductType = _productTypeMapper.Map(dto.ProductType),
            Price = dto.Price,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            IsActive = dto.IsActive,
            CustomerId = dto.CustomerId
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
        entity.ProductType = _productTypeMapper.Map(dto.ProductType);
        entity.Price = dto.Price;
        entity.StartTime = dto.StartTime;
        entity.EndTime = dto.EndTime;
        entity.IsActive = dto.IsActive;
        entity.CustomerId = dto.CustomerId;
    }

    public void Map(Product entity, ProductDto dto)
    {
        dto.ProductType = _productTypeMapper.Map(entity.ProductType);
        dto.Price = entity.Price;
        dto.StartTime = entity.StartTime;
        dto.EndTime = entity.EndTime;
        dto.IsActive = entity.IsActive;
        dto.CreatedBy = entity.CreatedBy;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedDate = entity.ModifiedDate;
        dto.CustomerId = entity.CustomerId;
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