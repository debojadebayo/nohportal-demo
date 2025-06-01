using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using System.Linq.Expressions;

namespace ComposedHealthBase.Server.Queries
{
    public interface IGetPagedQuery<T, TDto, TContext>
    {
        Task<IEnumerable<TDto>> Handle(int page, int pageSize, params Expression<Func<T, object>>[]? includes);
    }

    public class GetPagedQuery<T, TDto, TContext> : IGetPagedQuery<T, TDto, TContext>
    where T : BaseEntity<T>
    where TDto : IDto
    where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }

        public GetPagedQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TDto>> Handle(int page, int pageSize, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbContext.Set<T>().AsNoTracking();
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entities = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            return _mapper.Map(entities);
        }
    }
}