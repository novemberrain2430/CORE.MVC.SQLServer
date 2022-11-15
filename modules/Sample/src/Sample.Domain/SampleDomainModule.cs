using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace Sample
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(SampleDomainSharedModule)
    )]
    public class SampleDomainModule : AbpModule
    {

    }
}
