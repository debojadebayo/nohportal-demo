using AutoMapper;
using ComposedHealthBase.Server.BaseModule.Entities;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
namespace ComposedHealthBase.Server.BaseModule.Infrastructure.Commands
{
    interface ICreateCommand<T, TDto>
    {
        Task<long> Handle(TDto dto);
    }

    class CreateCommand<T, TDto> : ICreateCommand<T, TDto>
    where T : BaseEntity<T>
    where TDto : class
    {
        public IDbContext _dbContext { get; }
        public IMapper _mapper { get; }

        public CreateCommand(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<long> Handle(TDto dto)
        {
            var newEntity = _mapper.Map<T>(dto);
            _dbContext.Set<T>().Add(newEntity);
            await _dbContext.SaveChangesAsync();
            return newEntity.Id;
        }
    }
}