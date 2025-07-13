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
            Title = entity.Title,
            ReferralStatus = entity.ReferralStatus,
            CreatedBy = entity.CreatedBy,
            LastModifiedBy = entity.LastModifiedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedDate = entity.ModifiedDate,
            CustomerId = entity.CustomerId,
            EmployeeId = entity.EmployeeId,
            RelatedDocumentIds = entity.RelatedDocumentIds.ToList(),
            Details = entity.Details ?? new ReferralDetailsDto { ReferralId = entity.Id }
        };
    }

    public Referral Map(ReferralDto dto)
    {
        return new Referral
        {
            ReferralDetails = dto.ReferralDetails,
            Title = dto.Title,
            ReferralStatus = dto.ReferralStatus,
            CustomerId = dto.CustomerId,
            EmployeeId = dto.EmployeeId,
            RelatedDocumentIds = dto.RelatedDocumentIds.ToArray(),
            Details = dto.Details
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
        entity.Title = dto.Title;
        entity.ReferralStatus = dto.ReferralStatus;
        entity.CustomerId = dto.CustomerId;
        entity.EmployeeId = dto.EmployeeId;
        entity.RelatedDocumentIds = dto.RelatedDocumentIds.ToArray();
        entity.Details = dto.Details;
    }

    public void Map(Referral entity, ReferralDto dto)
    {
        dto.Id = entity.Id;
        dto.ReferralDetails = entity.ReferralDetails;
        dto.Title = entity.Title;
        dto.ReferralStatus = entity.ReferralStatus;
        dto.CreatedBy = entity.CreatedBy;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedDate = entity.ModifiedDate;
        dto.CustomerId = entity.CustomerId;
        dto.EmployeeId = entity.EmployeeId;
        dto.RelatedDocumentIds = entity.RelatedDocumentIds.ToList();
        dto.Details = entity.Details ?? new ReferralDetailsDto { ReferralId = entity.Id };
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
