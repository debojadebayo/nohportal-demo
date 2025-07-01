using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Interfaces;
using ComposedHealthBase.Server.Mappers;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using Shared.DTOs.Auth;
using System.Security.Claims;

namespace Server.Modules.Auth.Infrastructure.Queries
{
    public interface IGetLocalStorageKeyQuery : IQuery
    {
        Task<LocalStorageKeyDto?> Handle(string objectTypeName, Guid objectGuid, ClaimsPrincipal user);
    }

    public class GetLocalStorageKeyQuery : IGetLocalStorageKeyQuery
    {
        private readonly IDbContext<AuthDbContext> _dbContext;
        private readonly IMapper<LocalStorageKey, LocalStorageKeyDto> _mapper;

        public GetLocalStorageKeyQuery(IDbContext<AuthDbContext> dbContext, IMapper<LocalStorageKey, LocalStorageKeyDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<LocalStorageKeyDto?> Handle(string objectTypeName, Guid objectGuid, ClaimsPrincipal user)
        {
            var entity = await _dbContext.Set<LocalStorageKey>()
                .AsNoTracking()
                .FirstOrDefaultAsync(k => k.ObjectTypeName == objectTypeName && 
                                         k.ObjectGuid == objectGuid && 
                                         k.IsActive);

            return entity != null ? _mapper.Map(entity) : null;
        }
    }
}
