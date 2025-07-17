using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
    public class StudentClinFitnessCertificateDto : BaseDto<StudentClinFitnessCertificateDto>, IReferralDetailsDto
    {
        public Guid? ReferralId { get; set; }

        // Student Information
        public string StudentName { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
        public string StudentNumber { get; set; } = string.Empty;
        public string CourseAppliedFor { get; set; } = string.Empty;
        public DateTime? DateOfAssessment { get; set; }

        // Fitness Assessment (from NOHMED 134 template)
        public bool FitToAttendStudies { get; set; }
        public bool FitToAttendClinicalPlacement { get; set; }
        public bool UnfitToAttendStudies { get; set; }
        public bool UnfitToAttendClinicalPlacements { get; set; }
        public bool ClearedForExposureProneProcedures { get; set; }
        public bool ClearedForNonExposureProneClinicalActivities { get; set; }

        // Immunization/Health Screening Documentation
        public bool TBImmunityEvidence { get; set; }
        public bool RubellaImmunityEvidence { get; set; }
        public bool MeaslesImmunityEvidence { get; set; }
        public bool VaricellaImmunityEvidence { get; set; }
        public bool HepatitisBImmunityEvidence { get; set; }
        public bool HepatitisCScreeningEvidence { get; set; }
        public bool HIVScreeningEvidence { get; set; }

        // Additional Information
        public string Comments { get; set; } = string.Empty;

        // Clinician Details
        public string ClinicianName { get; set; } = string.Empty;
        public string ClinicianJobTitle { get; set; } = string.Empty;
        public string ClinicianSignature { get; set; } = string.Empty;
        public DateTime? SignatureDate { get; set; }
    }
}