using AutoMapper;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;

namespace Server.Base.Infrastructure.Commands
{
    interface IUpdateCommand
    {
        Task<long> Handle(IDto dto);
    }
    class UpdateCommand<TDto, T> where T : class where TDto : class
    {
        private readonly IDbContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateCommand(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(long id, IDto dto)
        {
            var existingEntity = await _dbContext.Set<T>().FindAsync(id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found.");
            }

            _mapper.Map(dto, existingEntity);
            _dbContext.Set<T>().Update(existingEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
