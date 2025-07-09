using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Interfaces;
using ComposedHealthBase.Server.Services;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using System.Security.Claims;

namespace Server.Modules.Auth.Infrastructure.Commands
{
    public interface IDeleteRoleCommand : ICommand
    {
        Task Handle(Guid id, ClaimsPrincipal user);
    }

    public class DeleteRoleCommand : IDeleteRoleCommand
    {
        private readonly IDbContext<AuthDbContext> _dbContext;
        private readonly IRolePermissionCacheService _rolePermissionCacheService;

        public DeleteRoleCommand(IDbContext<AuthDbContext> dbContext, IRolePermissionCacheService rolePermissionCacheService)
        {
            _dbContext = dbContext;
            _rolePermissionCacheService = rolePermissionCacheService;
        }

        public async Task Handle(Guid id, ClaimsPrincipal user)
        {
            var role = await _dbContext.Set<Role>().FindAsync(id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }

            _dbContext.Set<Role>().Remove(role);
            await _dbContext.SaveChangesAsync();

            // Refresh the role permission cache after successful deletion
            await _rolePermissionCacheService.InitAsync();
        }
    }
}
