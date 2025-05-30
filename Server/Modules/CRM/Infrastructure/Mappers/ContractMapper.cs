using ComposedHealthBase.Server.Mappers;
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
            IsActive = entity.IsActive,
            CreatedBy = entity.CreatedBy,
            LastModifiedBy = entity.LastModifiedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedDate = entity.ModifiedDate,
            RepresentativeId = entity.RepresentativeId
        };
    }

    public Contract Map(ContractDto dto)
    {
        return new Contract
        {
            Reference = dto.Reference,
            Notes = dto.Notes,
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            IsActive = dto.IsActive,
            RepresentativeId = dto.RepresentativeId
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
        entity.IsActive = dto.IsActive;
        entity.RepresentativeId = dto.RepresentativeId;
    }

    public void Map(Contract entity, ContractDto dto)
    {
        dto.Id = entity.Id;
        dto.Reference = entity.Reference;
        dto.Notes = entity.Notes;
        dto.StartTime = entity.StartTime;
        dto.EndTime = entity.EndTime;
        dto.IsActive = entity.IsActive;
        dto.CreatedBy = entity.CreatedBy;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedDate = entity.ModifiedDate;
        dto.RepresentativeId = entity.RepresentativeId;
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

