using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace CORE.MVC.SQLServer.Web
{
    [Dependency(ReplaceServices = true)]
    public class SQLServerBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "SQLServer";
    }
}
