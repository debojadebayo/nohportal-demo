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
using Server.Modules.Scheduling.Infrastructure.Queries;
using Server.Modules.CommonModule.Interfaces;

namespace Server.Modules.Scheduling.Endpoints
{
	public class ClinicianEndpoints : BaseEndpoints<Clinician, ClinicianDto, SchedulingDbContext>, IEndpoints
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/clinician");

			group.MapGet("/GetAllCliniciansWithSchedules", ([FromServices] SchedulingDbContext dbContext, [FromServices] IMapper<Clinician, ClinicianDto> mapper) => GetAllCliniciansWithSchedules(dbContext, mapper));

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
	public class ReferralEndpoints : CommonScheduleEndpoints<Referral, ReferralDto, SchedulingDbContext>, IEndpoints { }
	public class ScheduleEndpoints : CommonScheduleEndpoints<Schedule, ScheduleDto, SchedulingDbContext>, IEndpoints { }
	public abstract class CommonScheduleEndpoints<T, TDto, SchedulingDbContext> : BaseEndpoints<T, TDto, SchedulingDbContext>
		where T : BaseEntity<T>, IFilterByEmployee, IFilterByCustomer
		where TDto : IDto
		where SchedulingDbContext : IDbContext<SchedulingDbContext>
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);

			var entityName = typeof(T).Name.ToLower();

			var group = endpoints.MapGroup($"/api/{entityName}");

			group.MapGet("/GetAllByCustomerId/{customerId}", ([FromServices] SchedulingDbContext dbContext, [FromServices] IMapper<T, TDto> mapper, long customerId) => GetAllByCustomerId(dbContext, mapper, customerId));
			group.MapGet("/GetAllByEmployeeId/{employeeId}", ([FromServices] SchedulingDbContext dbContext, [FromServices] IMapper<T, TDto> mapper, long employeeId) => GetAllByEmployeeId(dbContext, mapper, employeeId));

			return endpoints;
		}
		protected async Task<IResult> GetAllByCustomerId(SchedulingDbContext dbContext, IMapper<T, TDto> mapper, long customerId)
		{
			try
			{
				var allEntities = await new GetByPredicateQuery<T, TDto, SchedulingDbContext>(dbContext, mapper).Handle(s => s.CustomerId == customerId);

				if (allEntities == null || !allEntities.Any())
				{
					return Results.NotFound($"No records found.");
				}

				return Results.Ok(allEntities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving records.");
			}
		}
		protected async Task<IResult> GetAllByEmployeeId(SchedulingDbContext dbContext, IMapper<T, TDto> mapper, long employeeId)
		{
			try
			{
				var allEntities = await new GetByPredicateQuery<T, TDto, SchedulingDbContext>(dbContext, mapper).Handle(s => s.EmployeeId == employeeId);

				if (allEntities == null || !allEntities.Any())
				{
					return Results.NotFound($"No records found.");
				}

				return Results.Ok(allEntities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving records.");
			}
		}
	}
}