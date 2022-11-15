using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace Sample
{
    [DependsOn(
        typeof(SampleHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class SampleConsoleApiClientModule : AbpModule
    {
        
    }
}
