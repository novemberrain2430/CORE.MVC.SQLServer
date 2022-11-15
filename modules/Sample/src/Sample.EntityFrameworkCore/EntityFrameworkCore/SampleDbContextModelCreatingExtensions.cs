using Volo.Abp.EntityFrameworkCore.Modeling;
using Sample.Books;
using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;

namespace Sample.EntityFrameworkCore
{
    public static class SampleDbContextModelCreatingExtensions
    {
        public static void ConfigureSample(
            this ModelBuilder builder,
            Action<SampleModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new SampleModelBuilderConfigurationOptions(
                SampleDbProperties.DbTablePrefix,
                SampleDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);

                b.ConfigureByConvention();

                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);

                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */
            if (builder.IsHostDatabase())
            {
                builder.Entity<Book>(b =>
    {
        b.ToTable(SampleDbProperties.DbTablePrefix + "Books", SampleDbProperties.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.Name).HasColumnName(nameof(Book.Name)).IsRequired();
        b.Property(x => x.Code).HasColumnName(nameof(Book.Code));
    });

            }
        }
    }
}