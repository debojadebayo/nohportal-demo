using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Commands;
using ComposedHealthBase.Server.Queries;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.Interfaces;
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
	public abstract class KeycloakTenantEndpoints<T, TDto, TContext> : BaseEndpoints<T, TDto, TContext>
	where T : class, IEntity, IAuditEntity, ITenant
	where TDto : IDto, ITenant
	where TContext : IDbContext<TContext>
	{
		public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/security");

			group.MapPost("/createtenant", async (
				[FromServices] CreateTenantCommand<T, TDto, TContext> createTenantCommand,
				ClaimsPrincipal user,
				[FromBody] TDto dto
			) => await CreateTenant(createTenantCommand, user, dto));

			return endpoints;
		}

		// Method for creating a tenant (organization)
		protected async Task<IResult> CreateTenant(
			CreateTenantCommand<T, TDto, TContext> createTenantCommand,
			ClaimsPrincipal user,
			TDto dto)
		{
			var id = await createTenantCommand.Handle(dto, user);
			return Results.Ok(id);
		}

		
	}
}