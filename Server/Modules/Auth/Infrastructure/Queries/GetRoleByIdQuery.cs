using Microsoft.EntityFrameworkCore;
using ComposedHealthBase.Server.Mappers;
using Shared.DTOs.Auth;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using ComposedHealthBase.Server.Interfaces;

namespace Server.Modules.Auth.Infrastructure.Queries
{
    public interface IGetRoleByIdQuery : IQuery
    {
        Task<RoleDto?> Handle(Guid id);
    }

    public class GetRoleByIdQuery : IGetRoleByIdQuery
    {
        private readonly AuthDbContext _dbContext;
        private readonly IMapper<Role, RoleDto> _mapper;

        public GetRoleByIdQuery(AuthDbContext dbContext, IMapper<Role, RoleDto> mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<RoleDto?> Handle(Guid id)
        {
            var role = await _dbContext.Set<Role>()
                .Include(r => r.Permissions)
                .FirstOrDefaultAsync(r => r.Id == id);

            return role != null ? _mapper.Map(role) : null;
        }
    }
}
