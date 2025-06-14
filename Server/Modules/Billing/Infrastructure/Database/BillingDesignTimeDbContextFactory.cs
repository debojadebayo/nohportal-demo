// filepath: Modules/Billing/Infrastructure/Database/BillingDesignTimeDbContextFactory.cs
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules.Billing.Infrastructure.Database;

public class BillingDesignTimeDbContextFactory : DesignTimeDbContextFactory<BillingDbContext>
{
}
