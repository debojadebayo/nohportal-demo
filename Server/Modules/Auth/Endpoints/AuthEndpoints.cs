using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using ComposedHealthBase.Server.Endpoints;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Shared.DTOs;
using Shared.DTOs.Auth;
using Server.Modules.Auth.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using Server.Modules.Auth.Infrastructure.Database;
using Server.Modules.Auth.Infrastructure.Commands;
using Server.Modules.Auth.Infrastructure.Queries;

namespace Server.Modules.Auth.Endpoints
{
	public class RoleEndpoints : IEndpoints
	{
		public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var group = endpoints.MapGroup($"/api/auth/roles");

			group.MapGet("/GetAll", async (
				[FromServices] IDbContext<AuthDbContext> dbContext,
				[FromServices] IMapper<Role, RoleDto> mapper
			) =>
			{
				try
				{
					var roles = await dbContext.Set<Role>().Include(r => r.Permissions).ToListAsync();
					var dtos = roles.Select(mapper.Map).ToList();
					return Results.Ok(dtos);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred: {ex.Message}");
					return Results.Problem("An error occurred while retrieving Role entities.");
				}
			});

			group.MapGet("/GetById/{id}", async (
				[FromServices] IDbContext<AuthDbContext> dbContext,
				[FromServices] IMapper<Role, RoleDto> mapper,
				Guid id
			) =>
			{
				try
				{
					var role = await dbContext.Set<Role>().Include(r => r.Permissions).FirstOrDefaultAsync(r => r.Id == id);
					if (role == null)
						return Results.NotFound();
					var dto = mapper.Map(role);
					return Results.Ok(dto);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred: {ex.Message}");
					return Results.Problem("An error occurred while retrieving the Role details.");
				}
			});

			group.MapPost("/Create", async (
				[FromServices] IDbContext<AuthDbContext> dbContext,
				[FromServices] IMapper<Role, RoleDto> mapper,
				[FromBody] RoleDto role
			) =>
			{
				try
				{
					var entity = mapper.Map(role);
					dbContext.Set<Role>().Add(entity);
					await dbContext.SaveChangesAsync();
					var dto = mapper.Map(entity);
					return Results.Ok(dto);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred: {ex.Message}");
					return Results.Problem("An error occurred while creating the Role.");
				}
			});

			group.MapPut("/Update", async (
				[FromServices] IDbContext<AuthDbContext> dbContext,
				[FromServices] IMapper<Role, RoleDto> mapper,
				[FromBody] RoleDto roleDto // Renamed for clarity
			) =>
			{
				try
				{
					var entityToUpdate = await dbContext.Set<Role>()
						.Include(r => r.Permissions) // Load existing permissions
						.FirstOrDefaultAsync(r => r.Id == roleDto.Id);

					if (entityToUpdate == null)
					{
						return Results.NotFound($"Role with ID {roleDto.Id} not found.");
					}

					// Update scalar properties
					entityToUpdate.Name = roleDto.Name;
					// Add any other scalar properties from roleDto to entityToUpdate here

					// Manage the Permissions collection
					// Get the IDs of the permissions from the DTO
					var desiredPermissionIds = roleDto.Permissions.Select(p => p.Id).ToHashSet();

					// Fetch the actual Permission entities from the database
					var desiredPermissions = await dbContext.Set<Permission>()
						.Where(p => desiredPermissionIds.Contains(p.Id))
						.ToListAsync();

					// Verify that all desired permissions were found in the database
					if (desiredPermissions.Count != desiredPermissionIds.Count)
					{
						var foundDbPermissionIds = desiredPermissions.Select(p => p.Id).ToHashSet();
						var missingIds = desiredPermissionIds.Except(foundDbPermissionIds);
						Console.Error.WriteLine($"Update Role: Could not find permissions with IDs: {string.Join(", ", missingIds)}");
						return Results.Problem($"One or more permissions specified for the role could not be found in the database.");
					}
					
					// Update the role's permissions collection
					// A common way is to clear existing and add desired ones.
					// EF Core will figure out the changes to the join table.
					entityToUpdate.Permissions.Clear();
					foreach (var permissionToAdd in desiredPermissions)
					{
						entityToUpdate.Permissions.Add(permissionToAdd);
					}

					// dbContext.Set<Role>().Update(entityToUpdate); // Not strictly necessary as entityToUpdate is tracked
					await dbContext.SaveChangesAsync();

					var updatedDto = mapper.Map(entityToUpdate); // Map the updated entity back to DTO
					return Results.Ok(updatedDto);
				}
				catch (DbUpdateException ex) // Catch specific DbUpdateException
				{
					Console.Error.WriteLine($"A database update error occurred: {ex.Message}");
					// Log inner exception if present
					if (ex.InnerException != null)
					{
						Console.Error.WriteLine($"Inner exception: {ex.InnerException.Message}");
					}
					return Results.Problem("An error occurred while updating the Role due to a database issue. Check server logs for details.");
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An unexpected error occurred: {ex.Message}");
					return Results.Problem("An unexpected error occurred while updating the Role.");
				}
			});

			group.MapPost("/Delete/{id}", async (
				[FromServices] IDbContext<AuthDbContext> dbContext,
				[FromServices] IMapper<Role, RoleDto> mapper,
				Guid id
			) =>
			{
				try
				{
					var role = await dbContext.Set<Role>().FindAsync(id);
					if (role == null)
						return Results.NotFound();
					dbContext.Set<Role>().Remove(role);
					await dbContext.SaveChangesAsync();
					return Results.Ok();
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred: {ex.Message}");
					return Results.Problem("An error occurred while deleting the Role.");
				}
			});

