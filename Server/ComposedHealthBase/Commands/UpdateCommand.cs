
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Server.Modules;
using Shared.DTOs;
namespace ComposedHealthBase.Server.Commands
{
    public interface IUpdateCommand<T, TDto>
    {
        Task<long> Handle(TDto dto);
    }

    public class UpdateCommand<T, TDto> : IUpdateCommand<T, TDto>
    where T : BaseEntity<T>
    where TDto : BaseDto<TDto>
    {
        private IDbContext _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }

        public UpdateCommand(IDbContext dbContext, IMapper<T, TDto> mapper)
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