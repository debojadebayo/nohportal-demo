using System.Linq.Expressions;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ComposedHealthBase.Server.Queries
{
	public interface IGetAllByTenantIdQuery<T, TDto, TContext>
		where T : BaseEntity<T>
		where TDto : IDto
		where TContext : IDbContext<TContext>
	{
		Task<IEnumerable<TDto>> Handle(long tenantId, params Expression<Func<T, object>>[]? includes);
	}
	public class GetAllByTenantIdQuery<T, TDto, TContext> : IGetAllByTenantIdQuery<T, TDto, TContext>
		where T : BaseEntity<T>
		where TDto : IDto
		where TContext : IDbContext<TContext>
	{
		private readonly IDbContext<TContext> _dbContext;
		private readonly IMapper<T, TDto> _mapper;

		public GetAllByTenantIdQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<TDto>> Handle(long tenantId, params Expression<Func<T, object>>[]? includes)
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
			return entities.Select(e => _mapper.Map(e));
		}
	}
}
