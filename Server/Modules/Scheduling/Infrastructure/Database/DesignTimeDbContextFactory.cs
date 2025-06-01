using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules.Scheduling.Infrastructure.Database
{
    public class DesignTimeSchedulingDbContextFactory : DesignTimeDbContextFactory<SchedulingDbContext>
    {
    }
}