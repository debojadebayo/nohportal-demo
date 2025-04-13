
using ComposedHealthBase.Server.BaseModule.Entities;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using NationOH.Server.Base.Infrastructure.Mappers;

namespace ComposedHealthBase.Server.BaseModule.Infrastructure.Queries
{
    public interface IGetAllQuery<T, TDto>
    {
        Task<IEnumerable<TDto>> Handle();
    }

    public class GetAllQuery<T, TDto> : IGetAllQuery<T, TDto>
    where T : BaseEntity<T>
    where TDto : BaseDto<TDto>
    {
        public IDbContext _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }

        public GetAllQuery(IDbContext dbContext, IMapper<T, TDto> mapper)
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