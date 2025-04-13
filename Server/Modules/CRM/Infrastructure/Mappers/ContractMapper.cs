using NationOH.Server.Base.Infrastructure.Mappers;
using Server.Modules.CRM.Entities;
using Shared.DTOs.CRM;

public class ContractMapper : IMapper<Contract, ContractDto>
{
    public ContractDto Map(Contract entity)
    {
        return new ContractDto
        {
            Id = entity.Id,
            Reference = entity.Reference,
            Notes = entity.Notes,
            StartTime = entity.StartTime,
            EndTime = entity.EndTime,
            Products = new HashSet<ProductDto>(entity.Products.Select(p => new ProductMapper().Map(p))),
            IsActive = entity.IsActive,
            CreatedBy = entity.CreatedBy,
            LastModifiedBy = entity.LastModifiedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedDate = entity.ModifiedDate
        };
    }

    public Contract Map(ContractDto dto)
    {
        return new Contract
        {
            Id = dto.Id,
            Reference = dto.Reference,
            Notes = dto.Notes,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            Products = new HashSet<Product>(dto.Products.Select(p => new ProductMapper().Map(p))),
            IsActive = dto.IsActive,
            CreatedBy = dto.CreatedBy,
            LastModifiedBy = dto.LastModifiedBy,
            CreatedDate = dto.CreatedDate,
            ModifiedDate = dto.ModifiedDate
        };
    }

    public IEnumerable<ContractDto> Map(IEnumerable<Contract> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<Contract> Map(IEnumerable<ContractDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(ContractDto dto, Contract entity)
    {
        entity.Reference = dto.Reference;
        entity.Notes = dto.Notes;
        entity.StartTime = dto.StartTime;
        entity.EndTime = dto.EndTime;
        entity.Products = new HashSet<Product>(dto.Products.Select(p => new ProductMapper().Map(p)));
        entity.IsActive = dto.IsActive;
        entity.CreatedBy = dto.CreatedBy;
        entity.LastModifiedBy = dto.LastModifiedBy;
        entity.CreatedDate = dto.CreatedDate;
        entity.ModifiedDate = dto.ModifiedDate;
    }

    public void Map(Contract entity, ContractDto dto)
    {
        dto.Reference = entity.Reference;
        dto.Notes = entity.Notes;
        dto.StartTime = entity.StartTime;
        dto.EndTime = entity.EndTime;
        dto.Products = new HashSet<ProductDto>(entity.Products.Select(p => new ProductMapper().Map(p)));
        dto.IsActive = entity.IsActive;
        dto.CreatedBy = entity.CreatedBy;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedDate = entity.ModifiedDate;
    }

    public void Map(IEnumerable<ContractDto> dtos, IEnumerable<Contract> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<Contract> entities, IEnumerable<ContractDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}

