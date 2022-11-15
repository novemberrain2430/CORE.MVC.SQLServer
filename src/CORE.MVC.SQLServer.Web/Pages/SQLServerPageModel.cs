using CORE.MVC.SQLServer.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace CORE.MVC.SQLServer.Web.Pages
{
    /* Inherit your Page Model classes from this class.
     */
    public abstract class SQLServerPageModel : AbpPageModel
    {
        protected SQLServerPageModel()
        {
            LocalizationResourceType = typeof(SQLServerResource);
        }
    }
}