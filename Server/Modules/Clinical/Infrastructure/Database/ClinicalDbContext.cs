using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Clinical.Entities;

namespace Server.Modules.Clinical.Infrastructure.Database
{
	public sealed class ClinicalDbContext(DbContextOptions<ClinicalDbContext> options) : BaseDbContext<ClinicalDbContext>(options)
	{
		public DbSet<Clinician> Clinicians { get; set; }
		public DbSet<Patient> Patients { get; set; }
		public DbSet<CaseReport> CaseReports { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema.Clinical);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicalDbContext).Assembly);
		}
	}
}