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
using System.Threading.Tasks;
using System;
using System.Security.Claims;

namespace ComposedHealthBase.Server.Endpoints
{
	public abstract class DocumentEndpoints<T, TDto, TContext> : BaseEndpoints<T, TDto, TContext>
	where T : class, IAuditEntity, IDocument
	where TDto : IDto, IDocumentDto
	where TContext : IDbContext<TContext>
	{
		private static string EndpointType => typeof(T).Name.ToLowerInvariant();
		public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/{EndpointType}");
			// New upload endpoint
			group.MapPost("/upload", async (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] BlobServiceClient blobServiceClient,
				[FromServices] Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService,
				[FromForm] TDto documentDto,
				[FromForm] IFormFile file,
				ClaimsPrincipal user
			) => await UploadDocument(dbContext, mapper, blobServiceClient, authorizationService, documentDto, file, user)).DisableAntiforgery();

			group.MapGet("/getsaslink/{documentId}", async (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] BlobServiceClient blobServiceClient,
				[FromServices] Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService,
				[FromServices] GetByPredicateQuery<T, TDto, TContext> getByPredicateQuery,
				Guid documentGuid,
				ClaimsPrincipal user
			) => await GetDocumentSasLink(getByPredicateQuery, blobServiceClient, authorizationService, documentGuid, user));

			group.MapGet("/getcontent/{documentId}", async (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] BlobServiceClient blobServiceClient,
				[FromServices] Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService,
				[FromServices] GetByIdQuery<T, TDto, TContext> getByIdQuery,
				Guid documentId,
				ClaimsPrincipal user
			) => await GetDocumentContent(getByIdQuery, blobServiceClient, authorizationService, documentId, user));

			return endpoints;
		}

		// New method for uploading document and file to Azure Blob Storage
		protected async Task<IResult> UploadDocument(
			IDbContext<TContext> dbContext,
			IMapper<T, TDto> mapper,
			BlobServiceClient blobServiceClient,
			Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService,
			TDto documentDto,
			IFormFile file,
			ClaimsPrincipal user)
		{
			if (file == null || file.Length == 0)
				return Results.BadRequest("File is required.");

			try
			{
				var containerName = $"documents";

				var containerClient = blobServiceClient.GetBlobContainerClient(containerName);

				containerClient.CreateIfNotExists();

				var blobName = $"{file.FileName}_{documentDto.DocumentGuid:N}";
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
				await dbContext.SaveChangesWithAuditAsync(user);

				return Results.Ok(new { url = blobClient.Uri.ToString() });
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem("An error occurred while uploading the document.");
			}
		}

		protected async Task<IResult> GetDocumentSasLink(
			GetByPredicateQuery<T, TDto, TContext> getByPredicateQuery,
			BlobServiceClient blobServiceClient,
			Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService,
			Guid documentGuid,
			ClaimsPrincipal user)
		{
			try
			{
				// Retrieve the document entity from the database using the injected query handler
				var document = (await getByPredicateQuery.Handle(d => d.DocumentGuid == documentGuid, user)).SingleOrDefault();
				if (document == null)
					return Results.NotFound("Document not found.");
				//TODO: Use authorizationService to check access if needed

				var containerClient = blobServiceClient.GetBlobContainerClient(document.BlobContainerName);
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
			GetByIdQuery<T, TDto, TContext> getByIdQuery,
			BlobServiceClient blobServiceClient,
			Microsoft.AspNetCore.Authorization.IAuthorizationService authorizationService,
			Guid documentId,
			ClaimsPrincipal user)
		{
			try
			{
				// Retrieve the document entity from the database using the injected query handler
				var document = await getByIdQuery.Handle(documentId, user);
				if (document == null)
					return Results.NotFound("Document not found.");
				//TODO: Use authorizationService to check access if needed

				var containerClient = blobServiceClient.GetBlobContainerClient(document.BlobContainerName);
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