using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Shared.Interfaces;
using System.Text.Json.Serialization;

namespace Shared.DTOs.Scheduling
{
    [JsonPolymorphic(TypeDiscriminatorPropertyName = "$type")]
    [JsonDerivedType(typeof(CaseReferralDetailsDto), "case")]
    [JsonDerivedType(typeof(PphaClinFitnessCertificateDto), "ppha-clin-fitness")]
    [JsonDerivedType(typeof(StudentClinFitnessCertificateDto), "student-clin-fitness")]
    [JsonDerivedType(typeof(PphaFitnessCertificateDto), "ppha-fitness")]
    [JsonDerivedType(typeof(PphaFormStudentDto), "ppha-student")]
    [JsonDerivedType(typeof(PphaFormAdminDto), "ppha-admin")]
    [JsonDerivedType(typeof(PphaFormClinStudentDto), "ppha-clin-student")]
    [JsonDerivedType(typeof(PphaFormManWorkerDto), "ppha-man-worker")]
    [JsonDerivedType(typeof(PphaFormClinWorkerDto), "ppha-clin-worker")]
    public abstract class ReferralDetailsDto : BaseDto<ReferralDetailsDto>
    {
        public Guid? ReferralId { get; set; }
    }

}