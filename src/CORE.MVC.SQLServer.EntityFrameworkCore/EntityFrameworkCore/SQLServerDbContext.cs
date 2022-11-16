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
using CORE.MVC.SQLServer.Shifts;
using CORE.MVC.SQLServer.Absents;
using CORE.MVC.SQLServer.Holidays;
using CORE.MVC.SQLServer.TinhCongs;
using CORE.MVC.SQLServer.InOuts;
using CORE.MVC.SQLServer.Weekends;
using CORE.MVC.SQLServer.CodeChamCongs;

namespace CORE.MVC.SQLServer.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public class SQLServerDbContext : SQLServerDbContextBase<SQLServerDbContext>
    {
        public DbSet<Xample> Xamples { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Shift> Shifts { set; get; }
        public DbSet<Absent> Absents { set; get; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<TinhCong> TinhCongs { set; get; }
        public DbSet<InOut> InOuts { set; get; }
        public DbSet<Weekend> Weekends { set; get; }
        public DbSet<CodeChamCong> CodeChamCongs { set; get; }

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
            builder.Entity<Shift>(b =>
            {
                b.ToTable(SQLServerConsts.DbTablePrefix+ "_CC_" + "Shifts",
                    SQLServerConsts.DbSchema);

                b.ConfigureByConvention();
                b.HasIndex(x => x.Name);
            });
            builder.Entity<Absent>(b =>
            {
                b.ToTable(SQLServerConsts.DbTablePrefix + "_CC_" + "Absents",
                    SQLServerConsts.DbSchema);

                b.ConfigureByConvention();
                b.HasIndex(x => x.Name);
            });
            builder.Entity<Holiday>(b =>
            {
                b.ToTable(SQLServerConsts.DbTablePrefix + "_CC_" + "Holidays",
                    SQLServerConsts.DbSchema);

                b.ConfigureByConvention();
                b.HasIndex(x => x.Name);
            });
            builder.Entity<TinhCong>(b =>
            {
                b.ToTable(SQLServerConsts.DbTablePrefix + "_CC_" + "TinhCongs",
                    SQLServerConsts.DbSchema);

                b.ConfigureByConvention();
            });
            builder.Entity<InOut>(b =>
            {
                b.ToTable(SQLServerConsts.DbTablePrefix + "_CC_" + "InOuts",
                    SQLServerConsts.DbSchema);

                b.ConfigureByConvention();
            });
            builder.Entity<Weekend>(b =>
            {
                b.ToTable(SQLServerConsts.DbTablePrefix + "_CC_" + "Weekends",
                    SQLServerConsts.DbSchema);

                b.ConfigureByConvention();
            });
            builder.Entity<CodeChamCong>(b =>
            {
                b.ToTable(SQLServerConsts.DbTablePrefix + "_CC_" + "CodeChamCongs",
                    SQLServerConsts.DbSchema);

                b.ConfigureByConvention();
            });

        }
    }
}