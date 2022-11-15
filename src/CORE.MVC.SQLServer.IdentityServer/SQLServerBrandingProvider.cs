using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace CORE.MVC.SQLServer
{
    [Dependency(ReplaceServices = true)]
    public class SQLServerBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "SQLServer";
    }
}
