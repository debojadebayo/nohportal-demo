using ComposedHealthBase.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComposedHealthBase.Server.Database
{
	public class BaseDbContext<TContext> : DbContext, IDbContext<TContext>
	where TContext : DbContext
	{
		public BaseDbContext(DbContextOptions<TContext> options) : base(options) { }

		public Task<int> SaveChangesWithAuditAsync(string userFullName, CancellationToken cancellationToken = default)
		{
			var timeNow = DateTime.UtcNow;
			var entries = ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
				.ToList();

			foreach (var entry in entries)
			{
				if (entry.Entity is IEntity entity)
				{
					if (entry.State == EntityState.Added)
					{
						entity.CreatedBy = userFullName;
						entity.CreatedDate = timeNow;
						entity.ModifiedDate = timeNow;
					}
					else if (entry.State == EntityState.Modified)
					{
						entity.LastModifiedBy = userFullName;
						entity.ModifiedDate = timeNow;
					}
				}

				var properties = entry.Entity.GetType().GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
					.Where(p => p.CanRead && p.CanWrite &&
						(p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?)));

				foreach (var prop in properties)
				{
					var value = prop.GetValue(entry.Entity);
					if (value is DateTime dt)
					{
						if (dt.Kind != DateTimeKind.Utc && dt != default)
						{
							prop.SetValue(entry.Entity, DateTime.SpecifyKind(dt, DateTimeKind.Utc));
						}
					}
				}
			}
			return SaveChangesAsync(true, cancellationToken);
		}
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{

			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}
		
	}
}