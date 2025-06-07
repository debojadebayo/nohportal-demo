using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ComposedHealthBase.Server.Queries
{
    public interface IGetAllQuery<T, TDto, TContext>
    {
        Task<IEnumerable<TDto>> Handle(ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes);
    }

    public class GetAllQuery<T, TDto, TContext> : IGetAllQuery<T, TDto, TContext>
    where T : BaseEntity<T>
    where TDto : IDto
    where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }

        public GetAllQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TDto>> Handle(ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbContext.Set<T>().AsNoTracking();
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entities = await query.ToListAsync();
            return _mapper.Map(entities);
        }
    }
}