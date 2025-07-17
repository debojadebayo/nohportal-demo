using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
    public class PphaFormStudentDto : BaseDto<PphaFormStudentDto>, IReferralDetailsDto
    {
        public Guid? ReferralId { get; set; }

        // Personal Information
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PreviousSurname { get; set; } = string.Empty;
        public DateTime? DateOfBirth { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string GpName { get; set; } = string.Empty;
        public string GpAddress { get; set; } = string.Empty;

        // University Course Details
        public string CourseAppliedFor { get; set; } = string.Empty;
        public string CourseDuration { get; set; } = string.Empty;
        public string University { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public DateTime? ProposedStartDate { get; set; }
        public string? StudentId { get; set; } = string.Empty;
        public bool ManualHandling { get; set; }
        public bool DisplayScreenEquipment { get; set; }
        public bool VulnerableAdultsChildren { get; set; }
        public bool ClinicalDutiesBodyFluidExposure { get; set; }

        // Health History Questions (from NOHMED 133 template)
        public bool PhysicalIllnessAffectsStudy { get; set; }
        public string? PhysicalIllnessDetails { get; set; } = string.Empty;
        public bool PsychologicalIllnessAffectsStudy { get; set; }
        public string? PsychologicalIllnessDetails { get; set; } = string.Empty;
        public bool StudyPlacementCausedIllness { get; set; }
        public string? StudyPlacementIllnessDetails { get; set; } = string.Empty;
        public bool CurrentTreatmentOrMedication { get; set; }
        public string? CurrentTreatmentDetails { get; set; } = string.Empty;
        public bool TakingMedication { get; set; }
        public string? MedicationDetails { get; set; } = string.Empty;
        public bool HasAllergies { get; set; }
        public string? AllergiesDetails { get; set; } = string.Empty;
        public bool NeedsAdjustments { get; set; }
        public string? AdjustmentsDetails { get; set; } = string.Empty;
        public bool DrinksAlcohol { get; set; }
        public string? AlcoholUnitsPerWeek { get; set; } = string.Empty;

        // Medical Consent (from templates)
        public bool ConsentToMedicalRecordsDisclosure { get; set; }
        public bool WishToSeeMedicalInformationFirst { get; set; }

        // Declaration
        public bool DeclarationSigned { get; set; }
        public DateTime? DeclarationDate { get; set; }
        public string DeclarationSignature { get; set; } = string.Empty;

        // Additional Information
        public string? AdditionalRelevantInformation { get; set; } = string.Empty;
    }
}