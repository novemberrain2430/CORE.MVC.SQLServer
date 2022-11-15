using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Volo.Payment
{
    [DependsOn(
        typeof(AbpPaymentDomainSharedModule),
        typeof(AbpDddApplicationContractsModule))]
    public class AbpPaymentApplicationContractsModule : AbpModule
    {
        
    }
}
