using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;

namespace CORE.MVC.SQLServer.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class SQLServerTenantDbContext : SQLServerDbContextBase<SQLServerTenantDbContext>
    {
        public SQLServerTenantDbContext(DbContextOptions<SQLServerTenantDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SetMultiTenancySide(MultiTenancySides.Tenant);

            base.OnModelCreating(builder);
        }
    }
}
