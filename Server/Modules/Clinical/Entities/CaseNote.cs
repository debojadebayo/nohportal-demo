using ComposedHealthBase.Server.Entities;
using Shared.Enums;
using ComposedHealthBase.Shared.DTOs;
using System;

namespace Server.Modules.Clinical.Entities;

public class CaseNote : BaseEntity<CaseNote>, IEntity, IAuditEntity
{
    public string? CaseNotes { get; set; }
    public AppointmentTypeEnum AppointmentType { get; set; }
    public FitForWorkStatusEnum FitForWorkStatus { get; set; }
    public string? RecommendedAdjustments { get; set; }
    public bool IsFollowUpNeeded { get; set; }
    public DateTime? FollowUpDate { get; set; }
    public string? FollowUpReasonForReferral { get; set; }
    public Guid ClinicianId { get; set; }
}