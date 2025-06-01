using ComposedHealthBase.Server.Mappers;
using Server.Modules.Scheduling.Entities;
using Shared.DTOs.Scheduling;

public class ScheduleMapper : IMapper<Schedule, ScheduleDto>
{
    public ScheduleDto Map(Schedule entity)
    {
        return new ScheduleDto
        {
            Id = entity.Id,
            CustomerId = entity.CustomerId,
            ReferralId = entity.ReferralId,
            EmployeeId = entity.EmployeeId,
            ClinicianId = entity.ClinicianId,
            StartTime = entity.Start,
            EndTime = entity.End,
            CreatedBy = entity.CreatedBy,
            LastModifiedBy = entity.LastModifiedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedDate = entity.ModifiedDate,
            Title = entity.Title,
            Description = entity.Description,
        };
    }

    public Schedule Map(ScheduleDto dto)
    {
        return new Schedule
        {
            CustomerId = dto.CustomerId,
            ReferralId = dto.ReferralId,
            EmployeeId = dto.EmployeeId,
            ClinicianId = dto.ClinicianId,
            Start = dto.StartTime,
            End = dto.EndTime,
            Title = dto.Title,
            Description = dto.Description,
        };
    }

    public IEnumerable<ScheduleDto> Map(IEnumerable<Schedule> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<Schedule> Map(IEnumerable<ScheduleDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(ScheduleDto dto, Schedule entity)
    {
        entity.CustomerId = dto.CustomerId;
        entity.ReferralId = dto.ReferralId;
        entity.EmployeeId = dto.EmployeeId;
        entity.ClinicianId = dto.ClinicianId;
        entity.Start = dto.StartTime;
        entity.End = dto.EndTime;
        entity.Title = dto.Title;
        entity.Description = dto.Description;
    }

    public void Map(Schedule entity, ScheduleDto dto)
    {
        dto.Id = entity.Id;
        dto.CustomerId = entity.CustomerId;
        dto.ReferralId = entity.ReferralId;
        dto.EmployeeId = entity.EmployeeId;
        dto.ClinicianId = entity.ClinicianId;
        dto.StartTime = entity.Start;
        dto.End = entity.End;
        dto.CreatedBy = entity.CreatedBy;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedDate = entity.ModifiedDate;
        dto.Title = entity.Title;
        dto.Description = entity.Description;
    }

    public void Map(IEnumerable<ScheduleDto> dtos, IEnumerable<Schedule> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<Schedule> entities, IEnumerable<ScheduleDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}
