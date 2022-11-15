using CORE.MVC.SQLServer.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace CORE.MVC.SQLServer.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(SQLServerEntityFrameworkCoreModule),
        typeof(SQLServerApplicationContractsModule)
    )]
    public class SQLServerDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options =>
            {
                options.IsJobExecutionEnabled = false;
            });
        }
    }
}
