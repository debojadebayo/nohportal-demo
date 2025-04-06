using ComposedHealthBase.Server.BaseModule.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComposedHealthBase.Server.BaseModule.Infrastructure.Database
{
	public class BaseDbContext<TContext> : DbContext where TContext : DbContext
	{
		public BaseDbContext(DbContextOptions<TContext> options) : base(options) { }
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			var timeNow = DateTime.UtcNow;
			var createdEntities = ChangeTracker.Entries<IBaseEntity>().Where(e => e.State == EntityState.Added).ToList();
			var modifiedEntities = ChangeTracker.Entries<IBaseEntity>().Where(e => e.State == EntityState.Modified).ToList();
			foreach (var createdEntity in createdEntities)
			{
				createdEntity.Entity.CreatedDate = timeNow;
				createdEntity.Entity.ModifiedDate = timeNow;
			}
			foreach (var modifiedEntity in modifiedEntities)
			{
				modifiedEntity.Entity.ModifiedDate = timeNow;
			}
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
	}
}