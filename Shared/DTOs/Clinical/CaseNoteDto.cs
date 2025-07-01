using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using Shared.DTOs;
using Shared.Enums;
using System;

namespace Shared.DTOs.Clinical
{
    public class CaseNoteDto : BaseDto<CaseNoteDto>, IDto, IAuditDto, ILazyLookup
    {
        public string? CaseNotes { get; set; }
        public AppointmentTypeEnum AppointmentType { get; set; }
        public FitForWorkStatusEnum FitForWorkStatus { get; set; }
        public string? RecommendedAdjustments { get; set; }
        public bool IsFollowUpNeeded { get; set; }
        public DateTime? FollowUpDate { get; set; }
        public string? FollowUpReasonForReferral { get; set; }
        public Guid ClinicianId { get; set; }
        public string? LocalEncryptionKey { get; set; }
        public string DisplayName { get; set; } = string.Empty;
    }
}
