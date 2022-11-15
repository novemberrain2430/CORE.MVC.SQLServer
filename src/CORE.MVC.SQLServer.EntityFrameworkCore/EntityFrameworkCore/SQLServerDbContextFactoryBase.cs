using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CORE.MVC.SQLServer.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public abstract class SQLServerDbContextFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
        where TDbContext : DbContext
    {
        public TDbContext CreateDbContext(string[] args)
        {
            SQLServerEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return CreateDbContext(builder.Options);
        }

        protected abstract TDbContext CreateDbContext(DbContextOptions<TDbContext> dbContextOptions);

        protected IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CORE.MVC.SQLServer.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
