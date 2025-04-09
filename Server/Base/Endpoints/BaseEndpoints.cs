using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Commands;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Queries;
using AutoMapper;
using ComposedHealthBase.Server.BaseModule.Entities;
using ComposedHealthBase.Shared.DTOs;

namespace ComposedHealthBase.Server.BaseModule.Endpoints
{
	public abstract class BaseEndpoints<T, TDto>
    where T : BaseEntity<T>
    where TDto : BaseDto<TDto>
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var endpointName = typeof(T).Name;
			var group = endpoints.MapGroup($"/api/{endpointName}");

			group.MapGet("/GetAll", (IDbContext dbContext, IMapper mapper) => GetAll(dbContext, mapper));
			group.MapGet("/GetById/{id}", (IDbContext dbContext, IMapper mapper, long id) => GetById(dbContext, mapper, id));
			group.MapPost("/Create", (IDbContext dbContext, IMapper mapper, TDto dto) => Create(dbContext, mapper, dto));
			group.MapPut("/Update", (IDbContext dbContext, IMapper mapper, TDto dto) => Update(dbContext, mapper, dto));
			group.MapPost("/Delete/{id}", (IDbContext dbContext, IMapper mapper, long id) => Delete(dbContext, mapper, id));

			return endpoints;
		}

		protected async Task<IResult> GetAll(IDbContext dbContext, IMapper mapper)
		{
			try
			{
				var allEntities = await new GetAllQuery<T, TDto>(dbContext, mapper).Handle();

				if (allEntities == null || !allEntities.Any())
				{
					return Results.NotFound($"No {typeof(T).Name} entities found.");
				}

				return Results.Ok(allEntities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities.");
			}
		}

		protected async Task<IResult> GetById(IDbContext dbContext, IMapper mapper, long id)
		{
			try
			{
				var entity = await new GetByIdQuery<T, TDto>(dbContext, mapper).Handle(id);

				if (entity == null)
				{
					return Results.NotFound($"{typeof(T).Name} with ID {id} not found.");
				}

				return Results.Ok(entity);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving the {typeof(T).Name} details.");
			}
		}

		protected async Task<IResult> Create(IDbContext dbContext, IMapper mapper, TDto dto)
		{
			try
			{
				var result = await new CreateCommand<T, TDto>(dbContext, mapper).Handle(dto);

				if (result == null || result <= 0)
				{
					return Results.BadRequest($"Failed to create the {typeof(T).Name}.");
				}

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while creating the {typeof(T).Name}.");
			}
		}

		protected async Task<IResult> Update(IDbContext dbContext, IMapper mapper, TDto dto)
		{
			try
			{
				var result = await new UpdateCommand<T, TDto>(dbContext, mapper).Handle(dto);

				if (result == null || result <= 0)
				{
					return Results.BadRequest(result);
				}

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while updating the {typeof(T).Name}.");
			}
		}

		protected async Task<IResult> Delete(IDbContext dbContext, IMapper mapper, long id)
		{
			try
			{
				var result = await new DeleteCommand<T>(dbContext).Handle(id);

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