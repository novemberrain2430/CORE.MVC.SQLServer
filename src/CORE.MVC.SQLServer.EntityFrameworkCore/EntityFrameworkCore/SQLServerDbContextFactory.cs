using Microsoft.EntityFrameworkCore;

namespace CORE.MVC.SQLServer.EntityFrameworkCore
{
    public class SQLServerDbContextFactory :
        SQLServerDbContextFactoryBase<SQLServerDbContext>
    {
        protected override SQLServerDbContext CreateDbContext(
            DbContextOptions<SQLServerDbContext> dbContextOptions)
        {
            return new SQLServerDbContext(dbContextOptions);
        }
    }
}
