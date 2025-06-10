using System.Linq.Expressions;
using System.Security.Claims;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ComposedHealthBase.Server.Interfaces;

namespace ComposedHealthBase.Server.Queries
{
	public interface IGetAllByTenantIdQuery<T, TDto, TContext>
		where T : BaseEntity<T>
		where TDto : IDto
		where TContext : IDbContext<TContext>
	{
		Task<IEnumerable<TDto>> Handle(long tenantId, ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes);
	}
	public class GetAllByTenantIdQuery<T, TDto, TContext> : IGetAllByTenantIdQuery<T, TDto, TContext>, IQuery
		where T : BaseEntity<T>
		where TDto : IDto
		where TContext : IDbContext<TContext>
	{
		private readonly IDbContext<TContext> _dbContext;
		private readonly IMapper<T, TDto> _mapper;
		private readonly IAuthorizationService _authorizationService;

		public GetAllByTenantIdQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, IAuthorizationService authorizationService)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_authorizationService = authorizationService;
		}

		public async Task<IEnumerable<TDto>> Handle(long tenantId, ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes)
		{
			var query = _dbContext.Set<T>().AsNoTracking().Where(e => e.TenantId == tenantId);
			if (includes != null && includes.Length > 0)
			{
				foreach (var include in includes)
				{
					query = query.Include(include);
				}
			}
			var entities = await query.ToListAsync();
			var authorizedEntities = new List<TDto>();
			foreach (var entity in entities)
			{
				var authResult = await _authorizationService.AuthorizeAsync(user, entity, "resource-access");
				if (authResult.Succeeded)
				{
					authorizedEntities.Add(_mapper.Map(entity));
				}
			}
			return authorizedEntities;
		}
	}
}
