using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;

namespace ComposedHealthBase.Server.Queries
{
	public class GetAllBySubjectIdQuery<T, TDto, TContext>
		where T : BaseEntity<T>
		where TDto : IDto
		where TContext : IDbContext<TContext>
	{
		private readonly IDbContext<TContext> _dbContext;
		private readonly IMapper<T, TDto> _mapper;

		public GetAllBySubjectIdQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<IEnumerable<TDto>> Handle(long subjectId)
		{
			// Assumes T has a SubjectId property
			var entities = await _dbContext.Set<T>().Where(e => e.SubjectId == subjectId).ToListAsync();
			return entities.Select(e => _mapper.ToDto(e));
		}
	}
}
