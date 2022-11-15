using System;
using System.Threading.Tasks;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CORE.MVC.SQLServer.Localization;
using Volo.Abp.Account.Localization;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Users;

namespace CORE.MVC.SQLServer.Web.Public.Menus
{
    public class SQLServerPublicMenuContributor : IMenuContributor
    {
        private readonly IConfiguration _configuration;

        public SQLServerPublicMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
            else if (context.Menu.Name == StandardMenus.User)
            {
                await ConfigureUserMenuAsync(context);
            }
        }

        private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<SQLServerResource>();

            // Home
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    SQLServerPublicMenus.HomePage,
                    l["Menu:HomePage"],
                    "~/",
                    icon: "fa fa-home",
                    order: 1
                )
            );

            // ArticleSample
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    SQLServerPublicMenus.ArticleSample,
                    l["Menu:ArticleSample"],
                    "~/article-sample",
                    icon: "fa fa-file-signature",
                    order: 2
                    )
            );

            // Contact Us
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    SQLServerPublicMenus.ContactUs,
                    l["Menu:ContactUs"],
                    "~/contact-us",
                    icon: "fa fa-phone",
                    order: 3
                    )
            );

            return Task.CompletedTask;
        }

        private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
        {
            var identityServerUrl = _configuration["AuthServer:Authority"] ?? "~";
            var uiResource = context.GetLocalizer<AbpUiResource>();
            var accountResource = context.GetLocalizer<AccountResource>();
            context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountResource["MyAccount"], $"{identityServerUrl.EnsureEndsWith('/')}Account/Manage", icon: "fa fa-cog", order: 1000, null, "_blank").RequireAuthenticated());
            context.Menu.AddItem(new ApplicationMenuItem("Account.SecurityLogs", accountResource["MySecurityLogs"], $"{identityServerUrl.EnsureEndsWith('/')}Account/SecurityLogs", target: "_blank").RequireAuthenticated());
            context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", uiResource["Logout"], url: "~/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000).RequireAuthenticated());

            return Task.CompletedTask;
        }
    }
}
