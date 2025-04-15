
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;

namespace ComposedHealthBase.Server.Queries
{
    public interface IGetAllQuery<T, TDto, TContext>
    {
        Task<IEnumerable<TDto>> Handle();
    }

    public class GetAllQuery<T, TDto, TContext> : IGetAllQuery<T, TDto, TContext>
    where T : BaseEntity<T>
    where TDto : BaseDto<TDto>
    where TContext : IDbContext<TContext>
    {
        public IDbContext<TContext> _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }

        public GetAllQuery(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> Handle()
        {
            var entities = await _dbContext.Set<T>().ToListAsync();
            return _mapper.Map(entities);
        }
    }
}