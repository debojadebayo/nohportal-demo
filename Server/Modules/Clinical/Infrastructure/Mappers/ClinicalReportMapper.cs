using ComposedHealthBase.Server.Mappers;
using Server.Modules.Clinical.Entities;
using Shared.DTOs.Clinical;

public class ClinicalReportMapper : IMapper<ClinicalReport, ClinicalReportDto>
{
    public ClinicalReportDto Map(ClinicalReport entity)
    {
        return new ClinicalReportDto
        {
            Id = entity.Id,
            EmployeeId = entity.EmployeeId,
            EmployeeName = entity.EmployeeName,
            DateOfBirth = entity.DateOfBirth,
            Company = entity.Company,
            JobRole = entity.JobRole,
            ReportType = entity.ReportType,
            AssessmentDate = entity.AssessmentDate,
            DateReportSubmitted = entity.DateReportSubmitted,
            ReportNotes = entity.ReportNotes,
            ClinicianId = entity.ClinicianId
        };
    }

    public ClinicalReport Map(ClinicalReportDto dto)
    {
        return new ClinicalReport
        {
            Id = dto.Id,
            EmployeeId = dto.EmployeeId,
            EmployeeName = dto.EmployeeName,
            DateOfBirth = dto.DateOfBirth,
            Company = dto.Company,
            JobRole = dto.JobRole,
            ReportType = dto.ReportType,
            AssessmentDate = dto.AssessmentDate,
            DateReportSubmitted = dto.DateReportSubmitted,
            ReportNotes = dto.ReportNotes,
            ClinicianId = dto.ClinicianId
        };
    }

    public IEnumerable<ClinicalReportDto> Map(IEnumerable<ClinicalReport> entities)
    {
        return entities.Select(Map);
    }

    public IEnumerable<ClinicalReport> Map(IEnumerable<ClinicalReportDto> dtos)
    {
        return dtos.Select(Map);
    }

    public void Map(ClinicalReportDto dto, ClinicalReport entity)
    {
        entity.Id = dto.Id;
        entity.EmployeeId = dto.EmployeeId;
        entity.EmployeeName = dto.EmployeeName;
        entity.DateOfBirth = dto.DateOfBirth;
        entity.Company = dto.Company;
        entity.JobRole = dto.JobRole;
        entity.ReportType = dto.ReportType;
        entity.AssessmentDate = dto.AssessmentDate;
        entity.DateReportSubmitted = dto.DateReportSubmitted;
        entity.ReportNotes = dto.ReportNotes;
        entity.ClinicianId = dto.ClinicianId;
    }

    public void Map(ClinicalReport entity, ClinicalReportDto dto)
    {
        dto.Id = entity.Id;
        dto.EmployeeId = entity.EmployeeId;
        dto.EmployeeName = entity.EmployeeName;
        dto.DateOfBirth = entity.DateOfBirth;
        dto.Company = entity.Company;
        dto.JobRole = entity.JobRole;
        dto.ReportType = entity.ReportType;
        dto.AssessmentDate = entity.AssessmentDate;
        dto.DateReportSubmitted = entity.DateReportSubmitted;
        dto.ReportNotes = entity.ReportNotes;
        dto.ClinicianId = entity.ClinicianId;
    }

    public void Map(IEnumerable<ClinicalReportDto> dtos, IEnumerable<ClinicalReport> entities)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(dtosArray[i], entitiesArray[i]);
        }
    }

    public void Map(IEnumerable<ClinicalReport> entities, IEnumerable<ClinicalReportDto> dtos)
    {
        var dtosArray = dtos.ToArray();
        var entitiesArray = entities.ToArray();
        for (int i = 0; i < Math.Min(dtosArray.Length, entitiesArray.Length); i++)
        {
            Map(entitiesArray[i], dtosArray[i]);
        }
    }
}
