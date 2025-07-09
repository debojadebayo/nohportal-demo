using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Commands;
using ComposedHealthBase.Server.Queries;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Auth.Constants;
using ComposedHealthBase.Server.Auth.Requirements;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ComposedHealthBase.Server.Endpoints
{
    public abstract class BaseEndpoints<T, TDto, TContext>
    where T : class, IEntity, IAuditEntity
    where TDto : IDto
    where TContext : IDbContext<TContext>
    {
        public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
        {
            var endpointName = typeof(T).Name;
            var group = endpoints.MapGroup($"/api/{endpointName}");

            // View permissions for GET operations (GetAll, GetById, GetByIds, Search)
            var viewPermission = PermissionHelper.GeneratePermission<T>(PermissionOperations.View);

            group.MapGet("/GetAll", (
                [FromServices] IDbContext<TContext> dbContext,
                [FromServices] IMapper<T, TDto> mapper,
                [FromServices] GetAllQuery<T, TDto, TContext> getAllQuery,
                ClaimsPrincipal user,
                [FromQuery] Guid? tenantId = null,
                [FromQuery] Guid? subjectId = null
            ) => GetAll(getAllQuery, user, tenantId, subjectId))
            .RequireAuthorization($"Permission:{viewPermission}");

            group.MapGet("/GetById/{id}", (
                [FromServices] IDbContext<TContext> dbContext,
                [FromServices] IMapper<T, TDto> mapper,
                [FromServices] GetByIdQuery<T, TDto, TContext> getByIdQuery,
                ClaimsPrincipal user,
                Guid id,
                [FromQuery] Guid? tenantId = null,
                [FromQuery] Guid? subjectId = null
            ) => GetById(getByIdQuery, user, id, tenantId, subjectId))
            .RequireAuthorization($"Permission:{viewPermission}");

            group.MapPost("/GetByIds", (
                [FromServices] IDbContext<TContext> dbContext,
                [FromServices] IMapper<T, TDto> mapper,
                [FromServices] GetByIdsQuery<T, TDto, TContext> getByIdsQuery,
                ClaimsPrincipal user,
                List<Guid> ids,
                [FromQuery] Guid? tenantId = null,
                [FromQuery] Guid? subjectId = null
            ) => GetByIds(getByIdsQuery, user, ids, tenantId, subjectId))
            .RequireAuthorization($"Permission:{viewPermission}");

            group.MapGet("/Search", (
                [FromServices] IDbContext<TContext> dbContext,
                [FromServices] IMapper<T, TDto> mapper,
                [FromServices] SearchQuery<T, TDto, TContext> searchQuery,
                ClaimsPrincipal user,
                [FromQuery] string term,
                [FromQuery] Guid? tenantId = null,
                [FromQuery] Guid? subjectId = null
            ) => Search(searchQuery, user, term, tenantId, subjectId))
            .RequireAuthorization($"Permission:{viewPermission}");

            // Create permission for POST operations
            var createPermission = PermissionHelper.GeneratePermission<T>(PermissionOperations.Create);

            group.MapPost("/Create", (
                [FromServices] IDbContext<TContext> dbContext,
                [FromServices] IMapper<T, TDto> mapper,
                [FromServices] CreateCommand<T, TDto, TContext> createCommand,
                ClaimsPrincipal user,
                TDto dto
            ) => Create(createCommand, user, dto))
            .RequireAuthorization($"Permission:{createPermission}");

            // Update permission for PUT operations
            var updatePermission = PermissionHelper.GeneratePermission<T>(PermissionOperations.Update);

            group.MapPut("/Update", (
                [FromServices] IDbContext<TContext> dbContext,
                [FromServices] IMapper<T, TDto> mapper,
                [FromServices] UpdateCommand<T, TDto, TContext> updateCommand,
                ClaimsPrincipal user,
                TDto dto
            ) => Update(updateCommand, user, dto))
            .RequireAuthorization($"Permission:{updatePermission}");

            // Delete permission for DELETE operations  
            var deletePermission = PermissionHelper.GeneratePermission<T>(PermissionOperations.Delete);

            group.MapPost("/Delete/{id}", (
                [FromServices] IDbContext<TContext> dbContext,
                [FromServices] IMapper<T, TDto> mapper,
                [FromServices] DeleteCommand<T, TContext> deleteCommand,
                ClaimsPrincipal user,
                Guid id
            ) => Delete(deleteCommand, user, id))
            .RequireAuthorization($"Permission:{deletePermission}");

            return endpoints;
        }

        protected async Task<IResult> GetAll(GetAllQuery<T, TDto, TContext> getAllQuery, ClaimsPrincipal user, Guid? tenantId, Guid? subjectId)
        {
            try
            {
                var allEntities = await getAllQuery.Handle(user, tenantId, subjectId);
                if (allEntities == null || !allEntities.Any())
                    return Results.NoContent();
                return Results.Ok(allEntities);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities.");
            }
        }

        protected async Task<IResult> GetById(GetByIdQuery<T, TDto, TContext> getByIdQuery, ClaimsPrincipal user, Guid id, Guid? tenantId, Guid? subjectId)
        {
            try
            {
                var entity = await getByIdQuery.Handle(id, user, tenantId, subjectId);
                if (entity == null)
                    return Results.NoContent();
                return Results.Ok(entity);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return Results.Problem($"An error occurred while retrieving the {typeof(T).Name} details.");
            }
        }

        protected async Task<IResult> GetByIds(GetByIdsQuery<T, TDto, TContext> getByIdsQuery, ClaimsPrincipal user, List<Guid> ids, Guid? tenantId, Guid? subjectId)
        {
            try
            {
                var entities = await getByIdsQuery.Handle(ids, user, tenantId, subjectId);
                if (entities == null || !entities.Any())
                    return Results.NoContent();
                return Results.Ok(entities);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return Results.Problem($"An error occurred while retrieving the {typeof(T).Name} entities.");
            }
        }

        protected async Task<IResult> Search(SearchQuery<T, TDto, TContext> searchQuery, ClaimsPrincipal user, string term, Guid? tenantId, Guid? subjectId)
        {
            try
            {
                var results = await searchQuery.Handle(user, term, tenantId, subjectId);
                if (results == null || !results.Any())
                    return Results.NoContent();
                return Results.Ok(results);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return Results.Problem($"An error occurred while searching for {typeof(T).Name} entities.");
            }
        }

        protected async Task<IResult> Create(CreateCommand<T, TDto, TContext> createCommand, ClaimsPrincipal user, TDto dto)
        {
            try
            {
                var result = await createCommand.Handle(dto, user);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return Results.Problem($"An error occurred while creating the {typeof(T).Name}.");
            }
        }

        protected async Task<IResult> Update(UpdateCommand<T, TDto, TContext> updateCommand, ClaimsPrincipal user, TDto dto)
        {
            try
            {
                var result = await updateCommand.Handle(dto, user);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return Results.Problem($"An error occurred while updating the {typeof(T).Name}.");
            }
        }

        protected async Task<IResult> Delete(DeleteCommand<T, TContext> deleteCommand, ClaimsPrincipal user, Guid id)
        {
            try
            {
                var result = await deleteCommand.Handle(id, user);
                return Results.Ok(result);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"An error occurred: {ex.Message}");
                return Results.Problem($"An error occurred while deleting the {typeof(T).Name}.");
            }
        }
    }
}