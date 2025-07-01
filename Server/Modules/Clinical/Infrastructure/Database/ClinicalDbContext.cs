// filepath: Modules/Clinical/Infrastructure/Database/ClinicalDbContext.cs
using Microsoft.EntityFrameworkCore;
using Server.Modules.Clinical.Entities;
using ComposedHealthBase.Server.Database;

namespace Server.Modules.Clinical.Infrastructure.Database;

public sealed class ClinicalDbContext(DbContextOptions<ClinicalDbContext> options) : BaseDbContext<ClinicalDbContext>(options), IDbContext<ClinicalDbContext>
{
    public DbSet<CaseNote> CaseNotes { get; set; }
    public DbSet<ClinicalReport> ClinicalReports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(ClinicalSchema.Name);

        modelBuilder.Entity<CaseNote>()
            .Property(p => p.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder.Entity<ClinicalReport>()
        .Property(p => p.Id)
        .HasDefaultValueSql("gen_random_uuid()");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClinicalDbContext).Assembly);
    }
}
