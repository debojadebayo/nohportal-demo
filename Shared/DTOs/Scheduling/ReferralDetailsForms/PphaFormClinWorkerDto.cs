using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
    public class PphaFormClinWorkerDto : BaseDto<PphaFormClinWorkerDto>, IReferralDetailsDto
    {
        public Guid? ReferralId { get; set; }

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
  

        // Health History Questions (from NOHMED 023 template)
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

        // Work Related History 
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

        //Disease history 
        public bool HadMeasles { get; set; }
        public bool HadMumps { get; set; }
        public bool HadRubella { get; set; }
        public bool HadChickenPox { get; set; }

        // Vaccination History - MMR/Varicella
        public bool HadMeaslesVaccination { get; set; }
        public DateTime? MeaslesVaccinationDate { get; set; }
        public bool HadRubellaVaccination { get; set; }
        public DateTime? RubellaVaccinationDate { get; set; }
        public bool MMRVaccination { get; set; }
        public DateTime? MMRVaccinationDate { get; set; }
        public bool VaricellaVaccination { get; set; }
        public DateTime? VaricellaVaccinationDate { get; set; }

        // Other Vaccinations
        public DateTime? Pertussis1stDate { get; set; }
        public DateTime? Pertussis2ndDate { get; set; }
        public DateTime? Pertussis3rdDate { get; set; }
        public DateTime? Polio1stDate { get; set; }
        public DateTime? Polio2ndDate { get; set; }
        public DateTime? Polio3rdDate { get; set; }
        public DateTime? Polio4thDate { get; set; }
        public DateTime? PolioBoosterDate { get; set; }
        public DateTime? Tetanus1stDate { get; set; }
        public DateTime? Tetanus2ndDate { get; set; }
        public DateTime? Tetanus3rdDate { get; set; }
        public DateTime? Tetanus4thDate { get; set; }
        public DateTime? TetanusBoosterDate { get; set; }
        public DateTime? Diphtheria1stDate { get; set; }
        public DateTime? Diphtheria2ndDate { get; set; }
        public DateTime? Diphtheria3rdDate { get; set; }
        public DateTime? Diphtheria4thDate { get; set; }
        public DateTime? DiphtheriaBoosterDate { get; set; }
        public DateTime? MeningitisCDate { get; set; }

        // Tuberculosis (TB) Assessment
        public bool TuberculosisDiagnosis { get; set; }
        public bool PersistentCough3Weeks { get; set; }
        public bool UnexplainedWeightLoss { get; set; }
        public bool NightSweatsOrFever { get; set; }
        public bool TBContactPastYear { get; set; }
        public bool FamilyTBHistory { get; set; }
        public bool TravelledHighTBCountry { get; set; }
        public string? TravelledHighTBCountryDetails { get; set; } = string.Empty;
        public bool TBVaccination { get; set; }
        public DateTime? TBVaccinationDate { get; set; }
        public bool VisibleTBVaccinationScar { get; set; }
        public bool PositiveTBTest { get; set; }
        public bool TBTreatmentHistory { get; set; }
        public bool RecentChestXray { get; set; }
        public bool ImmuneSystemConditions { get; set; }
        public bool ImmuneSuppressionMedication { get; set; }
        public string TBAssessmentDetails { get; set; } = string.Empty;

        // Hepatitis B Assessment
        public bool HepatitisBDiagnosis { get; set; }
        public bool JaundiceFatigueSymptoms { get; set; }
        public bool WorkedWithBloodTissue { get; set; }
        public bool HepatitisBContact { get; set; }
        public bool TravelledHighHepBCountry { get; set; }
        public bool OfferedHepBVaccination { get; set; }
        public DateTime? HepBVaccination1stDate { get; set; }
        public DateTime? HepBVaccination2ndDate { get; set; }
        public DateTime? HepBVaccination3rdDate { get; set; }
        public DateTime? HepBVaccination4thDate { get; set; }
        public DateTime? HepBBloodTestDate { get; set; }
        public string? HepBBloodTestResult { get; set; } = string.Empty;

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