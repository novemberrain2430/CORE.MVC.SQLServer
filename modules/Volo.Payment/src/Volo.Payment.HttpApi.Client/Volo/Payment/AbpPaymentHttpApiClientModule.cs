using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace Volo.Payment
{
    [DependsOn(
        typeof(AbpHttpClientModule),
        typeof(AbpPaymentApplicationContractsModule)
        )]
    public class AbpPaymentHttpApiClientModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(AbpPaymentApplicationContractsModule).Assembly,
                AbpPaymentCommonRemoteServiceConsts.RemoteServiceName
            );
        }
    }
}
