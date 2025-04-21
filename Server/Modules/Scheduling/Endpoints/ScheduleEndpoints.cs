using ComposedHealthBase.Server.Endpoints;
using Shared.DTOs.Scheduling;
using Server.Modules.Scheduling.Entities;
using Server.Modules.Scheduling.Infrastructure.Database;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using ComposedHealthBase.Server.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Server.Modules.Scheduling.Infrastructure.Queries;

namespace Server.Modules.Scheduling.Endpoints
{
	public class ClinicianEndpoints : BaseEndpoints<Clinician, ClinicianDto, SchedulingDbContext>, IEndpoints { 
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/clinician");

			group.MapGet("/GetAllCliniciansWithSchedules", ([FromServices] SchedulingDbContext dbContext, [FromServices] IMapper<Clinician, ClinicianDto> mapper) => GetAll(dbContext, mapper));

			return endpoints;
		}

		protected async Task<IResult> GetAllCliniciansWithSchedules(SchedulingDbContext dbContext, IMapper<Clinician, ClinicianDto> mapper)
		{
			try
			{
				var allEntities = await new GetAllCliniciansWithSchedulesQuery(dbContext, mapper).Handle();

				if (allEntities == null || !allEntities.Any())
				{
					return Results.NotFound($"No clinician entities found.");
				}

				return Results.Ok(allEntities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving clinician entities.");
			}
		}
	}
	public class ReferralEndpoints : BaseEndpoints<Referral, ReferralDto, SchedulingDbContext>, IEndpoints { }
	public class ScheduleEndpoints : BaseEndpoints<Schedule, ScheduleDto, SchedulingDbContext>, IEndpoints { }
}