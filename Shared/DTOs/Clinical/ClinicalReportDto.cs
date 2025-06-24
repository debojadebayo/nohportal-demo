using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using Shared.DTOs;
using Shared.Enums;
using System;

namespace Shared.DTOs.Clinical
{
    public class ClinicalReportDto : BaseDto<ClinicalReportDto>, IDto, IAuditDto, ILazyLookup
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
        public string DisplayName { get; set; } = string.Empty;
    }
}
