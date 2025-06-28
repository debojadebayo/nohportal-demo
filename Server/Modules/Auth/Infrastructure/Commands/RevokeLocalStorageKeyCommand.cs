using ComposedHealthBase.Server.Database;
using ComposedHealthBase.Server.Interfaces;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Auth.Entities;
using Server.Modules.Auth.Infrastructure.Database;
using System.Security.Claims;

namespace Server.Modules.Auth.Infrastructure.Commands
{
    public interface IRevokeLocalStorageKeyCommand : ICommand
    {
        Task Handle(Guid id, ClaimsPrincipal user);
    }

    public class RevokeLocalStorageKeyCommand : IRevokeLocalStorageKeyCommand
    {
        private readonly IDbContext<AuthDbContext> _dbContext;

        public RevokeLocalStorageKeyCommand(IDbContext<AuthDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(Guid id, ClaimsPrincipal user)
        {
            var key = await _dbContext.Set<LocalStorageKey>()
                .FirstOrDefaultAsync(k => k.Id == id);

            if (key == null)
            {
                throw new KeyNotFoundException($"LocalStorageKey with ID {id} not found.");
            }

            // Mark the key as inactive instead of deleting it
            key.IsActive = false;
            key.KeyExpiryDate = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
        }
    }
}
