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
using Server.Modules.CommonModule.Interfaces;
using ComposedHealthBase.Shared.DTOs;
using Microsoft.AspNetCore.Antiforgery;
using Azure.Storage.Blobs.Models;

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
	public class ContractEndpoints : BaseEndpoints<Contract, ContractDto, CRMDbContext>, IEndpoints
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints); // Call the base class method first
			var group = endpoints.MapGroup($"/api/contract");

			group.MapGet("/GetByCustomerId/{customerId}", async ([FromServices] CRMDbContext dbContext, [FromServices] IMapper<Contract, ContractDto> mapper, long customerId) =>
			{
				try
				{
					var query = new GetContractsByCustomerIdQuery(dbContext, mapper, customerId);
					var result = await query.Handle();
					if (result == null || !result.Any())
					{
						return Results.NotFound($"No contracts found for customer ID {customerId}.");
					}
					return Results.Ok(result);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred while retrieving contracts by customer ID: {ex.Message}");
					return Results.Problem($"An error occurred while retrieving contracts for customer ID {customerId}.");
				}
			});

			return endpoints;
		}
	}
	public class DocumentEndpoints : CommonCRMEndpoints<Document, DocumentDto, CRMDbContext>, IEndpoints
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/document");

			// New upload endpoint
			group.MapPost("/upload", async (
				[FromServices] CRMDbContext dbContext,
				[FromServices] IMapper<Document, DocumentDto> mapper,
				[FromServices] Azure.Storage.Blobs.BlobServiceClient blobServiceClient,
				[FromForm] DocumentDto documentDto,
				[FromForm] IFormFile file
			) => await UploadDocument(dbContext, mapper, blobServiceClient, documentDto, file)).DisableAntiforgery();

			return endpoints;
		}

		// New method for uploading document and file to Azure Blob Storage
		protected async Task<IResult> UploadDocument(
			CRMDbContext dbContext,
			IMapper<Document, DocumentDto> mapper,
			Azure.Storage.Blobs.BlobServiceClient blobServiceClient,
			DocumentDto documentDto,
			IFormFile file)
		{
			if (file == null || file.Length == 0)
				return Results.BadRequest("File is required.");

			try
			{
				var containerClient = blobServiceClient.GetBlobContainerClient("documents");
				await containerClient.CreateIfNotExistsAsync();

				var blobName = $"{Guid.NewGuid()}_{file.FileName}";
				var blobClient = containerClient.GetBlobClient(blobName);

				using (var stream = file.OpenReadStream())
				{
					await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType },  Overwrite = true });
				}

				// Optionally, update the DocumentDto with the blob URL
				documentDto.FilePath = blobClient.Uri.ToString();

				// Map and save the Document entity as needed
				var entity = mapper.Map(documentDto);
				dbContext.Documents.Add(entity);
				await dbContext.SaveChangesAsync();

				return Results.Ok(new { url = blobClient.Uri.ToString() });
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem("An error occurred while uploading the document.");
			}
		}
	}
	public class ManagerEndpoints : CommonCRMEndpoints<Manager, Shared.DTOs.CRM.ManagerDto, CRMDbContext>, IEndpoints { }
	//public class DocumentEndpoints : CommonCRMEndpoints<Document, DocumentDto, CRMDbContext>, IEndpoints { } //This line is now handled by the specific DocumentEndpoints class
	public abstract class CommonCRMEndpoints<T, TDto, CRMDbContext> : BaseEndpoints<T, TDto, CRMDbContext>
		where T : BaseEntity<T>, IFilterByEmployee, IFilterByCustomer
		where TDto : IDto
		where CRMDbContext : IDbContext<CRMDbContext>
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);

			var entityName = typeof(T).Name.ToLower();

			var group = endpoints.MapGroup($"/api/{entityName}");

			group.MapGet("/GetAllByCustomerId/{customerId}", ([FromServices] CRMDbContext dbContext, [FromServices] IMapper<T, TDto> mapper, long customerId) => GetAllByCustomerId(dbContext, mapper, customerId));
			group.MapGet("/GetAllByEmployeeId/{employeeId}", ([FromServices] CRMDbContext dbContext, [FromServices] IMapper<T, TDto> mapper, long employeeId) => GetAllByEmployeeId(dbContext, mapper, employeeId));

			return endpoints;
		}
		protected async Task<IResult> GetAllByCustomerId(CRMDbContext dbContext, IMapper<T, TDto> mapper, long customerId)
		{
			try
			{
				var allEntities = await new GetByPredicateQuery<T, TDto, CRMDbContext>(dbContext, mapper).Handle(s => s.CustomerId == customerId);

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
		protected async Task<IResult> GetAllByEmployeeId(CRMDbContext dbContext, IMapper<T, TDto> mapper, long employeeId)
		{
			try
			{
				var allEntities = await new GetByPredicateQuery<T, TDto, CRMDbContext>(dbContext, mapper).Handle(s => s.EmployeeId == employeeId);

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