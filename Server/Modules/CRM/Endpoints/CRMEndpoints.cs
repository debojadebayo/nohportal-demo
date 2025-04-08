using ComposedHealthBase.Server.BaseModule.Endpoints;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shared.DTOs.CRM;
using MediatR;
using Server.Modules.CRM.Infrastructure.Queries;

namespace Server.Modules.CRM.Endpoints
{
	public class CRMEndpoints : BaseEndpoints
	{
		public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			base.MapEndpoints(endpoints);

			group.MapGet("/Customers", (IMediator mediator) => GetAllCustomers(mediator));

			return endpoints;
		}

		private async Task<IResult> GetAllCustomers(IMediator mediator)
		{
			try
			{
				var allCustomers = await mediator.Send(new GetAllCustomersQuery());

				if (allCustomers == null || !allCustomers.Any())
				{
					return Results.NotFound("No customers found.");
				}

				return Results.Ok(allCustomers);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem("An error occurred while retrieving customer details.");
			}
			return Results.Ok();
		}
		private async Task<IResult> GetCustomerDetails(int customerId)
		{
			return Results.Ok();
		}

		private async Task<IResult> SearchCustomers(string? name, DateTime? contractStart, DateTime? contractEnd)
		{
			return Results.Ok();
		}

		private async Task<IResult> LinkProductsToCustomer(int customerId, ProductAssignmentDto productAssignmentDto)
		{
			return Results.Ok();
		}

		private async Task<IResult> GetCustomerMI(int customerId, DateTime? startDate, DateTime? endDate)
		{
			return Results.Ok();
		}

		private async Task<IResult> TrackCaseReferrals(int customerId)
		{
			return Results.Ok();
		}

		private async Task<IResult> UpdateCustomerContract(int customerId, ContractUpdateDto contractUpdateDto)
		{
			return Results.NoContent();
		}

		private async Task<IResult> DeactivateCustomerAccount(int customerId)
		{
			return Results.NoContent();
		}
	}
}