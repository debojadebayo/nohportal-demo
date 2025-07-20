using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
    public class PphaFormAdminDto : ReferralDetailsDto
    {
        // Employee Information
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PreviousSurname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? GpName { get; set; }
        public string? GpAddress { get; set; }

        // Employment Details
        public string JobTitle { get; set; } = string.Empty;
        public string CompanyOrganisation { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public bool IsPermanent { get; set; }
        public bool HasAppliedBefore { get; set; }
        public DateTime? StartDate { get; set; }
        public bool RoleIncludesManualHandling { get; set; }
        public bool RoleIncludesDriving { get; set; }
        public bool RoleIncludesDisplayScreenEquipment { get; set; }
        public bool RoleIncludesWorkingWithVulnerableAdults { get; set; }
        public bool RoleIncludesWorkingWithChildren { get; set; }


        // Health History Questions (from template)
        public bool PhysicalIllnessAffectsWork { get; set; }
        public string? PhysicalIllnessDetails { get; set; } = string.Empty;
        public bool PsychologicalIllnessAffectsWork { get; set; }
        public string? PsychologicalIllnessDetails { get; set; } = string.Empty;
        public bool WorkCausedIllness { get; set; }
        public string? WorkCausedIllnessDetails { get; set; } = string.Empty;
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

        // Work Related History Questions (from template)
        public bool AbsentFromWorkDueToIllness { get; set; }
        public string? AbsentFromWorkDetails { get; set; } = string.Empty;
        public bool LeftEmploymentMedicalReasons { get; set; }
        public string? LeftEmploymentDetails { get; set; } = string.Empty;
        public bool DeniedDrivingLicenceMedical { get; set; }
        public string? DeniedDrivingLicenceDetails { get; set; } = string.Empty;
        public bool WorkRelatedHealthCondition { get; set; }
        public string? WorkRelatedHealthDetails { get; set; } = string.Empty;
        public bool StatutoryMedicalRoles { get; set; }
        public string? StatutoryMedicalDetails { get; set; } = string.Empty;
        public string? AdditionalRelevantInformation { get; set; } = string.Empty;

        // Medical Consent (from templates)
        public bool ConsentToMedicalRecordsDisclosure { get; set; }
        public bool WishToSeeMedicalInformationFirst { get; set; }

        // Declaration
        public bool DeclarationSigned { get; set; }
        public DateTime? DeclarationDate { get; set; }
        public string DeclarationSignature { get; set; } = string.Empty;

        // Additional Information
        public string AdditionalComments { get; set; } = string.Empty;
    }
}