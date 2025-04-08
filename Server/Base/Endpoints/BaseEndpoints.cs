using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using MediatR;

namespace ComposedHealthBase.Server.BaseModule.Endpoints
{
	public class BaseEndpoints<T>
	where T : class
	{
		protected virtual IEndpointRouteBuilder MapEndpoints(IEndpointRouteBuilder endpoints)
		{
			var endpointName = typeof(T).Name;
			var group = endpoints.MapGroup($"/api/{endpointName}");

			group.MapGet("/GetAll", (IMediator mediator) => GetAll(mediator));
			group.MapGet("/GetById/{id}", (IMediator mediator, int id) => GetById(mediator, id));
			group.MapPost("/Create", (IMediator mediator, T entity) => Create(mediator, entity));
			group.MapPut("/Update", (IMediator mediator, T entity) => Update(mediator, entity));
			group.MapPost("/Delete/{id}", (IMediator mediator, int id) => Delete(mediator, id));

			return endpoints;
		}

		protected async Task<IResult> GetAll(IMediator mediator)
		{
			try
			{
				var allEntities = await mediator.Send(new GetAllQuery<T>());

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

		protected async Task<IResult> GetById(IMediator mediator, int id)
		{
			try
			{
				var entity = await mediator.Send(new GetByIdQuery<T>(id));

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

		protected async Task<IResult> Create(IMediator mediator, T entity)
		{
			try
			{
				var result = await mediator.Send(new CreateCommand<T>(entity));

				if (!result.Success)
				{
					return Results.BadRequest(result.Message);
				}

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while creating the {typeof(T).Name}.");
			}
		}

		protected async Task<IResult> Update(IMediator mediator, T entity)
		{
			try
			{
				var result = await mediator.Send(new UpdateCommand<T>(entity));

				if (!result.Success)
				{
					return Results.BadRequest(result.Message);
				}

				return Results.Ok(result);
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine($"An error occurred: {ex.Message}");
				return Results.Problem($"An error occurred while updating the {typeof(T).Name}.");
			}
		}

		private async Task<IResult> Delete(IMediator mediator, int id)
		{
			try
			{
				var result = await mediator.Send(new DeleteCommand<T>(id));

				if (!result.Success)
				{
					return Results.BadRequest(result.Message);
				}

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