
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Entities;
using Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using ComposedHealthBase.Server.Mappers;

namespace ComposedHealthBase.Server.Queries
{
    public interface IGetByIdQuery<T, TDto>
    {
        Task<TDto> Handle(long id);
    }

    public class GetByIdQuery<T, TDto> : IGetByIdQuery<T, TDto>
    where T : BaseEntity<T>
    where TDto : BaseDto<TDto>
    {
        public IDbContext _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }

        public GetByIdQuery(IDbContext dbContext, IMapper<T, TDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TDto> Handle(long id)
        {
            var entity = await _dbContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
            return _mapper.Map(entity);
        }
    }
}