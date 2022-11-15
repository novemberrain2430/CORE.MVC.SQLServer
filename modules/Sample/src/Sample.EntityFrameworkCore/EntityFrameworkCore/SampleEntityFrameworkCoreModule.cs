using Sample.Books;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Sample.EntityFrameworkCore
{
    [DependsOn(
        typeof(SampleDomainModule),
        typeof(AbpEntityFrameworkCoreModule)
    )]
    public class SampleEntityFrameworkCoreModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<SampleDbContext>(options =>
            {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
                options.AddRepository<Book, Books.EfCoreBookRepository>();

            });
        }
    }
}