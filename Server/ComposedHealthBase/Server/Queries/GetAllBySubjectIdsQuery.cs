using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ComposedHealthBase.Server.Queries
{
	public class GetAllBySubjectIdsQuery<T, TDto, TContext>
		where T : BaseEntity<T>
		where TDto : IDto
		where TContext : IDbContext<TContext>
	{
		private readonly IDbContext<TContext> _dbContext;
		private readonly IMapper<T, TDto> _mapper;

		public GetAllBySubjectIdsQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<TDto>> Handle(List<long> subjectIds)
		{
			// Assumes T has a SubjectId property
			var entities = await _dbContext.Set<T>().Where(e => subjectIds.Contains(e.SubjectId)).ToListAsync();
			return entities.Select(e => _mapper.Map(e));
		}
	}
}
