using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using System.Linq.Expressions;

namespace ComposedHealthBase.Server.Queries
{
    public interface IGetSingleQuery<T, TDto, TContext>
    {
        Task<TDto?> Handle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[]? includes);
    }

    public class GetSingleQuery<T, TDto, TContext> : IGetSingleQuery<T, TDto, TContext>
    where T : BaseEntity<T>
    where TDto : IDto
    where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }

        public GetSingleQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<TDto?> Handle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbContext.Set<T>().AsNoTracking();
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entity = await query.FirstOrDefaultAsync(predicate);
            if (entity == null)
            {
                return default;
            }
            return _mapper.Map(entity);
        }
    }
}