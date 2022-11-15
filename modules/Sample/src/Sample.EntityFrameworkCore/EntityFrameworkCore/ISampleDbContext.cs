using Sample.Books;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Sample.EntityFrameworkCore
{
    [ConnectionStringName(SampleDbProperties.ConnectionStringName)]
    public interface ISampleDbContext : IEfCoreDbContext
    {
        DbSet<Book> Books { get; set; }
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}