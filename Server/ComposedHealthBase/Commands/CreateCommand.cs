
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Mappers;
namespace ComposedHealthBase.Server.Commands
{
    public interface ICreateCommand<T, TDto>
    {
        Task<long> Handle(TDto dto);
    }

    public class CreateCommand<T, TDto> : ICreateCommand<T, TDto>
    where T : BaseEntity<T>
    where TDto : BaseDto<TDto>
    {
        private IDbContext _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }

        public CreateCommand(IDbContext dbContext, IMapper<T, TDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<long> Handle(TDto dto)
        {
            var newEntity = _mapper.Map(dto);
            _dbContext.Set<T>().Add(newEntity);
            await _dbContext.SaveChangesAsync();
            return newEntity.Id;
        }
    }
}