using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Localization;
using Volo.CmsKit.Permissions;

namespace Volo.CmsKit.Pro.Admin.Web.Menus
{
    public class CmsKitProAdminMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            await AddCmsMenuAsync(context);
        }

        private Task AddCmsMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<CmsKitResource>();

            var cmsProMenus = new List<ApplicationMenuItem>();

            if (GlobalFeatureManager.Instance.IsEnabled<NewslettersFeature>())
            {
                cmsProMenus.Add(new ApplicationMenuItem(
                        CmsKitProAdminMenus.Newsletters.NewsletterMenu,
                        l["Newsletters"].Value,
                        "/CmsKit/Newsletters"
                    ).RequirePermissions(CmsKitProAdminPermissions.Newsletters.Default)
                );
            }

            if (cmsProMenus.Any())
            {
                var cmsMenu = context.Menu.FindMenuItem(CmsKitProAdminMenus.GroupName);

                if (cmsMenu == null)
                {
                    cmsMenu = new ApplicationMenuItem(
                        CmsKitProAdminMenus.GroupName,
                        l["Cms"],
                        icon: "far fa-newspaper");

                    context.Menu.AddItem(cmsMenu);
                }

                foreach (var cmsProMenu in cmsProMenus)
                {
                    cmsMenu.AddItem(cmsProMenu);
                }
            }

            return Task.CompletedTask;
        }
    }
}
