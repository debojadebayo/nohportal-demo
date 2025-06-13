using System.Security.Claims;
using ComposedHealthBase.Server.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComposedHealthBase.Server.Database
{
	public class BaseDbContext<TContext> : DbContext, IDbContext<TContext>
	where TContext : DbContext
	{
		public BaseDbContext(DbContextOptions<TContext> options) : base(options) { }

		public Task<int> SaveChangesWithAuditAsync(ClaimsPrincipal user, CancellationToken cancellationToken = default)
		{
			if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
			{
				throw new UnauthorizedAccessException("User must be authenticated to save changes.");
			}
			var userFullName = user.FindFirst(ClaimTypes.Name)?.Value ?? "Unknown User";
			var userKeycloakId = user.FindFirst("keycloak_id")?.Value;
			Guid.TryParse(userKeycloakId, out Guid userKeycloakGuid);


			var timeNow = DateTime.UtcNow;
			var entries = ChangeTracker.Entries()
				.Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
				.ToList();

			foreach (var entry in entries)
			{
				if (entry.Entity is IAuditEntity entity)
				{
					if (entry.State == EntityState.Added)
					{
						entity.CreatedBy = userFullName;
						entity.CreatedDate = timeNow;
						entity.ModifiedDate = timeNow;
						entity.CreatedByKeycloakId = userKeycloakGuid;
						entity.ModifiedByKeycloakId = userKeycloakGuid;
					}
					else if (entry.State == EntityState.Modified)
					{
						entity.LastModifiedBy = userFullName;
						entity.ModifiedDate = timeNow;
						entity.ModifiedByKeycloakId = userKeycloakGuid;
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