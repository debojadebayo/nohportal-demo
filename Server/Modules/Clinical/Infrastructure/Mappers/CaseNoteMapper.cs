using ComposedHealthBase.Server.Mappers;
using Server.Modules.Clinical.Entities;
using Shared.DTOs.Clinical;

public class CaseNoteMapper : IMapper<CaseNote, CaseNoteDto>
{
    public CaseNoteDto Map(CaseNote entity)
    {
        return new CaseNoteDto
        {
            CaseNotes = entity.CaseNotes,
            AppointmentType = entity.AppointmentType,
            FitForWorkStatus = entity.FitForWorkStatus,
            RecommendedAdjustments = entity.RecommendedAdjustments,
            IsFollowUpNeeded = entity.IsFollowUpNeeded,
            FollowUpDate = entity.FollowUpDate,
            FollowUpReasonForReferral = entity.FollowUpReasonForReferral,
            ClinicianId = entity.ClinicianId
        };
    }

    public CaseNote MapWithKeycloakId(CaseNoteDto dto, Guid keycloakId)
    {
        return new CaseNote
        {
            CaseNotes = dto.CaseNotes,
            AppointmentType = dto.AppointmentType,
            FitForWorkStatus = dto.FitForWorkStatus,
            RecommendedAdjustments = dto.RecommendedAdjustments,
            IsFollowUpNeeded = dto.IsFollowUpNeeded,
            FollowUpDate = dto.FollowUpDate,
            FollowUpReasonForReferral = dto.FollowUpReasonForReferral,
            ClinicianId = dto.ClinicianId
        };
    }

    public CaseNote Map(CaseNoteDto dto)
    {
        throw new NotImplementedException("Map method without KeycloakId is not implemented. Use MapWithKeycloakId instead.");
    }

    public IEnumerable<CaseNoteDto> Map(IEnumerable<CaseNote> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<CaseNote> Map(IEnumerable<CaseNoteDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(CaseNoteDto dto, CaseNote entity)
    {
        entity.CaseNotes = dto.CaseNotes;
        entity.AppointmentType = dto.AppointmentType;
        entity.FitForWorkStatus = dto.FitForWorkStatus;
        entity.RecommendedAdjustments = dto.RecommendedAdjustments;
        entity.IsFollowUpNeeded = dto.IsFollowUpNeeded;
        entity.FollowUpDate = dto.FollowUpDate;
        entity.FollowUpReasonForReferral = dto.FollowUpReasonForReferral;
        entity.ClinicianId = dto.ClinicianId;
    }

    public void Map(CaseNote entity, CaseNoteDto dto)
    {
        dto.CaseNotes = entity.CaseNotes;
        dto.AppointmentType = entity.AppointmentType;
        dto.FitForWorkStatus = entity.FitForWorkStatus;
        dto.RecommendedAdjustments = entity.RecommendedAdjustments;
        dto.IsFollowUpNeeded = entity.IsFollowUpNeeded;
        dto.FollowUpDate = entity.FollowUpDate;
        dto.FollowUpReasonForReferral = entity.FollowUpReasonForReferral;
        dto.ClinicianId = entity.ClinicianId;
    }

    public void Map(IEnumerable<CaseNoteDto> dtos, IEnumerable<CaseNote> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<CaseNote> entities, IEnumerable<CaseNoteDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}
