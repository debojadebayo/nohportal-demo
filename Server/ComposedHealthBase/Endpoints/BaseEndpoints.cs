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
			var group = endpoints.MapGroup($"/api/{endpointName}");

			group.MapGet("/GetAll", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper) => GetAll(dbContext, mapper));
			group.MapGet("/GetById/{id}", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, long id) => GetById(dbContext, mapper, id));
			group.MapPost("/GetByIds", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, List<long> ids) => GetByIds(dbContext, mapper, ids));
			group.MapPost("/Create", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, TDto dto) => Create(dbContext, mapper, dto));
			group.MapPut("/Update", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, TDto dto) => Update(dbContext, mapper, dto));
			group.MapPost("/Delete/{id}", ([FromServices] IDbContext<TContext> dbContext, [FromServices] IMapper<T, TDto> mapper, long id) => Delete(dbContext, mapper, id));

			return endpoints;
		}

		protected async Task<IResult> GetAll(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
		{
			try
			{
				var allEntities = await new GetAllQuery<T, TDto, TContext>(dbContext, mapper).Handle();
				return Results.Ok(allEntities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving {typeof(T).Name} entities.");
			}
		}

		protected async Task<IResult> GetById(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, long id)
		{
			try
			{
				var entity = await new GetByIdQuery<T, TDto, TContext>(dbContext, mapper).Handle(id);
				return Results.Ok(entity);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving the {typeof(T).Name} details.");
			}
		}

		protected async Task<IResult> GetByIds(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, List<long> ids)
		{
			try
			{
				var entities = await new GetByIdsQuery<T, TDto, TContext>(dbContext, mapper).Handle(ids);
				return Results.Ok(entities);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while retrieving the {typeof(T).Name} entities.");
			}
		}

		protected async Task<IResult> Create(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, TDto dto)
		{
			try
			{
				var result = await new CreateCommand<T, TDto, TContext>(dbContext, mapper).Handle(dto);
				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while creating the {typeof(T).Name}.");
			}
		}

		protected async Task<IResult> Update(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, TDto dto)
		{
			try
			{
				var result = await new UpdateCommand<T, TDto, TContext>(dbContext, mapper).Handle(dto);
				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while updating the {typeof(T).Name}.");
			}
		}

		protected async Task<IResult> Delete(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, long id)
		{
			try
			{
				var result = await new DeleteCommand<T, TContext>(dbContext).Handle(id);

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