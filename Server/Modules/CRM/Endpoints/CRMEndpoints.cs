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

namespace Server.Modules.CRM.Endpoints
{
	public class EmployeeEndpoints : BaseEndpoints<Employee, EmployeeDto, CRMDbContext>, IEndpoints
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/employee");

			group.MapGet("/getbycustomer/{id}", ([FromServices] CRMDbContext dbContext, [FromServices] IMapper<Employee, EmployeeDto> mapper, long id) => GetByCustomer(dbContext, mapper, id));

			return endpoints;
		}

		protected async Task<IResult> GetByCustomer(CRMDbContext dbContext, IMapper<Employee, EmployeeDto> mapper, long id)
		{
			try
			{
				var allEntities = await new GetEmployeesByCustomerQuery(dbContext, mapper, id).Handle();

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
	}
	public class CustomerEndpoints : BaseEndpoints<Customer, CustomerDto, CRMDbContext>, IEndpoints { }
	public class ContractEndpoints : BaseEndpoints<Contract, ContractDto, CRMDbContext>, IEndpoints { }
	public class ProductEndpoints : BaseEndpoints<Product, ProductDto, CRMDbContext>, IEndpoints { }
	public class ProductTypeEndpoints : BaseEndpoints<ProductType, ProductTypeDto, CRMDbContext>, IEndpoints { }
}