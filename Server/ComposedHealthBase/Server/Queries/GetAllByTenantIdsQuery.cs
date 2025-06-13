using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Interfaces;

namespace ComposedHealthBase.Server.Queries
{
	public interface IGetAllByTenantIdsQuery<T, TDto, TContext>
	{
		Task<IEnumerable<TDto>> Handle(List<long> subjectIds, ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes);
	}
	public class GetAllByTenantIdsQuery<T, TDto, TContext> : IGetAllByTenantIdsQuery<T, TDto, TContext>, IQuery
		where T : class, IAuditEntity
		where TDto : IDto
		where TContext : IDbContext<TContext>
	{
		private readonly IDbContext<TContext> _dbContext;
		private readonly IMapper<T, TDto> _mapper;
		private readonly IAuthorizationService _authorizationService;

		public GetAllByTenantIdsQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper, IAuthorizationService authorizationService)
		{
			_dbContext = dbContext;
			_mapper = mapper;
			_authorizationService = authorizationService;
		}

		public async Task<IEnumerable<TDto>> Handle(List<long> tenantIds, ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes)
		{
			var query = _dbContext.Set<T>().AsNoTracking().Where(e => tenantIds.Contains(e.TenantId));
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
