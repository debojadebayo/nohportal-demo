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

namespace ComposedHealthBase.Server.Queries
{
	public interface IGetAllBySubjectIdsQuery<T, TDto, TContext>
	{
		Task<IEnumerable<TDto>> Handle(List<long> subjectIds, ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes);
	}
	public class GetAllBySubjectIdsQuery<T, TDto, TContext> : IGetAllBySubjectIdsQuery<T, TDto, TContext>
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

		public async Task<IEnumerable<TDto>> Handle(List<long> subjectIds, ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes)
		{
			var query = _dbContext.Set<T>().AsNoTracking().Where(e => subjectIds.Contains(e.SubjectId));
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
