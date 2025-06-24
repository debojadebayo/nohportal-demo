using ComposedHealthBase.Server.Mappers;
using Server.Modules.Clinical.Entities;
using Shared.DTOs.Clinical;

public class CaseNoteMapper : IMapper<CaseNote, CaseNoteDto>
{
    public CaseNoteDto Map(CaseNote entity)
    {
        return new CaseNoteDto
        {
            Id = entity.Id,
            CaseNotes = entity.CaseNotes,
            AppointmentType = entity.AppointmentType,
            FitForWorkStatus = entity.FitForWorkStatus,
            RecommendedAdjustments = entity.RecommendedAdjustments,
            IsFollowUpNeeded = entity.IsFollowUpNeeded,
            FollowUpDate = entity.FollowUpDate,
            FollowUpReasonForReferral = entity.FollowUpReasonForReferral,
            ClinicianId = entity.ClinicianId,
            LocalEncryptionKey = entity.LocalEncryptionKey
        };
    }

    public CaseNote Map(CaseNoteDto dto)
    {
        return new CaseNote
        {
            Id = dto.Id,
            CaseNotes = dto.CaseNotes,
            AppointmentType = dto.AppointmentType,
            FitForWorkStatus = dto.FitForWorkStatus,
            RecommendedAdjustments = dto.RecommendedAdjustments,
            IsFollowUpNeeded = dto.IsFollowUpNeeded,
            FollowUpDate = dto.FollowUpDate,
            FollowUpReasonForReferral = dto.FollowUpReasonForReferral,
            ClinicianId = dto.ClinicianId,
            LocalEncryptionKey = dto.LocalEncryptionKey
        };
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
        entity.Id = dto.Id;
        entity.CaseNotes = dto.CaseNotes;
        entity.AppointmentType = dto.AppointmentType;
        entity.FitForWorkStatus = dto.FitForWorkStatus;
        entity.RecommendedAdjustments = dto.RecommendedAdjustments;
        entity.IsFollowUpNeeded = dto.IsFollowUpNeeded;
        entity.FollowUpDate = dto.FollowUpDate;
        entity.FollowUpReasonForReferral = dto.FollowUpReasonForReferral;
        entity.ClinicianId = dto.ClinicianId;
        entity.LocalEncryptionKey = dto.LocalEncryptionKey;
    }

    public void Map(CaseNote entity, CaseNoteDto dto)
    {
        dto.Id = entity.Id;
        dto.CaseNotes = entity.CaseNotes;
        dto.AppointmentType = entity.AppointmentType;
        dto.FitForWorkStatus = entity.FitForWorkStatus;
        dto.RecommendedAdjustments = entity.RecommendedAdjustments;
        dto.IsFollowUpNeeded = entity.IsFollowUpNeeded;
        dto.FollowUpDate = entity.FollowUpDate;
        dto.FollowUpReasonForReferral = entity.FollowUpReasonForReferral;
        dto.ClinicianId = entity.ClinicianId;
        dto.LocalEncryptionKey = entity.LocalEncryptionKey;
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
