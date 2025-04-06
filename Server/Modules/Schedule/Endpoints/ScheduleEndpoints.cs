using ComposedHealthBase.Server.BaseModule.Endpoints;
using Shared.DTOs.Schedule;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MediatR;
using Server.Modules.Schedule.Infrastructure.Queries;
using Server.Modules.Schedule.Entities;
using Server.Modules.Schedule.Infrastructure.Commands;

namespace Server.Modules.Schedule.Endpoints
{
	public class ScheduleEndpoints : IEndpoints
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var referrals = endpoints.MapGroup("/api/referrals");
			referrals.MapGet("/", (IMediator mediator) => GetAllReferrals(mediator));
			referrals.MapGet("/{id}", (long id, IMediator mediator) => GetReferralById(id, mediator));
			referrals.MapPost("/", (ReferralDto referral, IMediator mediator) => CreateReferral(referral, mediator));
			referrals.MapPut("/{id}", (ReferralDto referral, IMediator mediator) => UpdateReferral(referral, mediator));
			referrals.MapDelete("/{id}", (long id, IMediator mediator) => DeleteReferral(id, mediator));

			var schedules = endpoints.MapGroup("/api/schedules");
			schedules.MapGet("/", GetAllSchedules);
			schedules.MapGet("/{id}", GetScheduleById);
			schedules.MapPost("/", CreateSchedule);
			schedules.MapPut("/{id}", UpdateSchedule);
			schedules.MapDelete("/{id}", DeleteSchedule);
			schedules.MapGet("/Availability/{clinicianId}", GetClinicianAvailability);
			schedules.MapGet("/Search", SearchSchedules);
			schedules.MapGet("/ExportInvoices", ExportInvoiceData);
			schedules.MapGet("/Status/{schedulesId}", GetScheduleStatus);
			schedules.MapGet("/Reports/MI", GenerateMIReport);
			schedules.MapPost("/LockCalendar/{clinicianId}", LockClinicianCalendar);
			schedules.MapGet("/Summary", GetScheduleSummary);

			return endpoints;
		}

		private async Task<IResult> GetAllReferrals(IMediator mediator)
		{
			try
			{
				var allReferrals = await mediator.Send(new GetAllReferralsQuery());

				if (allReferrals == null || !allReferrals.Any())
				{
					return Results.NotFound("No referrals found.");
				}

				return Results.Ok(allReferrals);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem("An error occurred while retrieving referral details.");
			}
			return Results.Ok();
		}

		private async Task<IResult> GetReferralById(long id, IMediator mediator)
		{
			return Results.Ok();
		}

		private async Task<IResult> CreateReferral(ReferralDto scheduleDto, IMediator mediator)
		{
			try
			{
				var id = await mediator.Send(new CreateReferralCommand { Referral = scheduleDto });

				return Results.Ok(id);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem("An error occurred while adding referral details.");
			}
			return Results.Ok();
		}

		private async Task<IResult> UpdateReferral(ReferralDto scheduleDto, IMediator mediator)
		{
			return Results.NoContent();
		}
		private async Task<IResult> DeleteReferral(long id, IMediator mediator)
		{
			return Results.NoContent();
		}

		private async Task<IResult> GetAllSchedules()
		{
			return Results.NoContent();
		}

		private async Task<IResult> GetScheduleById(int id)
		{
			// Implementation
			return Results.Ok();
		}

		private async Task<IResult> CreateSchedule(ScheduleDto scheduleDto)
		{
			// Implementation
			return Results.Created("/api/Schedules", scheduleDto);
		}

		private async Task<IResult> UpdateSchedule(int id, ScheduleDto scheduleDto)
		{
			// Implementation
			return Results.NoContent();
		}

		private async Task<IResult> DeleteSchedule(int id)
		{
			// Implementation
			return Results.NoContent();
		}

		// Enhanced functionalities
		private async Task<IResult> GetClinicianAvailability(int clinicianId)
		{
			// Fetch and return available slots
			return Results.Ok();
		}

		private async Task<IResult> SearchSchedules(DateTime date, int? clinicianId, string? venue, string? productType)
		{
			// Return filtered schedules
			return Results.Ok();
		}

		private async Task<IResult> ExportInvoiceData()
		{
			// Generate and return Xero-compatible spreadsheet
			return Results.Ok();
		}

		private async Task<IResult> GetScheduleStatus(int scheduleId)
		{
			// Fetch and return schedule status
			return Results.Ok();
		}

		private async Task<IResult> GenerateMIReport(DateTime startDate, DateTime endDate)
		{
			// Return MI summary for specified date range
			return Results.Ok();
		}

		private async Task<IResult> LockClinicianCalendar(int clinicianId, bool lockClinicianCalendar)
		{
			// Update calendar permissions
			return Results.NoContent();
		}

		private async Task<IResult> GetScheduleSummary(int? clinicianId, string? venue, DateTime? dateRangeStart, DateTime? dateRangeEnd)
		{
			// Generate and return summary
			return Results.Ok();
		}
	}
}