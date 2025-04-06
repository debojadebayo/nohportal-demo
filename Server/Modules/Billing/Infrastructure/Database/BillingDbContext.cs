using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Server.Modules.Billing.Entities;

namespace Server.Modules.Billing.Infrastructure.Database
{
	public sealed class BillingDbContext(DbContextOptions<BillingDbContext> options) : BaseDbContext<BillingDbContext>(options)
	{
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<InvoiceItem> InvoiceItems { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema.Billing);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(BillingDbContext).Assembly);
		}
	}
}