using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Scheduling.Entities;

namespace Server.Modules.Scheduling.Infrastructure.Database
{
	public sealed class SchedulingDbContext(DbContextOptions<SchedulingDbContext> options) : BaseDbContext<SchedulingDbContext>(options), IDbContext
	{
		public DbSet<Clinician> Clinicians { get; set; }
		public DbSet<Schedule> Schedules { get; set; }
		public DbSet<Referral> Referrals { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema.Schedule);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchedulingDbContext).Assembly);
		}
	}
}