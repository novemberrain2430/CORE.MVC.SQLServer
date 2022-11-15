using Sample.Permissions;
using Sample.Localization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace Sample.Web.Menus
{
    public class SampleMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }

            var moduleMenu = AddModuleMenuItem(context);
            await AddMenuItemBooks(context, moduleMenu);
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            //Add main menu items.
            context.Menu.AddItem(new ApplicationMenuItem(SampleMenus.Prefix, displayName: "Sample", "~/Sample", icon: "fa fa-globe"));

            return Task.CompletedTask;
        }

        private static ApplicationMenuItem AddModuleMenuItem(MenuConfigurationContext context)
        {
            var moduleMenu = new ApplicationMenuItem(
                SampleMenus.Prefix,
                context.GetLocalizer<SampleResource>()["Menu:Sample"],
                icon: "fa fa-folder"
            );

            context.Menu.Items.AddIfNotContains(moduleMenu);
            return moduleMenu;
        }
        private static async Task AddMenuItemBooks(MenuConfigurationContext context, ApplicationMenuItem parentMenu)
        {
            parentMenu.AddItem(
                new ApplicationMenuItem(
                    Menus.SampleMenus.Books,
                    context.GetLocalizer<SampleResource>()["Menu:Books"],
                    "/Sample/Books",
                    icon: "fa fa-file-alt",
                    requiredPermissionName: SamplePermissions.Books.Default
                )
            );
        }
    }
}