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
            ReferralDetails = entity.ReferralDetails,
            EmployeeDocumentId = entity.EmployeeDocumentId,
            Title = entity.Title,
            ReferralStatus = entity.ReferralStatus,
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
            ReferralDetails = dto.ReferralDetails,
            EmployeeDocumentId = dto.EmployeeDocumentId,
            Title = dto.Title,
            ReferralStatus = dto.ReferralStatus
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
        entity.ReferralDetails = dto.ReferralDetails;
        entity.EmployeeDocumentId = dto.EmployeeDocumentId;
        entity.Title = dto.Title;
        entity.ReferralStatus = dto.ReferralStatus;
    }

    public void Map(Referral entity, ReferralDto dto)
    {
        dto.Id = entity.Id;
        dto.ReferralDetails = entity.ReferralDetails;
        dto.EmployeeDocumentId = entity.EmployeeDocumentId;
        dto.Title = entity.Title;
        dto.ReferralStatus = entity.ReferralStatus;
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
