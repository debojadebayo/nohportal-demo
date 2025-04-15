using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;
using Server.Modules.CRM.Entities;

namespace Server.Modules.CRM.Infrastructure.Database
{
	public sealed class CRMDbContext(DbContextOptions<CRMDbContext> options) : BaseDbContext<CRMDbContext>(options), IDbContext<CRMDbContext>
	{
		public DbSet<Contract> Contracts { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductType> ProductTypes { get; set; }
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Customer> Customers { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.HasDefaultSchema(Schema.CRM);
			modelBuilder.ApplyConfigurationsFromAssembly(typeof(CRMDbContext).Assembly);
		}
	}
}