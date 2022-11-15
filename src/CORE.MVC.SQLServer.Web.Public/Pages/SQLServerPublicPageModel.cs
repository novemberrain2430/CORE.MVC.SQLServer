using CORE.MVC.SQLServer.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace CORE.MVC.SQLServer.Web.Public.Pages
{
    /* Inherit your Page Model classes from this class.
     */
    public abstract class SQLServerPublicPageModel : AbpPageModel
    {
        protected SQLServerPublicPageModel()
        {
            LocalizationResourceType = typeof(SQLServerResource);
        }
    }
}
