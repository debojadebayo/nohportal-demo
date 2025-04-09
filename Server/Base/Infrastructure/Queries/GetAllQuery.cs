using AutoMapper;
using ComposedHealthBase.Server.BaseModule.Entities;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using ComposedHealthBase.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComposedHealthBase.Server.BaseModule.Infrastructure.Queries
{
    public interface IGetAllQuery<T, TDto>
    {
        Task<IEnumerable<TDto>> Handle();
    }

    public class GetAllQuery<T, TDto> : IGetAllQuery<T, TDto>
    where T : BaseEntity<T>
    where TDto : class
    {
        public IDbContext _dbContext { get; }
        public IMapper _mapper { get; }

        public GetAllQuery(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TDto>> Handle()
        {
            var entities = await _dbContext.Set<T>().ToListAsync();
            return _mapper.Map<IEnumerable<TDto>>(entities);
        }
    }
}