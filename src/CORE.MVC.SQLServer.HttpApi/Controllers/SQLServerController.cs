using CORE.MVC.SQLServer.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace CORE.MVC.SQLServer.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class SQLServerController : AbpController
    {
        protected SQLServerController()
        {
            LocalizationResource = typeof(SQLServerResource);
        }
    }
}