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
	public class ProductEndpoints : BaseEndpoints<Product, ProductDto, CRMDbContext>, IEndpoints { }
	public class ProductTypeEndpoints : BaseEndpoints<ProductType, ProductTypeDto, CRMDbContext>, IEndpoints { }
	public class DocumentEndpoints : BaseEndpoints<Document, DocumentDto, CRMDbContext>, IEndpoints
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
			) => await UploadDocument(dbContext, mapper, blobServiceClient, documentDto, file));

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
					await blobClient.UploadAsync(stream, overwrite: true);
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
	public class ManagerEndpoints : BaseEndpoints<Manager, Shared.DTOs.CRM.ManagerDto, CRMDbContext>, IEndpoints { }
}