using ComposedHealthBase.Server.Entities;
using Shared.Enums;
using System;

namespace Server.Modules.Clinical.Entities;

public class ClinicalReport : BaseEntity<ClinicalReport>, IEntity, IAuditEntity
{
    public Guid EmployeeId { get; set; }
    public string? EmployeeName { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Company { get; set; }
    public string? JobRole { get; set; }
    public AppointmentTypeEnum ReportType { get; set; }
    public DateTime AssessmentDate { get; set; }
    public DateTime DateReportSubmitted { get; set; }
    public string? ReportNotes { get; set; }
    public Guid ClinicianId { get; set; }
}