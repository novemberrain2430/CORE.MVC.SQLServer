using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Modularity;
using Volo.CmsKit.Newsletters;

namespace Volo.CmsKit.EntityFrameworkCore
{
    [DependsOn(
        typeof(CmsKitProDomainModule),
        typeof(CmsKitEntityFrameworkCoreModule)
    )]
    public class CmsKitProEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<CmsKitProDbContext>(options =>
            {
                options.AddRepository<NewsletterRecord, EfCoreNewsletterRecordRepository>();
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            LicenseChecker.Check<CmsKitProEntityFrameworkCoreModule>(context);
        }
    }
}