using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using System.Linq.Expressions;

namespace ComposedHealthBase.Server.Queries
{
    public interface IGetByPredicateQuery<T, TDto, TContext>
    {
        Task<List<TDto>> Handle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[]? includes);
    }

    public class GetByPredicateQuery<T, TDto, TContext> : IGetByPredicateQuery<T, TDto, TContext>
        where T : BaseEntity<T>
        where TDto : IDto
        where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }

        public GetByPredicateQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<TDto>> Handle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbContext.Set<T>().AsNoTracking().Where(predicate);
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entities = await query.ToListAsync();
            return entities.ConvertAll(e => _mapper.Map(e));
        }
    }
}
