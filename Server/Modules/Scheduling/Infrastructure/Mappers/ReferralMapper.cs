using ComposedHealthBase.Server.Mappers;
using Server.Modules.Scheduling.Entities;
using Shared.DTOs.Scheduling;

public class ReferralMapper : IMapper<Referral, ReferralDto>
{
    public ReferralDto Map(Referral entity)
    {
        return new ReferralDto
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId,
            EmployeeId = entity.EmployeeId,
            ReferralDetails = entity.ReferralDetails,
            DocumentId = entity.DocumentId,
            Title = entity.Title,
            CreatedBy = entity.CreatedBy,
            LastModifiedBy = entity.LastModifiedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedDate = entity.ModifiedDate
        };
    }

    public Referral Map(ReferralDto dto)
    {
        return new Referral
        {
            Id = dto.Id,
            CustomerId = dto.CustomerId,
            EmployeeId = dto.EmployeeId,
            ReferralDetails = dto.ReferralDetails,
            DocumentId = dto.DocumentId,
            Title = dto.Title,
            CreatedBy = dto.CreatedBy,
            LastModifiedBy = dto.LastModifiedBy,
            CreatedDate = dto.CreatedDate,
            ModifiedDate = dto.ModifiedDate
        };
    }

    public IEnumerable<ReferralDto> Map(IEnumerable<Referral> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<Referral> Map(IEnumerable<ReferralDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(ReferralDto dto, Referral entity)
    {
        entity.Id = dto.Id;
        entity.CustomerId = dto.CustomerId;
        entity.EmployeeId = dto.EmployeeId;
        entity.ReferralDetails = dto.ReferralDetails;
        entity.DocumentId = dto.DocumentId;
        entity.Title = dto.Title;
        entity.CreatedBy = dto.CreatedBy;
        entity.LastModifiedBy = dto.LastModifiedBy;
        entity.CreatedDate = dto.CreatedDate;
        entity.ModifiedDate = dto.ModifiedDate;
    }

    public void Map(Referral entity, ReferralDto dto)
    {
        dto.Id = entity.Id;
        dto.CustomerId = entity.CustomerId;
        dto.EmployeeId = entity.EmployeeId;
        dto.ReferralDetails = entity.ReferralDetails;
        dto.DocumentId = entity.DocumentId;
        dto.CreatedBy = entity.CreatedBy;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedDate = entity.ModifiedDate;
    }

    public void Map(IEnumerable<ReferralDto> dtos, IEnumerable<Referral> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<Referral> entities, IEnumerable<ReferralDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}
