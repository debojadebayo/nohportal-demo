using ComposedHealthBase.Server.Interfaces;
using ComposedHealthBase.Server.Mappers;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using Shared.DTOs.Auth;

namespace Server.Modules.Auth.Infrastructure.Queries
{
    public interface IGetLocalStorageKeyByIdQuery : IQuery
    {
        Task<LocalStorageKeyDto?> Handle(Guid id);
    }

    public class GetLocalStorageKeyByIdQuery : IGetLocalStorageKeyByIdQuery
    {
        private readonly AuthDbContext _dbContext;
        private readonly IMapper<LocalStorageKey, LocalStorageKeyDto> _mapper;

        public GetLocalStorageKeyByIdQuery(AuthDbContext dbContext, IMapper<LocalStorageKey, LocalStorageKeyDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<LocalStorageKeyDto?> Handle(Guid id)
        {
            var key = await _dbContext.Set<LocalStorageKey>()
                .FirstOrDefaultAsync(k => k.Id == id);

            return key != null ? _mapper.Map(key) : null;
        }
    }
}
