using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Commands;
using ComposedHealthBase.Server.Queries;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

namespace ComposedHealthBase.Server.Endpoints
{
	public abstract class DocumentEndpoints<T, TDto, TContext>
	where T : BaseEntity<T>
	where TDto : IDto, IDocumentDto
	where TContext : IDbContext<TContext>
	{
		public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var group = endpoints.MapGroup($"/api/document");

			// New upload endpoint
			group.MapPost("/upload", async (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] BlobServiceClient blobServiceClient,
				[FromForm] TDto documentDto,
				[FromForm] IFormFile file
			) => await UploadDocument(dbContext, mapper, blobServiceClient, documentDto, file)).DisableAntiforgery();

			group.MapGet("/getsaslink/{documentId}", async (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] BlobServiceClient blobServiceClient,
				long documentId
			) => await GetDocumentSasLink(dbContext, mapper, blobServiceClient, documentId));

			group.MapGet("/getcontent/{documentId}", async (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] BlobServiceClient blobServiceClient,
				long documentId) =>
					await GetDocumentContent(dbContext, mapper, blobServiceClient, documentId));

			return endpoints;
		}

		// New method for uploading document and file to Azure Blob Storage
		protected async Task<IResult> UploadDocument(
			IDbContext<TContext> dbContext,
			IMapper<T, TDto> mapper,
			BlobServiceClient blobServiceClient,
			TDto documentDto,
			IFormFile file)
		{
			if (file == null || file.Length == 0)
				return Results.BadRequest("File is required.");

			try
			{
				var containerName = $"documents";

				var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

				containerClient.CreateIfNotExists();

				var blobName = $"{file.FileName}_{Guid.NewGuid()}";
				var blobClient = containerClient.GetBlobClient(blobName);

				using (var stream = file.OpenReadStream())
				{
					await blobClient.UploadAsync(stream, new BlobUploadOptions { HttpHeaders = new BlobHttpHeaders { ContentType = file.ContentType } });
				}

				// Optionally, update the TDto with the blob URL
				documentDto.FilePath = blobClient.Uri.ToString();

				documentDto.BlobContainerName = containerName;
				documentDto.BlobName = blobName;

				// Map and save the Document entity as needed
				var entity = mapper.Map(documentDto);
				dbContext.Set<T>().Add(entity);
				await dbContext.SaveChangesAsync();

				return Results.Ok(new { url = blobClient.Uri.ToString() });
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem("An error occurred while uploading the document.");
			}
		}
		protected async Task<IResult> GetDocumentSasLink(
			IDbContext<TContext> dbContext,
			IMapper<T, TDto> mapper,
			BlobServiceClient blobServiceClient,
			long documentId)
		{
			try
			{
				// Retrieve the document entity from the database using the generic query handler
				var document = await new GetByIdQuery<T, TDto, TContext>(dbContext, mapper).Handle(documentId);
				if (document == null)
					return Results.NotFound("Document not found.");
				//TODO Check the current user's claims against ownerId on the entity

				var containerClient = blobServiceClient.GetBlobContainerClient(document.BlobContainerName);
				// Use the stored BlobName directly instead of extracting from FilePath
				var blobClient = containerClient.GetBlobClient(document.BlobName);
				var sasToken = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddHours(1));

				return Results.Ok(sasToken.ToString());
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem("An error occurred while generating document access link.");
			}
		}
		protected async Task<IResult> GetDocumentContent(
			IDbContext<TContext> dbContext,
			IMapper<T, TDto> mapper,
			BlobServiceClient blobServiceClient,
			long documentId)
		{
			try
			{
				// Retrieve the document entity from the database using the generic query handler
				var document = await new GetByIdQuery<T, TDto, TContext>(dbContext, mapper).Handle(documentId);
				if (document == null)
					return Results.NotFound("Document not found.");
				//TODO Check the current user's claims against ownerId on the entity

				var containerClient = blobServiceClient.GetBlobContainerClient(document.BlobContainerName);
				// Use the stored BlobName directly instead of extracting from FilePath
				var blobClient = containerClient.GetBlobClient(document.BlobName);

				var response = await blobClient.DownloadContentAsync();
				return Results.File(response.Value.Content.ToArray(),
								   response.Value.Details.ContentType,
								   document.Name);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem("An error occurred while retrieving the document.");
			}
		}
	}
}