using ComposedHealthBase.Server.Mappers;
using Server.Modules.Scheduling.Entities;
using Shared.DTOs.Scheduling;
using Shared.Enums;
using Shared.Factories.Scheduling;
using System.Text.Json;

public class ReferralMapper : IMapper<Referral, ReferralDto>
{
    public ReferralDto Map(Referral entity)
    {
        var dto = new ReferralDto
        {
            Id = entity.Id,
            ReferralDetails = entity.ReferralDetails,
            Title = entity.Title,
            ReferralStatus = entity.ReferralStatus,
            ReferralType = entity.ReferralType,
            CustomerId = entity.CustomerId,
            EmployeeId = entity.EmployeeId,
            RelatedDocumentIds = entity.RelatedDocumentIds.ToList(),
            CreatedBy = entity.CreatedBy,
            LastModifiedBy = entity.LastModifiedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedDate = entity.ModifiedDate
        };

        dto.Details = DeserializeDetails(entity.Details, entity.ReferralType);
        return dto;
    }

    public Referral Map(ReferralDto dto)
    {
        var entity = new Referral
        {
            ReferralDetails = dto.ReferralDetails,
            Title = dto.Title,
            ReferralStatus = dto.ReferralStatus,
            ReferralType = dto.ReferralType,
            CustomerId = dto.CustomerId,
            EmployeeId = dto.EmployeeId,
            RelatedDocumentIds = dto.RelatedDocumentIds.ToArray(),
        };

        if (dto.Details != null)
        {
            entity.Details = JsonSerializer.SerializeToDocument(dto.Details, dto.Details.GetType());
        }

        return entity;
    }

    private IReferralDetailsDto? DeserializeDetails(JsonDocument? details, ReferralTypeEnum type)
    {
        if (details == null)
        {
            return null;
        }

        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return type switch
            {
                ReferralTypeEnum.CaseReferral => JsonSerializer.Deserialize<CaseReferralDetailsDto>(details, options),
                ReferralTypeEnum.PphaClinFitnessCertificate => JsonSerializer.Deserialize<PphaClinFitnessCertificateDto>(details, options),
                ReferralTypeEnum.PphaFitnessCertificate => JsonSerializer.Deserialize<PphaFitnessCertificateDto>(details, options),
                ReferralTypeEnum.PphaForm_Admin => JsonSerializer.Deserialize<PphaFormAdminDto>(details, options),
                ReferralTypeEnum.PphaForm_Student => JsonSerializer.Deserialize<PphaFormStudentDto>(details, options),
                ReferralTypeEnum.PphaForm_ClinStudent => JsonSerializer.Deserialize<PphaFormClinStudentDto>(details, options),
                ReferralTypeEnum.PphaForm_ClinWorker => JsonSerializer.Deserialize<PphaFormClinWorkerDto>(details, options),
                ReferralTypeEnum.PphaForm_ManWorker => JsonSerializer.Deserialize<PphaFormManWorkerDto>(details, options),
                ReferralTypeEnum.StudentClinFitnessCertificate => JsonSerializer.Deserialize<StudentClinFitnessCertificateDto>(details, options),
                _ => null
            };
        }
        catch (JsonException ex)
        {
            // Log the error if you have logging configured
            // _logger?.LogWarning(ex, "Failed to deserialize details for referral type {Type}, creating new instance", type);

            // Fallback to null to indicate no form completed
            return null;
        }
    }

    public void Map(ReferralDto dto, Referral entity)
    {
        entity.ReferralDetails = dto.ReferralDetails;
        entity.Title = dto.Title;
        entity.ReferralStatus = dto.ReferralStatus;
        entity.ReferralType = dto.ReferralType;
        entity.CustomerId = dto.CustomerId;
        entity.EmployeeId = dto.EmployeeId;
        entity.RelatedDocumentIds = dto.RelatedDocumentIds.ToArray();

        if (dto.Details != null)
        {
            entity.Details = JsonSerializer.SerializeToDocument(dto.Details, dto.Details.GetType());
        }
        else
        {
            entity.Details = null;
        }
    }

    public void Map(Referral entity, ReferralDto dto)
    {
        var mappedDto = Map(entity);
        dto.Id = mappedDto.Id;
        dto.ReferralDetails = mappedDto.ReferralDetails;
        dto.Title = mappedDto.Title;
        dto.ReferralStatus = mappedDto.ReferralStatus;
        dto.ReferralType = mappedDto.ReferralType;
        dto.Details = mappedDto.Details;
        dto.CustomerId = mappedDto.CustomerId;
        dto.EmployeeId = mappedDto.EmployeeId;
        dto.RelatedDocumentIds = mappedDto.RelatedDocumentIds;
        dto.CreatedBy = mappedDto.CreatedBy;
        dto.LastModifiedBy = mappedDto.LastModifiedBy;
        dto.CreatedDate = mappedDto.CreatedDate;
        dto.ModifiedDate = mappedDto.ModifiedDate;
    }

    public IEnumerable<ReferralDto> Map(IEnumerable<Referral> entities) => entities.Select(Map);

    public IEnumerable<Referral> Map(IEnumerable<ReferralDto> dtos) => dtos.Select(Map);

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
