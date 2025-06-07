
using ComposedHealthBase.Server.Entities;
using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Shared.DTOs;
using ComposedHealthBase.Server.Mappers;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ComposedHealthBase.Server.Commands
{
    public interface IUploadCommand<T, TDto, TContext>
    {
        Task<long> Handle(TDto dto, ClaimsPrincipal user);
    }

    public class UploadCommand<T, TDto, TContext> : IUploadCommand<T, TDto, TContext>
    where T : BaseEntity<T>
    where TDto : IDto
    where TContext : IDbContext<TContext>
    {
        private IDbContext<TContext> _dbContext { get; }
        private IMapper<T, TDto> _mapper { get; }

        public UploadCommand(IDbContext<TContext> dbContext, IMapper<T, TDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<long> Handle(TDto dto, ClaimsPrincipal user)
        {
            var newEntity = _mapper.Map(dto);
            _dbContext.Set<T>().Add(newEntity);
            await _dbContext.SaveChangesWithAuditAsync(user);
            return newEntity.Id;
        }
    }
}