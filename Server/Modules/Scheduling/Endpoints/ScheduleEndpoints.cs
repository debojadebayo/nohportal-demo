using ComposedHealthBase.Server.Endpoints;
using Shared.DTOs.Scheduling;
using Server.Modules.Scheduling.Entities;
using Server.Modules.Scheduling.Infrastructure.Database;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using ComposedHealthBase.Server.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ComposedHealthBase.Server.Queries;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Entities;
using System.Linq.Expressions;
using System.Security.Claims;


namespace Server.Modules.Scheduling.Endpoints
{
	public class ClinicianEndpoints : BaseEndpoints<Clinician, ClinicianDto, SchedulingDbContext>, IEndpoints
	{
		public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/clinician");

			group.MapGet("/GetAllCliniciansWithSchedules", (
				[FromServices] SchedulingDbContext dbContext,
				[FromServices] IMapper<Clinician, ClinicianDto> mapper,
				[FromServices] GetAllQuery<Clinician, ClinicianDto, SchedulingDbContext> getAllQuery,
				ClaimsPrincipal user,
				[FromQuery] long tenantId = 0,
				[FromQuery] long subjectId = 0
			) => GetAllCliniciansWithSchedules(getAllQuery, user, tenantId, subjectId));

			return endpoints;
		}

		protected async Task<IResult> GetAllCliniciansWithSchedules(
			GetAllQuery<Clinician, ClinicianDto, SchedulingDbContext> getAllQuery,
			ClaimsPrincipal user,
			long tenantId,
			long subjectId)
		{
			try
			{
				var allEntities = await getAllQuery.Handle(user, tenantId, subjectId, x => x.CalendarItems);
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