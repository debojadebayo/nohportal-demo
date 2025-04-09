using ComposedHealthBase.Server.BaseModule.Endpoints;
using Shared.DTOs.Scheduling;
using Server.Modules.Scheduling.Entities;

namespace Server.Modules.Scheduling.Endpoints
{
		public class EmployeeEndpoints : BaseEndpoints<Schedule, ScheduleDto> {}
		public class CustomerEndpoints : BaseEndpoints<Referral, ReferralDto> {}
}