using ComposedHealthBase.Server.Endpoints;
using Shared.DTOs.Scheduling;
using Server.Modules.Scheduling.Entities;

namespace Server.Modules.Scheduling.Endpoints
{
	public class ClinicianEndpoints : BaseEndpoints<Clinician, ClinicianDto>, IEndpoints { }
	public class ReferralEndpoints : BaseEndpoints<Referral, ReferralDto>, IEndpoints { }
	public class ScheduleEndpoints : BaseEndpoints<Schedule, ScheduleDto>, IEndpoints { }
}