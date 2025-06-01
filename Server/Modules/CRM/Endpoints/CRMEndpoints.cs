using ComposedHealthBase.Server.Endpoints;
using Shared.DTOs.CRM;
using Server.Modules.CRM.Entities;
using Server.Modules.CRM.Infrastructure.Database;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using ComposedHealthBase.Server.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Server.Modules.CRM.Infrastructure.Queries;
using ComposedHealthBase.Server.Queries;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;

using ComposedHealthBase.Shared.DTOs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;

namespace Server.Modules.CRM.Endpoints
{
	public class EmployeeEndpoints : BaseEndpoints<Employee, EmployeeDto, CRMDbContext>, IEndpoints
	{
		public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/employee");

			group.MapGet("/search", ([FromServices] CRMDbContext dbContext, [FromServices] IMapper<Employee, EmployeeDto> mapper, [FromQuery] string term) => SearchEmployees(dbContext, mapper, term));

			return endpoints;
		}

		// New method for searching employees by free text
		protected async Task<IResult> SearchEmployees(CRMDbContext dbContext, IMapper<Employee, EmployeeDto> mapper, string term)
		{
			try
			{
				var results = await new SearchEmployeesQuery(dbContext, mapper, term).Handle();
				return Results.Ok(results);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem("An error occurred while searching for employees.");
			}
		}
	}
	public class CustomerEndpoints : BaseEndpoints<Customer, CustomerDto, CRMDbContext>, IEndpoints
	{
		public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/customer");

			group.MapGet("/search", ([FromServices] CRMDbContext dbContext, [FromServices] IMapper<Customer, CustomerDto> mapper, [FromQuery] string term) => SearchCustomers(dbContext, mapper, term));

			return endpoints;
		}

		protected async Task<IResult> SearchCustomers(CRMDbContext dbContext, IMapper<Customer, CustomerDto> mapper, string term)
		{
			try
			{
				var results = await new SearchCustomersQuery(dbContext, mapper, term).Handle();
				return Results.Ok(results);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem("An error occurred while searching for customers.");
			}
		}
	}
	public class ContractEndpoints : BaseEndpoints<Contract, ContractDto, CRMDbContext>, IEndpoints { }
	public class CustomerDocumentEndpoints : DocumentEndpoints<CustomerDocument, DocumentDto, CRMDbContext>, IEndpoints { }
	public class EmployeeDocumentEndpoints : DocumentEndpoints<EmployeeDocument, DocumentDto, CRMDbContext>, IEndpoints { }
	public class ManagerEndpoints : BaseEndpoints<Manager, ManagerDto, CRMDbContext>, IEndpoints { }
}