using Volo.Abp.Modularity;

namespace CORE.MVC.SQLServer
{
    [DependsOn(
        typeof(SQLServerApplicationModule),
        typeof(SQLServerDomainTestModule)
        )]
    public class SQLServerApplicationTestModule : AbpModule
    {

    }
}