using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
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

namespace CORE.MVC.SQLServer.EntityFrameworkCore
{
    public abstract class SQLServerDbContextBase<TDbContext> : AbpDbContext<TDbContext>
        where TDbContext : DbContext
    {
        public SQLServerDbContextBase(DbContextOptions<TDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            /* Include modules to your migration db context */

            builder.ConfigurePermissionManagement();
            builder.ConfigureSettingManagement();
            builder.ConfigureBackgroundJobs();
            builder.ConfigureAuditLogging();
            builder.ConfigureIdentityPro();
            builder.ConfigureIdentityServer();
            builder.ConfigureFeatureManagement();
            builder.ConfigureLanguageManagement();
            builder.ConfigureSaas();
            builder.ConfigureTextTemplateManagement();
            builder.ConfigureBlobStoring();
            builder.ConfigureCmsKit();
            builder.ConfigureCmsKitPro();

            /* Configure your own tables/entities inside here */

            //builder.Entity<YourEntity>(b =>
            //{
            //    b.ToTable(SQLServerConsts.DbTablePrefix + "YourEntities", SQLServerConsts.DbSchema);
            //    b.ConfigureByConvention(); //auto configure for the base class props
            //    //...
            //});

            //if (builder.IsHostDatabase())
            //{
            //    /* Tip: Configure mappings like that for the entities only available in the host side,
            //     * but should not be in the tenant databases. */
            //}
            builder.ConfigureBlogging();
            builder.ConfigureDocs();
            builder.ConfigureIdentity();
            builder.ConfigurePayment();
            builder.ConfigureFileManagement();
            builder.ConfigureForms();
            builder.ConfigureChat();
            builder.ConfigureSample();
        }
    }
}
