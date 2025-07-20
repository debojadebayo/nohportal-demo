using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;

namespace Shared.DTOs.Scheduling
{
    public class CaseReferralDetailsDto : ReferralDetailsDto
    {
        // Section 1: Referring Manager
        public Guid? ReferringManagerId { get; set; }
        public Guid? ReferringManager2Id { get; set; }
        public Guid? CustomerId { get; set; }
        public DateTime? ReferralDate { get; set; }

        // Section 2: HR Contact
        public Guid? HrContactId { get; set; }

        // Section 3: Employee Details
        public Guid? EmployeeId { get; set; }

        // Section 4: Finance
        public string PurchaseOrderNumber { get; set; } = string.Empty;
        public string FinanceContactName { get; set; } = string.Empty;
        public string FinanceContactNumber { get; set; } = string.Empty;

        // Reason for Referral
        public string ReasonForReferral { get; set; } = string.Empty;

        // Section 5: Employee Informed
        public bool EmployeeInformed { get; set; }
        public DateTime? DateEmployeeInformed { get; set; }
        public string AccessibilityNeeds { get; set; } = string.Empty;

        // Section 6: Employee Work Pattern
        public bool IsTemporary { get; set; }
        public bool IsPermanent { get; set; }
        public bool IsCasual { get; set; }
        public bool IsFullTime { get; set; }
        public bool IsPartTime { get; set; }
        public bool IsNightWorking { get; set; }
        public bool IsRotationalShift { get; set; }

        // Section 7: Employee's Role & Hazards
        public bool ManualHandlingHeavyLoads { get; set; }
        public bool ManualHandlingOther { get; set; }
        public bool VibratingEquipment { get; set; }
        public bool Noise { get; set; }
        public bool RepetitiveWork { get; set; }
        public bool ProlongedStanding { get; set; }
        public bool ProlongedSitting { get; set; }
        public bool ExtremesOfTemperature { get; set; }
        public bool ConfinedSpaces { get; set; }
        public bool AdverseWeatherConditions { get; set; }
        public bool WorkingAtHeights { get; set; }
        public bool DrivingLgcPcvMinibus { get; set; }
        public bool FumesDustGases { get; set; }
        public bool SolventsOilsPaints { get; set; }
        public bool PesticidesHerbicidesInsecticides { get; set; }
        public bool DetergentCleaningChemicals { get; set; }
        public bool ExposureProneProcedures { get; set; }
        public bool BiologicalHazards { get; set; }
        public string EmployeeDutiesDescription { get; set; } = string.Empty;

        // Section 8: Purpose of Referral
        public bool ProlongedSicknessAbsence { get; set; }
        public bool RecurrentShortTermSickness { get; set; }
        public bool ConcernsAboutWorkPerformance { get; set; }
        public bool AdviceOnWorkAdaptations { get; set; }
        public bool HealthSurveillanceProcesses { get; set; }
        public bool OccupationalExposureHazardConcerns { get; set; }
        public bool AdviceFollowingWorkplaceIllnessInjury { get; set; }
        public bool FitnessToAttendDisciplinary { get; set; }
        public bool AdviceRelatedToSubstanceMisuse { get; set; }

        // Section 9: Attendance
        public string AttendanceRecord12Months { get; set; } = string.Empty;
        public DateTime? FirstDateOfAbsence { get; set; }

        // Absence categories
        public bool BackAndNeckProblems { get; set; }
        public bool OtherMusculoskeletal { get; set; }
        public bool EyeEarNoseMouthDental { get; set; }
        public bool GenitourinaryGynecological { get; set; }
        public bool HeartBpCirculation { get; set; }
        public bool Infections { get; set; }
        public bool Neurological { get; set; }
        public bool Hospitalisation { get; set; }
        public bool Skin { get; set; }
        public bool ChestRespiratory { get; set; }
        public bool PregnancyRelated { get; set; }
        public bool StomachLiverKidneyDigestion { get; set; }
        public bool StressDepressionAnxiety { get; set; }
        public bool OtherMentalHealth { get; set; }
        public bool Cancer { get; set; }

        // Section 10: Standard Questions
        public bool IsEmployeeFitForWork { get; set; }
        public bool WorkBasedFactorsImplication { get; set; }
        public bool SuggestedActionsForReturn { get; set; }
        public bool EqualityAct2010Related { get; set; }

        // Section 11: Additional Questions or Comments
        public string AdditionalQuestionsComments { get; set; } = string.Empty;
    }
}
