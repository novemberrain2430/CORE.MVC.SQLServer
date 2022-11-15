using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.GlobalFeatures;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Newsletters;

namespace Volo.CmsKit.EntityFrameworkCore
{
    public static class CmsKitProDbContextModelCreatingExtensions
    {
        public static void ConfigureCmsKitPro(
            this ModelBuilder builder,
            Action<CmsKitProModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new CmsKitProModelBuilderConfigurationOptions(
                CmsKitDbProperties.DbTablePrefix,
                CmsKitDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.ConfigureCmsKit(cmskitOptions =>
            {
                cmskitOptions.TablePrefix = options.TablePrefix;
                cmskitOptions.Schema = options.Schema;
            });

            if (GlobalFeatureManager.Instance.IsEnabled<NewslettersFeature>())
            {
                builder.Entity<NewsletterRecord>(b =>
                {
                    b.ToTable(options.TablePrefix + "NewsletterRecords", options.Schema);

                    b.ConfigureByConvention();

                    b.Property(n => n.EmailAddress).HasMaxLength(NewsletterRecordConst.MaxEmailAddressLength).IsRequired().HasColumnName(nameof(NewsletterRecord.EmailAddress));

                    b.HasIndex(n => new {n.TenantId, n.EmailAddress});

                    b.HasMany(n => n.Preferences).WithOne().HasForeignKey(x => x.NewsletterRecordId).IsRequired();

                    b.ApplyObjectExtensionMappings();
                });

                builder.Entity<NewsletterPreference>(b =>
                {
                    b.ToTable(options.TablePrefix + "NewsletterPreferences", options.Schema);

                    b.ConfigureByConvention();

                    b.Property(n => n.Preference).HasMaxLength(NewsletterPreferenceConst.MaxPreferenceLength).IsRequired().HasColumnName(nameof(NewsletterPreference.Preference));
                    b.Property(n => n.Source).HasMaxLength(NewsletterPreferenceConst.MaxSourceLength).IsRequired().HasColumnName(nameof(NewsletterPreference.Source));
                    b.Property(n => n.SourceUrl).HasMaxLength(NewsletterPreferenceConst.MaxSourceUrlLength).IsRequired().HasColumnName(nameof(NewsletterPreference.SourceUrl));

                    b.HasIndex(n => new {n.TenantId, n.Preference, n.Source});

                    b.ApplyObjectExtensionMappings();
                });
            }

            builder.TryConfigureObjectExtensions<CmsKitProDbContext>();
        }
    }
}
