using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using Shared.DTOs.Auth;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using ComposedHealthBase.Server.Interfaces;
using ComposedHealthBase.Server.Queries.ModuleQueries;

namespace Server.Modules.Auth.Infrastructure.Queries
{
    public interface IGetAllRolesQuery : IQuery
    {
        Task<IEnumerable<RoleDto>> Handle();
    }

    public class GetAllRolesQuery : IGetAllRolesQuery, IGetAllRolesWithPermissionsQuery
    {
        private readonly AuthDbContext _dbContext;
        private readonly IMapper<Role, RoleDto> _mapper;

        public GetAllRolesQuery(AuthDbContext dbContext, IMapper<Role, RoleDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RoleDto>> Handle()
        {
            var roles = await _dbContext.Set<Role>()
                .Include(r => r.Permissions)
                .ToListAsync();
            
            return roles.Select(_mapper.Map);
        }
    }
}
