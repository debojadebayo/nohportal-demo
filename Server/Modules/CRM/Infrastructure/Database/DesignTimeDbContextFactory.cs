using ComposedHealthBase.Server.Database;
using Microsoft.EntityFrameworkCore;

namespace Server.Modules.CRM.Infrastructure.Database
{
    public class DesignTimeCRMDbContextFactory : DesignTimeDbContextFactory<CRMDbContext>
    {
    }
}