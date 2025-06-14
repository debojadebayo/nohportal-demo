// filepath: Modules/Billing/Infrastructure/Database/BillingDbContext.cs
using Microsoft.EntityFrameworkCore;
using Server.Modules.Billing.Entities;
using ComposedHealthBase.Server.Database;

namespace Server.Modules.Billing.Infrastructure.Database;

public sealed class BillingDbContext(DbContextOptions<BillingDbContext> options) : BaseDbContext<BillingDbContext>(options), IDbContext<BillingDbContext>
{
    public DbSet<ExampleEntity> ExampleEntities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(BillingSchema.Name);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillingDbContext).Assembly);
    }
}
