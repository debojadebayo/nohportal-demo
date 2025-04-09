using Microsoft.EntityFrameworkCore;

namespace ComposedHealthBase.Server.BaseModule.Infrastructure.Database
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}