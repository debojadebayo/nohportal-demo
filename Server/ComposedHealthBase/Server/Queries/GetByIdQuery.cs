using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
using System.Linq.Expressions;
using System.Security.Claims;

namespace ComposedHealthBase.Server.Queries
{
    public interface IGetByIdQuery<T, TDto, TContext>
    {
        Task<TDto?> Handle(long id, ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes);
    }

    public class GetByIdQuery<T, TDto, TContext> : IGetByIdQuery<T, TDto, TContext>
    where T : BaseEntity<T>
    where TDto : IDto
    where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }

        public GetByIdQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TDto?> Handle(long id, ClaimsPrincipal user, params Expression<Func<T, object>>[]? includes)
        {
            var query = _dbContext.Set<T>().AsNoTracking().Where(x => x.Id == id);
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            var entity = await query.SingleOrDefaultAsync();
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found");
            }
            return _mapper.Map(entity);
        }
    }
}