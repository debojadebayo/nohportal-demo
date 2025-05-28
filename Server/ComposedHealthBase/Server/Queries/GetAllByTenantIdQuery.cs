using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ComposedHealthBase.Server.Queries
{
	public class GetAllByTenantIdQuery<T, TDto, TContext>
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

		public async Task<IEnumerable<TDto>> Handle(long tenantId)
		{
			// Assumes T has a TenantId property
			var entities = await _dbContext.Set<T>().Where(e => e.TenantId == tenantId).ToListAsync();
			return entities.Select(e => _mapper.Map(e));
		}
	}
}
