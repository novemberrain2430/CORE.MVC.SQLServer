using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace Sample
{
    [DependsOn(
        typeof(SampleDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationAbstractionsModule)
        )]
    public class SampleApplicationContractsModule : AbpModule
    {

    }
}
