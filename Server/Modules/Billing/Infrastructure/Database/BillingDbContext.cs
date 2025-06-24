// filepath: Modules/Billing/Infrastructure/Database/BillingDbContext.cs
using Microsoft.EntityFrameworkCore;
using Server.Modules.Billing.Entities;
using ComposedHealthBase.Server.Database;

namespace Server.Modules.Billing.Infrastructure.Database;

public sealed class BillingDbContext(DbContextOptions<BillingDbContext> options) : BaseDbContext<BillingDbContext>(options), IDbContext<BillingDbContext>
{
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<LineItem> LineItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(BillingSchema.Name);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillingDbContext).Assembly);

        // Configure Invoice entity
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.InvoiceNumber).IsRequired().HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasPrecision(18, 2);
            entity.Property(e => e.NetAmount).HasPrecision(18, 2);
            entity.Property(e => e.TaxAmount).HasPrecision(18, 2);
            entity.Property(e => e.TaxRate).HasPrecision(5, 4);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.HasIndex(e => e.InvoiceNumber).IsUnique();
            entity.HasIndex(e => e.CustomerId);
        });

        // Configure LineItem entity
        modelBuilder.Entity<LineItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.ProductName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.ProductTypeChargeCode).HasMaxLength(50);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
            entity.Property(e => e.LineTotal).HasPrecision(18, 2);
            entity.Property(e => e.EmployeeName).HasMaxLength(200);
            entity.Property(e => e.ClinicianName).HasMaxLength(200);
            
            // Configure relationship
            entity.HasOne(li => li.Invoice)
                  .WithMany(i => i.LineItems)
                  .HasForeignKey(li => li.InvoiceId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
