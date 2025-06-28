using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using Shared.DTOs.Auth;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using ComposedHealthBase.Server.Interfaces;

namespace Server.Modules.Auth.Infrastructure.Queries
{
    public interface IGetAllPermissionsQuery : IQuery
    {
        Task<IEnumerable<PermissionDto>> Handle();
    }

    public class GetAllPermissionsQuery : IGetAllPermissionsQuery
    {
        private readonly AuthDbContext _dbContext;
        private readonly IMapper<Permission, PermissionDto> _mapper;

        public GetAllPermissionsQuery(AuthDbContext dbContext, IMapper<Permission, PermissionDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PermissionDto>> Handle()
        {
            var permissions = await _dbContext.Set<Permission>().ToListAsync();
            return permissions.Select(_mapper.Map);
        }
    }
}
