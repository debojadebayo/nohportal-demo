using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
    public class PphaFitnessCertificateDto : BaseDto<PphaFitnessCertificateDto>, IReferralDetailsDto
    {
        public Guid? ReferralId { get; set; }

        // Employee Information
        public string Name { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string RoleAppliedFor { get; set; } = string.Empty;
        public DateTime? DateOfAssessment { get; set; }

        // Fitness Assessment (from NOHMED 137 template)
        public bool FitForWork { get; set; }
        public bool TemporarilyUnfitForWork { get; set; }
        public bool UnfitForWork { get; set; }
        public bool AdjustmentsRequired { get; set; }
        public string? AdjustmentsDetails { get; set; } = string.Empty;

        // Clinician Details
        public string ClinicianName { get; set; } = string.Empty;
        public string ClinicianJobTitle { get; set; } = string.Empty;
        public string ClinicianSignature { get; set; } = string.Empty;
        public DateTime? SignatureDate { get; set; }
    }
}