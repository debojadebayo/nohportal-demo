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
        Task<List<TDto>> Handle(Expression<System.Func<T, bool>> predicate);
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

        public async Task<List<TDto>> Handle(Expression<System.Func<T, bool>> predicate)
        {
            var entities = await _dbContext.Set<T>().Where(predicate).ToListAsync();
            return entities.ConvertAll(e => _mapper.Map(e));
        }
    }
}
