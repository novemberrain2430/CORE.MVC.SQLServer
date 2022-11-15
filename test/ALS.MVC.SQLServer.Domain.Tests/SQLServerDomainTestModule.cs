using CORE.MVC.SQLServer.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace CORE.MVC.SQLServer
{
    [DependsOn(
        typeof(SQLServerEntityFrameworkCoreTestModule)
        )]
    public class SQLServerDomainTestModule : AbpModule
    {

    }
}