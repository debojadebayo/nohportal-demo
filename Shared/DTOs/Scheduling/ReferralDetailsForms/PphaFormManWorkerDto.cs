using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces; 


namespace Shared.DTOs.Scheduling
{
    public class PphaFormManWorkerDto : ReferralDetailsDto
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

        // Health History Questions 
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

        // Neurological
        public bool BlackoutsSirezuresEpilepsy { get; set; }
        public bool RecurringHeadachesMigraines { get; set; }
        public bool HeadInjuryConcussionBalance { get; set; }
        public bool BrainHeadSurgery { get; set; }
        public bool NeurologicalConditions { get; set; }
        public string? NeurologicalDetails { get; set; } = string.Empty;

        // Cardiovascular
        public bool HeartIssues { get; set; }
        public bool ChestPain { get; set; }
        public bool HeartSurgery { get; set; }
        public bool BloodPressureConcerns { get; set; }
        public bool VaricoseVeinsHemorrhoidsThrombosis { get; set; }
        public string? CardiovascularDetails { get; set; } = string.Empty;

        // Respiratory
        public bool AsthmaHistory { get; set; }
        public bool HayFever { get; set; }
        public bool CoughingChestTightnessWheezing { get; set; }
        public bool ChestLungConditions { get; set; }
        public bool ChestLungSurgery { get; set; }
        public string? RespiratoryDetails { get; set; } = string.Empty;

        // Metabolic
        public bool DiabetesDiagnosis { get; set; }
        public bool ThyroidEndocrineDisorders { get; set; }
        public bool BloodDisorder { get; set; }
        public string MetabolicDetails { get; set; } = string.Empty;

        // Gastro-Intestinal
        public bool StomachIssues { get; set; }
        public bool BowelIssues { get; set; }
        public bool StomachBowelSurgery { get; set; }
        public string? GastroIntestinalDetails { get; set; } = string.Empty;

        // Skin
        public bool SkinConditionAllergy { get; set; }
        public bool SkinConditionDiagnosis { get; set; }
        public bool SkinConditionTreatment { get; set; }
        public string? SkinDetails { get; set; } = string.Empty;

        // Eyes
        public bool GlassesContactLenses { get; set; }
        public bool ColourVisionDeficiency { get; set; }
        public bool EyeInjuryDiseaseSquint { get; set; }
        public bool EyeSurgery { get; set; }
        public string? EyeDetails { get; set; } = string.Empty;

        // Ears
        public bool HearingDifficulties { get; set; }
        public bool HearingAid { get; set; }
        public bool FamilyHearingHistory { get; set; }
        public bool LoudNoiseExposure { get; set; }
        public bool Tinnitus { get; set; }
        public bool EarDiseaseInfections { get; set; }
        public bool EarSurgery { get; set; }
        public string? EarDetails { get; set; } = string.Empty;

        // Musculoskeletal
        public bool BackNeckJointProblems { get; set; }
        public bool BackNeckJointTreatment { get; set; }
        public bool FracturesInjuries { get; set; }
        public string? MusculoskeletalDetails { get; set; } = string.Empty;

        // Mental Health
        public bool DepressionAnxiety { get; set; }
        public bool OtherMentalHealthConcerns { get; set; }
        public bool PsychiatricConditionsPhobias { get; set; }
        public bool EatingDisorder { get; set; }
        public bool AlcoholDrugMisuse { get; set; }
        public string MentalHealthDetails { get; set; } = string.Empty;

        // Lifestyle and Hobbies
        public bool NoisyHobbies { get; set; }
        public string NoisyHobbiesDetails { get; set; } = string.Empty;

        // Work Related History Questions (from template)
        public bool AbsentFromWorkDueToIllness { get; set; }
        public string? AbsentFromWorkDetails { get; set; }
        public bool LeftEmploymentMedicalReasons { get; set; }
        public string? LeftEmploymentDetails { get; set; }
        public bool DeniedDrivingLicenceMedical { get; set; }
        public string? DeniedDrivingLicenceDetails { get; set; }
        public bool WorkRelatedHealthCondition { get; set; }
        public string? WorkRelatedHealthDetails { get; set; }
        public bool StatutoryMedicalRoles { get; set; }
        public string? StatutoryMedicalDetails { get; set; }

        // Employment history
        public List<EmploymentHistoryEntry> EmploymentHistory { get; set; } = new List<EmploymentHistoryEntry>();
        

        // Declaration
        public bool DeclarationSigned { get; set; }
        public DateTime? DeclarationDate { get; set; }
        public string DeclarationSignature { get; set; } = string.Empty;

        // Additional Information
        public string AdditionalComments { get; set; } = string.Empty;
    }
}