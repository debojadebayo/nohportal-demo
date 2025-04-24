
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Shared.DTOs;
namespace ComposedHealthBase.Server.Commands
{
    public interface ICreateCommand<T, TDto, TContext>
    {
        Task<long> Handle(TDto dto);
    }

    public class CreateCommand<T, TDto, TContext> : ICreateCommand<T, TDto, TContext>
    where T : BaseEntity<T>
    where TDto : IDto
    where TContext : IDbContext<TContext>
    {
        private IDbContext<TContext> _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }

        public CreateCommand(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
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