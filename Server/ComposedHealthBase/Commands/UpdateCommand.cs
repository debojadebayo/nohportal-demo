
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Server.Modules;
using ComposedHealthBase.Shared.DTOs;
namespace ComposedHealthBase.Server.Commands
{
    public interface IUpdateCommand<T, TDto, TContext>
    {
        Task<long> Handle(TDto dto);
    }

    public class UpdateCommand<T, TDto, TContext> : IUpdateCommand<T, TDto, TContext>
    where T : BaseEntity<T>
    where TDto : BaseDto<TDto>
    where TContext : IDbContext<TContext>
    {
        private IDbContext<TContext> _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }

        public UpdateCommand(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
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