using AutoMapper;
using ComposedHealthBase.Server.BaseModule.Entities;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using ComposedHealthBase.Shared.DTOs;
namespace ComposedHealthBase.Server.BaseModule.Infrastructure.Commands
{
    interface IUpdateCommand<T, TDto>
    {
        Task<long> Handle(TDto dto);
    }

    class UpdateCommand<T, TDto> : IUpdateCommand<T, TDto>
    where T : BaseEntity<T>
    where TDto : BaseDto<TDto>
    {
        public IDbContext _dbContext { get; }
        public IMapper _mapper { get; }

        public UpdateCommand(IDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<long> Handle(TDto dto)
        {
            var existingEntity = await _dbContext.Set<T>().FindAsync(dto.Id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with id {dto.Id} not found.");
            }
            _mapper.Map(dto, existingEntity);
            _dbContext.Set<T>().Update(existingEntity);
            await _dbContext.SaveChangesAsync();
            return existingEntity.Id;
        }
    }
}