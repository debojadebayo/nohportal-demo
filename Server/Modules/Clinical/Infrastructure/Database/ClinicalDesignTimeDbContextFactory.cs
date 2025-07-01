// filepath: Modules/Clinical/Infrastructure/Database/ClinicalDesignTimeDbContextFactory.cs
using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules.Clinical.Infrastructure.Database;

public class ClinicalDesignTimeDbContextFactory : DesignTimeDbContextFactory<ClinicalDbContext>
{
}
