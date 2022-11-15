using CORE.MVC.SQLServer.Localization;
using Volo.Abp.Application.Services;

namespace CORE.MVC.SQLServer
{
    /* Inherit your application services from this class.
     */
    public abstract class SQLServerAppService : ApplicationService
    {
        protected SQLServerAppService()
        {
            LocalizationResource = typeof(SQLServerResource);
        }
    }
}
