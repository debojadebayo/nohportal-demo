using AutoMapper;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using ComposedHealthBase.Server.BaseModule.Entities.DTOs;

namespace ComposedHealthBase.Server.BaseModule.Infrastructure.Queries
{
    interface IGetByIdQuery
    {
        Task<TDto> Handle<TDto, T>(long id) where T : class where TDto : class;
    }

    class GetByIdQuery : IGetByIdQuery
    {
        public IDbContext _dbContext { get; }
        public IMapper _mapper { get; }

        public GetByIdQuery(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<TDto> Handle<TDto, T>(long id) where T : class where TDto : class
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            return _mapper.Map<TDto>(entity);
        }
    }
}