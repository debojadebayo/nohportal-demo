
using ComposedHealthBase.Server.BaseModule.Entities;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using Shared.DTOs;
using NationOH.Server.Base.Infrastructure.Mappers;
namespace ComposedHealthBase.Server.BaseModule.Infrastructure.Commands
{
    public interface ICreateCommand<T, TDto>
    {
        Task<long> Handle(TDto dto);
    }

    public class CreateCommand<T, TDto> : ICreateCommand<T, TDto>
    where T : BaseEntity<T>
    where TDto : BaseDto<TDto>
    {
        public IDbContext _dbContext { get; }
        public IMapper<T, TDto> _mapper { get; }

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