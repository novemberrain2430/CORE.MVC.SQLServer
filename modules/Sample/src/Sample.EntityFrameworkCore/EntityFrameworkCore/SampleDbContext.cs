using Sample.Books;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Sample.EntityFrameworkCore
{
    [ConnectionStringName(SampleDbProperties.ConnectionStringName)]
    public class SampleDbContext : AbpDbContext<SampleDbContext>, ISampleDbContext
    {
        public DbSet<Book> Books { get; set; }
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */

        public SampleDbContext(DbContextOptions<SampleDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureSample();
        }
    }
}