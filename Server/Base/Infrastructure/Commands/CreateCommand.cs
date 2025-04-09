using AutoMapper;
using ComposedHealthBase.Server.BaseModule.Entities;
using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using ComposedHealthBase.Shared.DTOs;
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