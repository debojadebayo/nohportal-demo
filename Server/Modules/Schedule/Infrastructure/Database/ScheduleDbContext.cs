using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Schedule.Entities;

namespace Server.Modules.Schedule.Infrastructure.Database
{
	public sealed class ScheduleDbContext(DbContextOptions<ScheduleDbContext> options) : BaseDbContext<ScheduleDbContext>(options)
	{
		public DbSet<Entities.Schedule> Schedules { get; set; }
		public DbSet<Referral> Referrals { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema.Schedule);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ScheduleDbContext).Assembly);
		}
	}
}