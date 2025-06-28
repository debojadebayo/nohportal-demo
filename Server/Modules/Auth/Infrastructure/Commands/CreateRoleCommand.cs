using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Interfaces;
using ComposedHealthBase.Server.Mappers;
using ComposedHealthBase.Server.Services;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using Shared.DTOs.Auth;
using System.Security.Claims;

namespace Server.Modules.Auth.Infrastructure.Commands
{
    public interface ICreateRoleCommand : ICommand
    {
        Task<RoleDto> Handle(RoleDto roleDto, ClaimsPrincipal user);
    }

    public class CreateRoleCommand : ICreateRoleCommand
    {
        private readonly IDbContext<AuthDbContext> _dbContext;
        private readonly IMapper<Role, RoleDto> _mapper;
        private readonly IRolePermissionCacheService _rolePermissionCacheService;

        public CreateRoleCommand(IDbContext<AuthDbContext> dbContext, IMapper<Role, RoleDto> mapper, IRolePermissionCacheService rolePermissionCacheService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _rolePermissionCacheService = rolePermissionCacheService;
        }

        public async Task<RoleDto> Handle(RoleDto roleDto, ClaimsPrincipal user)
        {
            var entity = _mapper.Map(roleDto);
            _dbContext.Set<Role>().Add(entity);
            await _dbContext.SaveChangesAsync();
            
            // Refresh the role permission cache after successful creation
            await _rolePermissionCacheService.InitAsync();
            
            return _mapper.Map(entity);
        }
    }
}
