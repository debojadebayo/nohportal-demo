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
using System.Security.Claims;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ComposedHealthBase.Server.Endpoints
{
	public abstract class BaseEndpoints<T, TDto, TContext>
	where T : class, IAuditEntity
	where TDto : IDto
	where TContext : IDbContext<TContext>
	{
		public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var endpointName = typeof(T).Name;
			var group = endpoints.MapGroup($"/api/{endpointName}");

			group.MapGet("/GetAll", (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] GetAllQuery<T, TDto, TContext> getAllQuery,
				ClaimsPrincipal user
			) => GetAll(getAllQuery, user));

			group.MapGet("/GetById/{id}", (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] GetByIdQuery<T, TDto, TContext> getByIdQuery,
				ClaimsPrincipal user,
				long id
			) => GetById(getByIdQuery, user, id));

			group.MapPost("/GetByIds", (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] GetByIdsQuery<T, TDto, TContext> getByIdsQuery,
				ClaimsPrincipal user,
				List<long> ids
			) => GetByIds(getByIdsQuery, user, ids));

			group.MapPost("/Create", (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] CreateCommand<T, TDto, TContext> createCommand,
				ClaimsPrincipal user,
				TDto dto
			) => Create(createCommand, user, dto));

			group.MapPut("/Update", (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] UpdateCommand<T, TDto, TContext> updateCommand,
				ClaimsPrincipal user,
				TDto dto
			) => Update(updateCommand, user, dto));

			group.MapPost("/Delete/{id}", (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] DeleteCommand<T, TContext> deleteCommand,
				ClaimsPrincipal user,
				long id
			) => Delete(deleteCommand, user, id));

			// New endpoints
			group.MapGet("/GetAllByTenantId/{tenantId}", (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] GetAllByTenantIdQuery<T, TDto, TContext> getAllByTenantIdQuery,
				ClaimsPrincipal user,
				long tenantId
			) => GetAllByTenantId(getAllByTenantIdQuery, user, tenantId));

			group.MapPost("/GetAllByTenantIds", (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] GetAllByTenantIdsQuery<T, TDto, TContext> getAllByTenantIdsQuery,
				ClaimsPrincipal user,
				List<long> tenantIds
			) => GetAllByTenantIds(getAllByTenantIdsQuery, user, tenantIds));

			group.MapGet("/GetAllBySubjectId/{subjectId}", (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] GetAllBySubjectIdQuery<T, TDto, TContext> getAllBySubjectIdQuery,
				ClaimsPrincipal user,
				long subjectId
			) => GetAllBySubjectId(getAllBySubjectIdQuery, user, subjectId));

			group.MapPost("/GetAllBySubjectIds", (
				[FromServices] IDbContext<TContext> dbContext,
				[FromServices] IMapper<T, TDto> mapper,
				[FromServices] GetAllBySubjectIdsQuery<T, TDto, TContext> getAllBySubjectIdsQuery,
				ClaimsPrincipal user,
				List<long> subjectIds
			) => GetAllBySubjectIds(getAllBySubjectIdsQuery, user, subjectIds));

			return endpoints;
		}

		protected async Task<IResult> GetAll(GetAllQuery<T, TDto, TContext> getAllQuery, ClaimsPrincipal user)
		{
			try
			{
				var allEntities = await getAllQuery.Handle(user);
				return Results.Ok(allEntities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities.");
			}
		}

		protected async Task<IResult> GetById(GetByIdQuery<T, TDto, TContext> getByIdQuery, ClaimsPrincipal user, long id)
		{
			try
			{
				var entity = await getByIdQuery.Handle(id, user);
				return Results.Ok(entity);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving the {typeof(T).Name} details.");
			}
		}

		protected async Task<IResult> GetByIds(GetByIdsQuery<T, TDto, TContext> getByIdsQuery, ClaimsPrincipal user, List<long> ids)
		{
			try
			{
				var entities = await getByIdsQuery.Handle(ids, user);
				return Results.Ok(entities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving the {typeof(T).Name} entities.");
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

		protected async Task<IResult> Delete(DeleteCommand<T, TContext> deleteCommand, ClaimsPrincipal user, long id)
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

		// New methods for tenant and subject filtering

		protected async Task<IResult> GetAllByTenantId(GetAllByTenantIdQuery<T, TDto, TContext> getAllByTenantIdQuery, ClaimsPrincipal user, long tenantId)
		{
			try
			{
				var entities = await getAllByTenantIdQuery.Handle(tenantId, user);
				return Results.Ok(entities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities by tenantId.");
			}
		}

		protected async Task<IResult> GetAllByTenantIds(GetAllByTenantIdsQuery<T, TDto, TContext> getAllByTenantIdsQuery, ClaimsPrincipal user, List<long> tenantIds)
		{
			try
			{
				var entities = await getAllByTenantIdsQuery.Handle(tenantIds, user);
				return Results.Ok(entities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities by tenantIds.");
			}
		}

		protected async Task<IResult> GetAllBySubjectId(GetAllBySubjectIdQuery<T, TDto, TContext> getAllBySubjectIdQuery, ClaimsPrincipal user, long subjectId)
		{
			try
			{
				var entities = await getAllBySubjectIdQuery.Handle(subjectId, user);
				return Results.Ok(entities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities by subjectId.");
			}
		}

		protected async Task<IResult> GetAllBySubjectIds(GetAllBySubjectIdsQuery<T, TDto, TContext> getAllBySubjectIdsQuery, ClaimsPrincipal user, List<long> subjectIds)
		{
			try
			{
				var entities = await getAllBySubjectIdsQuery.Handle(subjectIds, user);
				return Results.Ok(entities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities by subjectIds.");
			}
		}
	}
}