			return endpoints;
		}
	}
	public class PermissionEndpoints : IEndpoints
	{
		public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var group = endpoints.MapGroup($"/api/auth/permissions");

			group.MapGet("/GetAll", async (
				[FromServices] IDbContext<AuthDbContext> dbContext,
				[FromServices] IMapper<Permission, PermissionDto> mapper
			) =>
			{
				try
				{
					var Permissions = await dbContext.Set<Permission>().ToListAsync();
					var dtos = Permissions.Select(mapper.Map).ToList();
					return Results.Ok(dtos);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred: {ex.Message}");
					return Results.Problem("An error occurred while retrieving Permission entities.");
				}
			});

			group.MapGet("/GetById/{id}", async (
				[FromServices] IDbContext<AuthDbContext> dbContext,
				[FromServices] IMapper<Permission, PermissionDto> mapper,
				Guid id
			) =>
			{
				try
				{
					var Permission = await dbContext.Set<Permission>().FindAsync(id);
					if (Permission == null)
						return Results.NotFound();
					var dto = mapper.Map(Permission);
					return Results.Ok(dto);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred: {ex.Message}");
					return Results.Problem("An error occurred while retrieving the Permission details.");
				}
			});

			group.MapPost("/Create", async (
				[FromServices] IDbContext<AuthDbContext> dbContext,
				[FromServices] IMapper<Permission, PermissionDto> mapper,
				[FromBody] PermissionDto Permission
			) =>
			{
				try
				{
					var entity = mapper.Map(Permission);
					dbContext.Set<Permission>().Add(entity);
					await dbContext.SaveChangesAsync();
					var dto = mapper.Map(entity);
					return Results.Ok(dto);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred: {ex.Message}");
					return Results.Problem("An error occurred while creating the Permission.");
				}
			});

			group.MapPut("/Update", async (
				[FromServices] IDbContext<AuthDbContext> dbContext,
				[FromServices] IMapper<Permission, PermissionDto> mapper,
				[FromBody] PermissionDto Permission
			) =>
			{
				try
				{
					var entity = mapper.Map(Permission);
					dbContext.Set<Permission>().Update(entity);
					await dbContext.SaveChangesAsync();
					var dto = mapper.Map(entity);
					return Results.Ok(dto);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred: {ex.Message}");
					return Results.Problem("An error occurred while updating the Permission.");
				}
			});

			group.MapPost("/Delete/{id}", async (
				[FromServices] IDbContext<AuthDbContext> dbContext,
				[FromServices] IMapper<Permission, PermissionDto> mapper,
				Guid id
			) =>
			{
				try
				{
					var Permission = await dbContext.Set<Permission>().FindAsync(id);
					if (Permission == null)
						return Results.NotFound();
					dbContext.Set<Permission>().Remove(Permission);
					await dbContext.SaveChangesAsync();
					return Results.Ok();
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred: {ex.Message}");
					return Results.Problem("An error occurred while deleting the Permission.");
				}
			});

			return endpoints;
		}
	}

	public class LocalStorageKeyEndpoints : IEndpoints
	{
		public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var group = endpoints.MapGroup($"/api/auth/localstoragekeys");

			group.MapPost("/generate", async (
				[FromServices] IDbContext<AuthDbContext> dbContext,
				[FromServices] IMapper<LocalStorageKey, LocalStorageKeyDto> mapper,
				[FromServices] IGenerateRSAKeyCommand generateKeyCommand,
				[FromServices] IGetLocalStorageKeyQuery getKeyQuery,
				ClaimsPrincipal user,
				[FromBody] GenerateKeyRequestDto request
			) =>
			{
				try
				{
					// Check if a key already exists for this object
					var existingKey = await getKeyQuery.Handle(request.ObjectTypeName, request.ObjectGuid, user);
					if (existingKey != null)
					{
						return Results.Conflict("A key already exists for this object. Delete the existing key first if you want to generate a new one.");
					}

					// Generate new RSA keypair
					var newKey = await generateKeyCommand.Handle(request.ObjectTypeName, request.ObjectGuid, user);
					var dto = mapper.Map(newKey);
					
					return Results.Ok(new 
					{ 
						message = "RSA keypair generated successfully",
						keyId = dto.Id,
						publicKey = dto.PublicKey,
						keyGeneratedDate = dto.KeyGeneratedDate
					});
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred: {ex.Message}");
					return Results.Problem("An error occurred while generating the RSA keypair.");
				}
			});

			group.MapGet("/GetByObject", async (
				[FromServices] IGetLocalStorageKeyQuery getKeyQuery,
				ClaimsPrincipal user,
				[FromQuery] string objectTypeName,
				[FromQuery] Guid objectGuid
			) =>
			{
				try
				{
					var key = await getKeyQuery.Handle(objectTypeName, objectGuid, user);
					if (key == null)
						return Results.NotFound("No key found for the specified object.");
					
					return Results.Ok(new 
					{ 
						keyId = key.Id,
						publicKey = key.PublicKey,
						keyGeneratedDate = key.KeyGeneratedDate,
						keyExpiryDate = key.KeyExpiryDate
					});
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"An error occurred: {ex.Message}");
					return Results.Problem("An error occurred while retrieving the key.");
				}
			});

			return endpoints;
		}
	}
}