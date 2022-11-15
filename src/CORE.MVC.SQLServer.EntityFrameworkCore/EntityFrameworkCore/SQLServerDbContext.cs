using CORE.MVC.SQLServer.Xamples;
using CORE.MVC.SQLServer.Samples;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Identity;
using Volo.Abp.Users.EntityFrameworkCore;
using CORE.MVC.SQLServer.Books;
using CORE.MVC.SQLServer.Authors;

namespace CORE.MVC.SQLServer.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class SQLServerDbContext : SQLServerDbContextBase<SQLServerDbContext>
    {
        public DbSet<Xample> Xamples { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public SQLServerDbContext(DbContextOptions<SQLServerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.SetMultiTenancySide(MultiTenancySides.Both);

            base.OnModelCreating(builder);

            /* Configure your own tables/entities inside the ConfigureAOMS method */

            //builder.ConfigureAOMS();

            builder.Entity<Xample>(b =>
    {
        b.ToTable(SQLServerConsts.DbTablePrefix + "Xamples", SQLServerConsts.DbSchema);
        b.ConfigureByConvention();
        b.Property(x => x.TenantId).HasColumnName(nameof(Xample.TenantId));
        b.Property(x => x.Name).HasColumnName(nameof(Xample.Name)).IsRequired();
        b.Property(x => x.Date1).HasColumnName(nameof(Xample.Date1)).IsRequired();
        b.Property(x => x.Year).HasColumnName(nameof(Xample.Year));
        b.Property(x => x.Code).HasColumnName(nameof(Xample.Code)).HasMaxLength(XampleConsts.CodeMaxLength);
        b.Property(x => x.Email).HasColumnName(nameof(Xample.Email)).IsRequired();
        b.Property(x => x.IsConfirm).HasColumnName(nameof(Xample.IsConfirm));
        b.Property(x => x.UserId).HasColumnName(nameof(Xample.UserId)).IsRequired();
    });
            builder.Entity<Book>(b =>
            {
                b.ToTable(SQLServerConsts.DbTablePrefix + "Books",
                    SQLServerConsts.DbSchema);
                b.ConfigureByConvention(); //auto configure for the base class props
                b.Property(x => x.Name).IsRequired().HasMaxLength(128);
                // ADD THE MAPPING FOR THE RELATION
                b.HasOne<Author>().WithMany().HasForeignKey(x => x.AuthorId).IsRequired();
            });
            builder.Entity<Author>(b =>
            {
                b.ToTable(SQLServerConsts.DbTablePrefix + "Authors",
                    SQLServerConsts.DbSchema);

                b.ConfigureByConvention();

                b.Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(AuthorConsts.MaxNameLength);

                b.HasIndex(x => x.Name);
            });

        }
    }
}