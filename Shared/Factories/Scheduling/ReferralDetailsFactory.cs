using System;
using System.Collections.Generic;
using Shared.DTOs.Scheduling;
using Shared.Enums;

namespace Shared.Factories.Scheduling
{
    public static class ReferralDetailsFactory
    {
        private static readonly Dictionary<ReferralTypeEnum, Func<ReferralDetailsDto>> _registry =
            new()
            {
                { ReferralTypeEnum.CaseReferral, () => new CaseReferralDetailsDto() },
                { ReferralTypeEnum.PphaForm_Admin, () => new PphaFormAdminDto() },
                { ReferralTypeEnum.PphaForm_Student, () => new PphaFormStudentDto() },
                { ReferralTypeEnum.PphaForm_ClinStudent, () => new PphaFormClinStudentDto() },
                { ReferralTypeEnum.PphaForm_ClinWorker, () => new PphaFormClinWorkerDto() },
                { ReferralTypeEnum.PphaForm_ManWorker, () => new PphaFormManWorkerDto() },
                { ReferralTypeEnum.PphaFitnessCertificate, () => new PphaFitnessCertificateDto() },
                { ReferralTypeEnum.PphaClinFitnessCertificate, () => new PphaClinFitnessCertificateDto() },
                { ReferralTypeEnum.StudentClinFitnessCertificate, () => new StudentClinFitnessCertificateDto() }
            };

        public static ReferralDetailsDto? Create(ReferralTypeEnum referralType)
        {
            return _registry.TryGetValue(referralType, out var factory) ? factory() : null;
        }

        public static void Register(ReferralTypeEnum referralType, Func<ReferralDetailsDto> factory)
        {
            _registry[referralType] = factory;
        }
    }
}