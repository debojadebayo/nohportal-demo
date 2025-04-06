using ComposedHealthBase.Server.BaseModule.Endpoints;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Shared.DTOs.CRM;
using MediatR;
using Server.Modules.CRM.Infrastructure.Queries;

namespace Server.Modules.CRM.Endpoints
{
	public class CRMEndpoints : IEndpoints
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var group = endpoints.MapGroup("/api/CRM");

			// Enhanced CRM endpoints
			group.MapGet("/Customers", (IMediator mediator) => GetAllCustomerDetails(mediator));
			group.MapGet("/Customers/{customerId}", GetCustomerDetails);
			group.MapGet("/Search", SearchCustomers);
			group.MapPost("/Products/{customerId}", LinkProductsToCustomer);
			group.MapGet("/Reports/MI/{customerId}", GetCustomerMI);
			group.MapGet("/Cases/{customerId}", TrackCaseReferrals);
			group.MapPut("/Contracts/{customerId}", UpdateCustomerContract);
			group.MapPost("/Deactivate/{customerId}", DeactivateCustomerAccount);

			return endpoints;
		}

		private async Task<IResult> GetAllCustomerDetails(IMediator mediator)
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