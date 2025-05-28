using ComposedHealthBase.Server.Mappers;
using Server.Modules.Scheduling.Entities;
using Shared.DTOs.Scheduling;

public class ClinicianMapper : IMapper<Clinician, ClinicianDto>
{
    private readonly IMapper<Schedule, ScheduleDto> _mapper;
    public ClinicianMapper(IMapper<Schedule, ScheduleDto> mapper)
    {
        _mapper = mapper;
    }
    public ClinicianDto Map(Clinician entity)
    {
        return new ClinicianDto
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Telephone = entity.Telephone,
            Email = entity.Email,
            ClinicianType = entity.ClinicianType,
            RegulatorType = entity.RegulatorType,
            LicenceNumber = entity.LicenceNumber,
            AvatarImage = entity.AvatarImage,
            AvatarTitle = entity.AvatarTitle,
            AvatarDescription = entity.AvatarDescription,
            CreatedBy = entity.CreatedBy,
            LastModifiedBy = entity.LastModifiedBy,
            CreatedDate = entity.CreatedDate,
            ModifiedDate = entity.ModifiedDate,
            CalendarItems = _mapper.Map(entity.CalendarItems).ToList()
        };
    }

    public Clinician Map(ClinicianDto dto)
    {
        return new Clinician
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Telephone = dto.Telephone,
            Email = dto.Email,
            ClinicianType = dto.ClinicianType,
            RegulatorType = dto.RegulatorType,
            LicenceNumber = dto.LicenceNumber,
            AvatarImage = dto.AvatarImage,
            AvatarTitle = dto.AvatarTitle,
            AvatarDescription = dto.AvatarDescription
        };
    }

    public IEnumerable<ClinicianDto> Map(IEnumerable<Clinician> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<Clinician> Map(IEnumerable<ClinicianDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(ClinicianDto dto, Clinician entity)
    {
        entity.FirstName = dto.FirstName;
        entity.LastName = dto.LastName;
        entity.Telephone = dto.Telephone;
        entity.Email = dto.Email;
        entity.ClinicianType = dto.ClinicianType;
        entity.RegulatorType = dto.RegulatorType;
        entity.LicenceNumber = dto.LicenceNumber;
        entity.AvatarImage = dto.AvatarImage;
        entity.AvatarTitle = dto.AvatarTitle;
        entity.AvatarDescription = dto.AvatarDescription;
    }

    public void Map(Clinician entity, ClinicianDto dto)
    {
        dto.Id = entity.Id;
        dto.FirstName = entity.FirstName;
        dto.LastName = entity.LastName;
        dto.Telephone = entity.Telephone;
        dto.Email = entity.Email;
        dto.ClinicianType = entity.ClinicianType;
        dto.RegulatorType = entity.RegulatorType;
        dto.LicenceNumber = entity.LicenceNumber;
        dto.AvatarImage = entity.AvatarImage;
        dto.AvatarTitle = entity.AvatarTitle;
        dto.AvatarDescription = entity.AvatarDescription;
        // Schedules mapping can be handled with a ScheduleMapper if needed
        dto.CreatedBy = entity.CreatedBy;
        dto.LastModifiedBy = entity.LastModifiedBy;
        dto.CreatedDate = entity.CreatedDate;
        dto.ModifiedDate = entity.ModifiedDate;
    }

    public void Map(IEnumerable<ClinicianDto> dtos, IEnumerable<Clinician> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<Clinician> entities, IEnumerable<ClinicianDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}
