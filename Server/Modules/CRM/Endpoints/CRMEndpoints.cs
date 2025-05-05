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

namespace Server.Modules.CRM.Endpoints
{
	public class EmployeeEndpoints : BaseEndpoints<Employee, EmployeeDto, CRMDbContext>, IEndpoints
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/employee");

			group.MapGet("/getbycustomer/{id}", ([FromServices] CRMDbContext dbContext, [FromServices] IMapper<Employee, EmployeeDto> mapper, long id) => GetByCustomer(dbContext, mapper, id));

			// Add search endpoint
			group.MapGet("/search", ([FromServices] CRMDbContext dbContext, [FromServices] IMapper<Employee, EmployeeDto> mapper, [FromQuery] string term) => SearchEmployees(dbContext, mapper, term));

			return endpoints;
		}

		protected async Task<IResult> GetByCustomer(CRMDbContext dbContext, IMapper<Employee, EmployeeDto> mapper, long id)
		{
			try
			{
				var allEntities = await new GetByPredicateQuery<Employee, EmployeeDto, CRMDbContext>(dbContext, mapper).Handle(e => e.CustomerId == id);

				if (allEntities == null || !allEntities.Any())
				{
					return Results.NotFound($"No employee entities found.");
				}

				return Results.Ok(allEntities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving employee entities.");
			}
		}

		// New method for searching employees by free text
		protected async Task<IResult> SearchEmployees(CRMDbContext dbContext, IMapper<Employee, EmployeeDto> mapper, string term)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(term))
					return Results.BadRequest("Search term is required.");

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
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
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
				if (string.IsNullOrWhiteSpace(term))
					return Results.BadRequest("Search term is required.");

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
	public class ProductEndpoints : BaseEndpoints<Product, ProductDto, CRMDbContext>, IEndpoints { }
	public class ProductTypeEndpoints : BaseEndpoints<ProductType, ProductTypeDto, CRMDbContext>, IEndpoints { }
	public class DocumentEndpoints : BaseEndpoints<Document, DocumentDto, CRMDbContext>, IEndpoints { }
}