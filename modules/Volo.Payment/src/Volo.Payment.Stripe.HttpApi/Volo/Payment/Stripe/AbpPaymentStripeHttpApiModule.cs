using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Volo.Payment.Stripe
{

    [DependsOn(
        typeof(AbpPaymentStripeDomainModule),
        typeof(AbpPaymentHttpApiModule))]
    public class AbpPaymentStripeHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(AbpPaymentHttpApiModule).Assembly);
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            LicenseChecker.Check<AbpPaymentHttpApiModule>(context);
        }
    }
}
