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
	public abstract class KeycloakSubjectEndpoints<T, TDto, TContext> : BaseEndpoints<T, TDto, TContext>
	where T : class, IEntity, IAuditEntity, ISubject
	where TDto : IDto, ISubject
	where TContext : IDbContext<TContext>
	{
		public override IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			endpoints = base.MapEndpoints(endpoints);
			var group = endpoints.MapGroup($"/api/security");

			group.MapPost("/createsubject", async (
				[FromServices] CreateSubjectCommand<T, TDto, TContext> createSubjectCommand,
				ClaimsPrincipal user,
				[FromBody] TDto dto
			) => await CreateSubject(createSubjectCommand, user, dto));

			return endpoints;
		}

		// Method for creating a subject (user)
		protected async Task<IResult> CreateSubject(
			CreateSubjectCommand<T, TDto, TContext> createSubjectCommand,
			ClaimsPrincipal user,
			TDto dto)
		{
			var id = await createSubjectCommand.Handle(dto, user);
			return Results.Ok(id);
		}
	}
}