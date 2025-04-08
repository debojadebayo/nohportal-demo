using AutoMapper;
using Server.Base.Infrastructure.Database;
namespace Server.Base.Infrastructure.Commands
{
    interface ICreateCommand
    {
        Task<long> Handle(IDto dto);
    }

    class CreateCommand<TDto, T> : ICreateCommand
    where T : class
    where TDto : class
    {
        public IDbContext _dbContext { get; }
        public IMapper _mapper { get; }

        public CreateCommand(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<long> Handle(IDto dto)
        {
            var newEntity = _mapper.Map<T>(dto);
            _dbContext.Set<T>().Add(newEntity);
            await _dbContext.SaveChangesAsync();
            return (long)newEntity.GetType().GetProperty("Id").GetValue(newEntity);
        }
    }
}