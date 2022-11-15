using Microsoft.EntityFrameworkCore;

namespace CORE.MVC.SQLServer.EntityFrameworkCore
{
    public class SQLServerTenantDbContextFactory :
        SQLServerDbContextFactoryBase<SQLServerTenantDbContext>
    {
        protected override SQLServerTenantDbContext CreateDbContext(
            DbContextOptions<SQLServerTenantDbContext> dbContextOptions)
        {
            return new SQLServerTenantDbContext(dbContextOptions);
        }
    }
}
