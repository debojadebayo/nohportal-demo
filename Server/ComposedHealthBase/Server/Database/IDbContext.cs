using Microsoft.EntityFrameworkCore;

namespace ComposedHealthBase.Server.Database
{
    public interface IDbContext<TContext>
    {
        DbSet<T> Set<T>() where T : class;

        int SaveChanges();
        Task<int> SaveChangesWithAuditAsync(string userFullName, CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}