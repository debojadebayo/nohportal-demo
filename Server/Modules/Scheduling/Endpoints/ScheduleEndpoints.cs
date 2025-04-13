using ComposedHealthBase.Server.BaseModule.Endpoints;
using Shared.DTOs.Scheduling;
using Server.Modules.Scheduling.Entities;

namespace Server.Modules.Scheduling.Endpoints
{
	public class ClinicianEndpoints : BaseEndpoints<Clinician, ClinicianDto>, IEndpoints {}
}