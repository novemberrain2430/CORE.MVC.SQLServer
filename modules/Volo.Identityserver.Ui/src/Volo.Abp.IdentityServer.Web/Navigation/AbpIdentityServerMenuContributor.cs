using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using Volo.Abp.IdentityServer.Localization;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Authorization.Permissions;

namespace Volo.Abp.IdentityServer.Web.Navigation
{
    public class AbpIdentityServerMenuContributor : IMenuContributor
    {
        public virtual async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        protected virtual Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<AbpIdentityServerResource>();

            var identityServerMenuItem = new ApplicationMenuItem(
                AbpIdentityServerMenuNames.GroupName,
                l["Menu:IdentityServer"],
                icon: "fa fa-server"
            );

            context.Menu.GetAdministration().AddItem(identityServerMenuItem);

            identityServerMenuItem.AddItem(new ApplicationMenuItem(AbpIdentityServerMenuNames.Clients, l["Menu:Clients"], "~/IdentityServer/Clients").RequirePermissions(AbpIdentityServerPermissions.Client.Default));
            identityServerMenuItem.AddItem(new ApplicationMenuItem(AbpIdentityServerMenuNames.IdentityResources, l["Menu:IdentityResources"], "~/IdentityServer/IdentityResources").RequirePermissions(AbpIdentityServerPermissions.IdentityResource.Default));
            identityServerMenuItem.AddItem(new ApplicationMenuItem(AbpIdentityServerMenuNames.ApiResources, l["Menu:ApiResources"], "~/IdentityServer/ApiResources").RequirePermissions( AbpIdentityServerPermissions.ApiResource.Default));
            identityServerMenuItem.AddItem(new ApplicationMenuItem(AbpIdentityServerMenuNames.ApiScopes, l["Menu:ApiScopes"], "~/IdentityServer/ApiScopes").RequirePermissions(AbpIdentityServerPermissions.ApiScope.Default));

            return Task.CompletedTask;
        }
    }
}
