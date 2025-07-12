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
using ComposedHealthBase.Server.Auth.Constants;

namespace Server.Modules.Auth.Endpoints
{
    public class RoleEndpoints : IEndpoints
    {
        public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup($"/api/auth/roles");

            // View permission for GET operations
            var viewPermission = PermissionHelper.GeneratePermission<Role>(PermissionOperations.View);
            // Create permission for POST operations
            var createPermission = PermissionHelper.GeneratePermission<Role>(PermissionOperations.Create);
            // Update permission for PUT operations
            var updatePermission = PermissionHelper.GeneratePermission<Role>(PermissionOperations.Update);
            // Delete permission for DELETE operations
            var deletePermission = PermissionHelper.GeneratePermission<Role>(PermissionOperations.Delete);

            group.MapGet("/GetAll", async (
                [FromServices] IGetAllRolesQuery getAllRolesQuery
            ) =>
            {
                try
                {
                    var roles = await getAllRolesQuery.Handle();
                    return Results.Ok(roles);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An error occurred: {ex.Message}");
                    return Results.Problem("An error occurred while retrieving Role entities.");
                }
            }).RequireAuthorization($"Permission:{viewPermission}");

            group.MapGet("/GetById/{id}", async (
                [FromServices] IGetRoleByIdQuery getRoleByIdQuery,
                Guid id
            ) =>
            {
                try
                {
                    var role = await getRoleByIdQuery.Handle(id);
                    if (role == null)
                        return Results.NotFound();
                    return Results.Ok(role);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An error occurred: {ex.Message}");
                    return Results.Problem("An error occurred while retrieving the Role details.");
                }
            }).RequireAuthorization($"Permission:{viewPermission}");

            group.MapPost("/Create", async (
                [FromServices] ICreateRoleCommand createRoleCommand,
                ClaimsPrincipal user,
                [FromBody] RoleDto role
            ) =>
            {
                try
                {
                    var dto = await createRoleCommand.Handle(role, user);
                    return Results.Ok(dto);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An error occurred: {ex.Message}");
                    return Results.Problem("An error occurred while creating the Role.");
                }
            }).RequireAuthorization($"Permission:{createPermission}");

            group.MapPut("/Update", async (
                [FromServices] IUpdateRoleCommand updateRoleCommand,
                ClaimsPrincipal user,
                [FromBody] RoleDto roleDto
            ) =>
            {
                try
                {
                    var updatedDto = await updateRoleCommand.Handle(roleDto, user);
                    return Results.Ok(updatedDto);
                }
                catch (KeyNotFoundException ex)
                {
                    Console.Error.WriteLine($"Role not found: {ex.Message}");
                    return Results.NotFound(ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.Error.WriteLine($"Invalid operation: {ex.Message}");
                    return Results.Problem(ex.Message);
                }
                catch (DbUpdateException ex)
                {
                    Console.Error.WriteLine($"A database update error occurred: {ex.Message}");
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
            }).RequireAuthorization($"Permission:{updatePermission}");

            group.MapPost("/Delete/{id}", async (
                [FromServices] IDeleteRoleCommand deleteRoleCommand,
                ClaimsPrincipal user,
                Guid id
            ) =>
            {
                try
                {
                    await deleteRoleCommand.Handle(id, user);
                    return Results.Ok();
                }
                catch (KeyNotFoundException ex)
                {
                    Console.Error.WriteLine($"Role not found: {ex.Message}");
                    return Results.NotFound();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An error occurred: {ex.Message}");
                    return Results.Problem("An error occurred while deleting the Role.");
                }
            }).RequireAuthorization($"Permission:{deletePermission}");

            return endpoints;
        }
    }

    public class LocalStorageKeyEndpoints : IEndpoints
    {
        public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup($"/api/auth/localstoragekeys");

            // View permission for GET operations
            var viewPermission = PermissionHelper.GeneratePermission<LocalStorageKey>(PermissionOperations.View);
            // Create permission for POST operations
            var createPermission = PermissionHelper.GeneratePermission<LocalStorageKey>(PermissionOperations.Create);
            // Update permission for PUT operations
            var updatePermission = PermissionHelper.GeneratePermission<LocalStorageKey>(PermissionOperations.Update);
            // Delete permission for DELETE operations
            var deletePermission = PermissionHelper.GeneratePermission<LocalStorageKey>(PermissionOperations.Delete);

            group.MapPost("/generate", async (
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

                    return Results.Ok(new
                    {
                        message = "RSA keypair generated successfully",
                        keyId = newKey.Id,
                        publicKey = newKey.PublicKey,
                        keyGeneratedDate = newKey.KeyGeneratedDate
                    });
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An error occurred: {ex.Message}");
                    return Results.Problem("An error occurred while generating the RSA keypair.");
                }
            }).RequireAuthorization($"Permission:{createPermission}");

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
            }).RequireAuthorization($"Permission:{viewPermission}");

            group.MapPost("/Revoke/{id}", async (
                [FromServices] IRevokeLocalStorageKeyCommand revokeKeyCommand,
                ClaimsPrincipal user,
                Guid id
            ) =>
            {
                try
                {
                    await revokeKeyCommand.Handle(id, user);
                    return Results.Ok();
                }
                catch (KeyNotFoundException ex)
                {
                    Console.Error.WriteLine($"LocalStorageKey not found: {ex.Message}");
                    return Results.NotFound();
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An error occurred: {ex.Message}");
                    return Results.Problem("An error occurred while revoking the LocalStorageKey.");
                }
            }).RequireAuthorization($"Permission:{updatePermission}");

            return endpoints;
        }
    }

    public class PermissionEndpoints : IEndpoints
    {
        public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup($"/api/auth/permissions");

            // View permission for GET operations
            var viewPermission = PermissionHelper.GeneratePermission<Permission>(PermissionOperations.View);

            group.MapGet("/GetAll", async (
                [FromServices] IGetAllPermissionsQuery getAllPermissionsQuery
            ) =>
            {
                try
                {
                    var permissions = await getAllPermissionsQuery.Handle();
                    return Results.Ok(permissions);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"An error occurred: {ex.Message}");
                    return Results.Problem("An error occurred while retrieving Permission entities.");
                }
            }).RequireAuthorization($"Permission:{viewPermission}");

            return endpoints;
        }
    }
}