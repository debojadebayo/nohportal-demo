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
	where T : BaseEntity<T>
	where TDto : IDto
	where TContext : IDbContext<TContext>
	{
		public virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var endpointName = typeof(T).Name;
			var group = endpoints.MapGroup($"/api/{endpointName}")
				.RequireAuthorization("resource-access");

			group.MapGet("/debugclaims", (ClaimsPrincipal user) =>
            {
                if (user?.Identity?.IsAuthenticated == true)
                {
                    var claimsInfo = user.Claims.Select(c => new { c.Type, c.Value }).ToList();
                    var identityRoleClaimType = "Unknown";
                    if (user.Identity is ClaimsIdentity claimsIdentity)
                    {
                        identityRoleClaimType = claimsIdentity.RoleClaimType;
                    }
                    return Results.Ok(new 
                    { 
                        IsAuthenticated = user.Identity.IsAuthenticated,
                        AuthenticationType = user.Identity.AuthenticationType,
                        NameClaimType = (user.Identity as ClaimsIdentity)?.NameClaimType,
                        RoleClaimType = identityRoleClaimType,
                        Claims = claimsInfo,
                        IsInAdministratorRole = user.IsInRole("administrator")
                    });
                }
                return Results.Unauthorized();
            }).RequireAuthorization();

			group.MapGet("/GetAll", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, ClaimsPrincipal user) => GetAll(dbContext, mapper, user));
			group.MapGet("/GetById/{id}", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, ClaimsPrincipal user, long id) => GetById(dbContext, mapper, user, id));
			group.MapPost("/GetByIds", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, ClaimsPrincipal user, List<long> ids) => GetByIds(dbContext, mapper, user, ids));
			group.MapPost("/Create", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, ClaimsPrincipal user, TDto dto) => Create(dbContext, mapper, user, dto));
			group.MapPut("/Update", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, ClaimsPrincipal user, TDto dto) => Update(dbContext, mapper, user, dto));
			group.MapPost("/Delete/{id}", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, ClaimsPrincipal user, long id) => Delete(dbContext, mapper, user, id));

			// New endpoints
			group.MapGet("/GetAllByTenantId/{tenantId}", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, ClaimsPrincipal user, long tenantId) => GetAllByTenantId(dbContext, mapper, user, tenantId));
			group.MapPost("/GetAllByTenantIds", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, ClaimsPrincipal user, List<long> tenantIds) => GetAllByTenantIds(dbContext, mapper, user, tenantIds));
			group.MapGet("/GetAllBySubjectId/{subjectId}", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, ClaimsPrincipal user, long subjectId) => GetAllBySubjectId(dbContext, mapper, user, subjectId));
			group.MapPost("/GetAllBySubjectIds", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, ClaimsPrincipal user, List<long> subjectIds) => GetAllBySubjectIds(dbContext, mapper, user, subjectIds));

			return endpoints;
		}

		protected async Task<IResult> GetAll(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, ClaimsPrincipal user)
		{
			try
			{
				var allEntities = await new GetAllQuery<T, TDto, TContext>(dbContext, mapper).Handle(user);
				return Results.Ok(allEntities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities.");
			}
		}

		protected async Task<IResult> GetById(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, ClaimsPrincipal user, long id)
		{
			try
			{
				var entity = await new GetByIdQuery<T, TDto, TContext>(dbContext, mapper).Handle(id, user);
				return Results.Ok(entity);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving the {typeof(T).Name} details.");
			}
		}

		protected async Task<IResult> GetByIds(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, ClaimsPrincipal user, List<long> ids)
		{
			try
			{
				var entities = await new GetByIdsQuery<T, TDto, TContext>(dbContext, mapper).Handle(ids, user);
				return Results.Ok(entities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving the {typeof(T).Name} entities.");
			}
		}

		protected async Task<IResult> Create(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, ClaimsPrincipal user, TDto dto)
		{
			try
			{
				var result = await new CreateCommand<T, TDto, TContext>(dbContext, mapper).Handle(dto, user);
				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while creating the {typeof(T).Name}.");
			}
		}

		protected async Task<IResult> Update(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, ClaimsPrincipal user, TDto dto)
		{
			try
			{
				var result = await new UpdateCommand<T, TDto, TContext>(dbContext, mapper).Handle(dto, user);
				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while updating the {typeof(T).Name}.");
			}
		}

		protected async Task<IResult> Delete(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, ClaimsPrincipal user, long id)
		{
			try
			{
				var result = await new DeleteCommand<T, TContext>(dbContext).Handle(id, user);

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while deleting the {typeof(T).Name}.");
			}
		}

		// New methods for tenant and subject filtering

		protected async Task<IResult> GetAllByTenantId(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, ClaimsPrincipal user, long tenantId)
		{
			try
			{
				var entities = await new GetAllByTenantIdQuery<T, TDto, TContext>(dbContext, mapper).Handle(tenantId, user);
				return Results.Ok(entities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities by tenantId.");
			}
		}

		protected async Task<IResult> GetAllByTenantIds(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, ClaimsPrincipal user, List<long> tenantIds)
		{
			try
			{
				var entities = await new GetAllByTenantIdsQuery<T, TDto, TContext>(dbContext, mapper).Handle(tenantIds, user);
				return Results.Ok(entities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities by tenantIds.");
			}
		}

		protected async Task<IResult> GetAllBySubjectId(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, ClaimsPrincipal user, long subjectId)
		{
			try
			{
				var entities = await new GetAllBySubjectIdQuery<T, TDto, TContext>(dbContext, mapper).Handle(subjectId, user);
				return Results.Ok(entities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities by subjectId.");
			}
		}

		protected async Task<IResult> GetAllBySubjectIds(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, ClaimsPrincipal user, List<long> subjectIds)
		{
			try
			{
				var entities = await new GetAllBySubjectIdsQuery<T, TDto, TContext>(dbContext, mapper).Handle(subjectIds, user);
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