using ComposedHealthBase.Server.BaseModule.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Server.Modules.CRM.Entities;

namespace Server.Modules.CRM.Infrastructure.Database
{
	public sealed class CRMDbContext(DbContextOptions<CRMDbContext> options) : BaseDbContext<CRMDbContext>(options), IDbContext
	{
		public DbSet<Contract> Contracts { get; set; }
		public DbSet<NOHCustomer> NOHCustomers { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema.CRM);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CRMDbContext).Assembly);
		}
	}
}