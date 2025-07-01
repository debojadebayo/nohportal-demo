using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ComposedHealthBase.Server.Database
{
    public interface IDbContext<TContext>
    {
        DbSet<T> Set<T>() where T : class;

        int SaveChanges();
        Task<int> SaveChangesWithAuditAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}