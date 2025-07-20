using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
    public class PphaFormClinStudentDto : ReferralDetailsDto
    {
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
        public string StudentId { get; set; } = string.Empty;
        public bool ManualHandling { get; set; }
        public bool DisplayScreenEquipment { get; set; }
        public bool VulnerableAdultsChildren { get; set; }
        public bool ClinicalDutiesBodyFluidExposure { get; set; }

        // Health History Questions (from NOHMED 132 template)
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

        // Declaration
        public bool DeclarationSigned { get; set; }
        public DateTime? DeclarationDate { get; set; }
        public string DeclarationSignature { get; set; } = string.Empty;

        // Additional Information
        public string AdditionalRelevantInformation { get; set; } = string.Empty;

        //? upload lab documents here
    }
}