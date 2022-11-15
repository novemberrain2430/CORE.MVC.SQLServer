using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.CmsKit.Newsletters;

namespace Volo.CmsKit.EntityFrameworkCore
{
    [ConnectionStringName(CmsKitDbProperties.ConnectionStringName)]
    public class CmsKitProDbContext : AbpDbContext<CmsKitProDbContext>, ICmsKitProDbContext
    {
        public DbSet<NewsletterRecord> NewsletterRecords { get; set; }
        public DbSet<NewsletterPreference> NewsletterPreferences { get; set; }

        public CmsKitProDbContext(DbContextOptions<CmsKitProDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureCmsKitPro();
        }
    }
}