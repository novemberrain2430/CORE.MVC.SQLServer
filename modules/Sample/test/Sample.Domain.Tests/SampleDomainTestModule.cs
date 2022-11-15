using Sample.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Sample
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(SampleEntityFrameworkCoreTestModule)
        )]
    public class SampleDomainTestModule : AbpModule
    {
        
    }
}
