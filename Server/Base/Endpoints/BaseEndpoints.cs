using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Commands;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Queries;
using AutoMapper;
using ComposedHealthBase.Server.BaseModule.Entities;

namespace ComposedHealthBase.Server.BaseModule.Endpoints
{
	public abstract class BaseEndpoints<T, TDto>
	where T : BaseEntity<T>
	where TDto : class
	{
		public IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var endpointName = typeof(T).Name;
			var group = endpoints.MapGroup($"/api/{endpointName}");

			group.MapGet("/GetAll", (IDbContext dbContext, IMapper mapper) => GetAll(dbContext, mapper));
			// group.MapGet("/GetById/{id}", (IDbContext dbContext, int id) => GetById(dbContext, id));
			// group.MapPost("/Create", (IDbContext dbContext, T entity) => Create(dbContext, entity));
			// group.MapPut("/Update", (IDbContext dbContext, T entity) => Update(dbContext, entity));
			// group.MapPost("/Delete/{id}", (IDbContext dbContext, int id) => Delete(dbContext, id));

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

		// protected async Task<IResult> GetById(IDbContext dbContext, int id)
		// {
		// 	try
		// 	{
		// 		var entity = await dbContext.Send(new GetByIdQuery<T>(id));

		// 		if (entity == null)
		// 		{
		// 			return Results.NotFound($"{typeof(T).Name} with ID {id} not found.");
		// 		}

		// 		return Results.Ok(entity);
		// 	}
		// 	catch (Exception ex)
		// 	{
		// 		Console.Error.WriteLine($"An error occurred: {ex.Message}");
		// 		return Results.Problem($"An error occurred while retrieving the {typeof(T).Name} details.");
		// 	}
		// }

		// protected async Task<IResult> Create(IDbContext dbContext, T entity)
		// {
		// 	try
		// 	{
		// 		var result = await dbContext.Send(new CreateCommand<T>(entity));

		// 		if (!result.Success)
		// 		{
		// 			return Results.BadRequest(result.Message);
		// 		}

		// 		return Results.Ok(result);
		// 	}
		// 	catch (Exception ex)
		// 	{
		// 		Console.Error.WriteLine($"An error occurred: {ex.Message}");
		// 		return Results.Problem($"An error occurred while creating the {typeof(T).Name}.");
		// 	}
		// }

		// protected async Task<IResult> Update(IDbContext dbContext, T entity)
		// {
		// 	try
		// 	{
		// 		var result = await dbContext.Send(new UpdateCommand<T>(entity));

		// 		if (!result.Success)
		// 		{
		// 			return Results.BadRequest(result.Message);
		// 		}

		// 		return Results.Ok(result);
		// 	}
		// 	catch (Exception ex)
		// 	{
		// 		Console.Error.WriteLine($"An error occurred: {ex.Message}");
		// 		return Results.Problem($"An error occurred while updating the {typeof(T).Name}.");
		// 	}
		// }

		// private async Task<IResult> Delete(IDbContext dbContext, int id)
		// {
		// 	try
		// 	{
		// 		var result = await dbContext.Send(new DeleteCommand<T>(id));

		// 		if (!result.Success)
		// 		{
		// 			return Results.BadRequest(result.Message);
		// 		}

		// 		return Results.Ok(result);
		// 	}
		// 	catch (Exception ex)
		// 	{
		// 		Console.Error.WriteLine($"An error occurred: {ex.Message}");
		// 		return Results.Problem($"An error occurred while deleting the {typeof(T).Name}.");
		// 	}
		// }
	}
}