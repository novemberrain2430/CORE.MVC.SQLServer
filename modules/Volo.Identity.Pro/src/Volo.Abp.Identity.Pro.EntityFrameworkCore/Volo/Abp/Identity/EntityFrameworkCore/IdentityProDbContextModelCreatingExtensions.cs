using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Volo.Abp.Identity.EntityFrameworkCore
{
    public static class IdentityProDbContextModelCreatingExtensions
    {
        public static void ConfigureIdentityPro(
            this ModelBuilder builder,
            Action<IdentityProModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new IdentityProModelBuilderConfigurationOptions(
                AbpIdentityDbProperties.DbTablePrefix,
                AbpIdentityDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            builder.ConfigureIdentity(configurationOptions =>
            {
                configurationOptions.TablePrefix = options.TablePrefix;
                configurationOptions.Schema = options.Schema;
            });

            builder.TryConfigureObjectExtensions<IdentityProDbContext>();
        }
    }
}
