using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
    public class PphaClinFitnessCertificateDto : ReferralDetailsDto
    {
        // Personal Information
        public string Name { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string RoleAppliedFor { get; set; } = string.Empty;
        public DateTime? DateOfAssessment { get; set; }

        // Fitness Assessment (from NOHMED 136 template)
        public bool FitForAllAspectsOfRole { get; set; }
        public bool FitForNonClinicalDutiesOnly { get; set; }
        public bool TemporarilyUnfitForRole { get; set; }
        public bool UnfitForRole { get; set; }
        public bool AdjustmentsRequired { get; set; }
        public string? AdjustmentsDetails { get; set; } = string.Empty;

        // Clinical Work Clearance
        public bool ClearedForExposureProneProcedures { get; set; }
        public bool ClearedForNonExposureProneClinicalWork { get; set; }

        // Immunization Documentation
        public bool TBImmunityEvidence { get; set; }
        public bool RubellaImmunityEvidence { get; set; }
        public bool MeaslesImmunityEvidence { get; set; }
        public bool VaricellaImmunityEvidence { get; set; }
        public bool HepatitisBImmunityEvidence { get; set; }
        public bool HepatitisCScreeningEvidence { get; set; }
        public bool HIVScreeningEvidence { get; set; }
    

        // Clinician Details
        public string ClinicianName { get; set; } = string.Empty;
        public string ClinicianJobTitle { get; set; } = string.Empty;
        public string ClinicianSignature { get; set; } = string.Empty;
        public DateTime? SignatureDate { get; set; }
    }
}