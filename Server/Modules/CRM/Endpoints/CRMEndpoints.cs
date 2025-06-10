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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Server.Modules.CRM.Endpoints
{
	public class EmployeeEndpoints : BaseEndpoints<Employee, EmployeeDto, CRMDbContext>, IEndpoints
	{
		public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/employee");

			group.MapGet("/search", (
				[FromServices] ISearchEmployeesQuery searchEmployeesQuery,
				ClaimsPrincipal user,
				[FromQuery] string term
			) => SearchEmployees(searchEmployeesQuery, user, term));

			return endpoints;
		}

		protected async Task<IResult> SearchEmployees(
			ISearchEmployeesQuery searchEmployeesQuery,
			ClaimsPrincipal user,
			string term)
		{
			try
			{
				var results = await searchEmployeesQuery.Handle(user, term);
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

			group.MapGet("/search", (
				[FromServices] ISearchCustomersQuery searchCustomersQuery,
				ClaimsPrincipal user,
				[FromQuery] string term
			) => SearchCustomers(searchCustomersQuery, user, term));

			return endpoints;
		}

		protected async Task<IResult> SearchCustomers(
			ISearchCustomersQuery searchCustomersQuery,
			ClaimsPrincipal user,
			string term)
		{
			try
			{
				var results = await searchCustomersQuery.Handle(user, term);
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
	public class CustomerDocumentEndpoints : DocumentEndpoints<CustomerDocument, CustomerDocumentDto, CRMDbContext>, IEndpoints { }
	public class EmployeeDocumentEndpoints : DocumentEndpoints<EmployeeDocument, EmployeeDocumentDto, CRMDbContext>, IEndpoints { }
	public class ManagerEndpoints : BaseEndpoints<Manager, ManagerDto, CRMDbContext>, IEndpoints { }
}