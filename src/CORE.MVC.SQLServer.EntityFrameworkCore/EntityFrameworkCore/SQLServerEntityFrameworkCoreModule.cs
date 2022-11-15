using CORE.MVC.SQLServer.Samples;
using CORE.MVC.SQLServer.Xamples;
using CORE.MVC.SQLServer.Samples;
using CORE.MVC.SQLServer.Samples;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TextTemplateManagement.EntityFrameworkCore;
using Volo.Saas.EntityFrameworkCore;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using Volo.CmsKit.EntityFrameworkCore;
using Volo.Blogging.EntityFrameworkCore;
using Volo.Docs.EntityFrameworkCore;
using Volo.Payment.EntityFrameworkCore;
using Volo.FileManagement.EntityFrameworkCore;
using Volo.Forms.EntityFrameworkCore;
using Volo.Chat.EntityFrameworkCore;
using Sample.EntityFrameworkCore;
using CORE.MVC.SQLServer.Books;

namespace CORE.MVC.SQLServer.EntityFrameworkCore
{
    [DependsOn(
        typeof(SQLServerDomainModule),
        typeof(AbpIdentityProEntityFrameworkCoreModule),
        typeof(AbpIdentityServerEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpFeatureManagementEntityFrameworkCoreModule),
        typeof(LanguageManagementEntityFrameworkCoreModule),
        typeof(SaasEntityFrameworkCoreModule),
        typeof(TextTemplateManagementEntityFrameworkCoreModule),
        typeof(CmsKitProEntityFrameworkCoreModule),
        typeof(BlobStoringDatabaseEntityFrameworkCoreModule)
    )]
    [DependsOn(typeof(BloggingEntityFrameworkCoreModule))]
    [DependsOn(typeof(DocsEntityFrameworkCoreModule))]
    [DependsOn(typeof(AbpPaymentEntityFrameworkCoreModule))]
    [DependsOn(typeof(FileManagementEntityFrameworkCoreModule))]
    [DependsOn(typeof(FormsEntityFrameworkCoreModule))]
    [DependsOn(typeof(ChatEntityFrameworkCoreModule))]
    //[DependsOn(typeof(SampleEntityFrameworkCoreModule))]
    public class SQLServerEntityFrameworkCoreModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            SQLServerEfCoreEntityExtensionMappings.Configure();
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SQLServerDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
                //options.AddRepository<Sample, Samples.EfCoreSampleRepository>();

                // options.AddRepository<Sample, Samples.EfCoreSampleRepository>();

                options.AddRepository<Xample, Xamples.EfCoreXampleRepository>();
                options.AddRepository<Book, Books.EfCoreBookRepository>();

                //  options.AddRepository<Sample, Samples.EfCoreSampleRepository>();

            });

            context.Services.AddAbpDbContext<SQLServerTenantDbContext>(options =>
            {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
                options.AddDefaultRepositories(includeAllEntities: true);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                /* The main point to change your DBMS.
                 * See also SQLServerDbContextFactoryBase for EF Core tooling. */
                options.UseSqlServer();
            });
        }
    }
}