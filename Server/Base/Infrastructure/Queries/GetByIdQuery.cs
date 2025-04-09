using AutoMapper;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using ComposedHealthBase.Server.BaseModule.Entities;
using ComposedHealthBase.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComposedHealthBase.Server.BaseModule.Infrastructure.Queries
{
    public interface IGetByIdQuery<T, TDto>
    {
        Task<TDto> Handle(long id);
    }

    public class GetByIdQuery<T, TDto> : IGetByIdQuery<T, TDto>
    where T : BaseEntity<T>
    where TDto : class
    {
        public IDbContext _dbContext { get; }
        public IMapper _mapper { get; }

        public GetByIdQuery(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TDto> Handle(long id)
        {
            var entity = await _dbContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<TDto>(entity);
        }
    }
}