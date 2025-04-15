using ComposedHealthBase.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComposedHealthBase.Server.Database
{
	public class BaseDbContext<TContext> : DbContext, IDbContext<TContext>
	where TContext : DbContext
	{
		public BaseDbContext(DbContextOptions<TContext> options) : base(options) { }
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			var timeNow = DateTime.UtcNow;
			var createdEntities = ChangeTracker.Entries<IEntity>().Where(e => e.State == EntityState.Added).ToList();
			var modifiedEntities = ChangeTracker.Entries<IEntity>().Where(e => e.State == EntityState.Modified).ToList();
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