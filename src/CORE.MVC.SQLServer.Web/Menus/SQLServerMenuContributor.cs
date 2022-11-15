using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using CORE.MVC.SQLServer.Localization;
using CORE.MVC.SQLServer.Permissions;
using Volo.Abp.Account.Localization;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.IdentityServer.Web.Navigation;
using Volo.Abp.LanguageManagement.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TextTemplateManagement.Web.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Users;
//using Volo.CmsKit.Pro.Admin.Web.Menus;
using Volo.Saas.Host.Navigation;

namespace CORE.MVC.SQLServer.Web.Menus
{
    public class SQLServerMenuContributor : IMenuContributor
    {
        private readonly IConfiguration _configuration;

        public SQLServerMenuContributor(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
            //else if (context.Menu.Name == StandardMenus.User)
            //{
            //    await ConfigureUserMenuAsync(context);
            //}
        }

        private static Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            var l = context.GetLocalizer<SQLServerResource>();
            //Home
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    SQLServerMenus.Home,
                    l["Menu:Home"],
                    "~/",
                    icon: "fa fa-home",
                    order: 1
                )
            );

            //Host Dashboard
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    SQLServerMenus.HostDashboard,
                    l["Menu:Dashboard"],
                    "~/HostDashboard",
                    icon: "fa fa-line-chart",
                    order: 2
                ).RequirePermissions(SQLServerPermissions.Dashboard.Host)
            );

            //Tenant Dashboard
            context.Menu.AddItem(
                new ApplicationMenuItem(
                    SQLServerMenus.TenantDashboard,
                    l["Menu:Dashboard"],
                    "~/Dashboard",
                    icon: "fa fa-line-chart",
                    order: 2
                ).RequirePermissions(SQLServerPermissions.Dashboard.Tenant)
            );
            context.Menu.AddItem(
    new ApplicationMenuItem(
        "BooksStore",
        l["Menu:BookStore"],
        icon: "fa fa-book"
    ).AddItem(
        new ApplicationMenuItem(
            "BooksStore.Books",
            l["Menu:Books"],
            url: "/Books"
        )
    )
);
            context.Menu.AddItem(
               new ApplicationMenuItem(
                       "BooksStore.Authors",
                    l["Menu:Authors"],
                    url: "/Authors"
               ).RequirePermissions(SQLServerPermissions.Authors.Default)
           );
            //Saas
            context.Menu.SetSubItemOrder(SaasHostMenuNames.GroupName, 3);

            //CMS
           // context.Menu.SetSubItemOrder(CmsKitProAdminMenus.GroupName, 4);

            //Administration
            var administration = context.Menu.GetAdministration();
            administration.Order = 5;

            //Administration->Identity
            administration.SetSubItemOrder(IdentityMenuNames.GroupName, 1);

            //Administration->Identity Server
            administration.SetSubItemOrder(AbpIdentityServerMenuNames.GroupName, 2);

            //Administration->Language Management
            //administration.SetSubItemOrder(LanguageManagementMenuNames.GroupName, 3);

            //Administration->Text Template Management
            administration.SetSubItemOrder(TextTemplateManagementMainMenuNames.GroupName, 4);

            //Administration->Audit Logs
            administration.SetSubItemOrder(AbpAuditLoggingMainMenuNames.GroupName, 5);

            //Administration->Settings
            administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 6);

            //context.Menu.AddItem(
            //    new ApplicationMenuItem(
            //        SQLServerMenus.Samples,
            //        l["Menu:Samples"],
            //        url: "/Samples",
            //        icon: "fa fa-file-alt",
            //        requiredPermissionName: SQLServerPermissions.Samples.Default)
            //);

            //context.Menu.AddItem(
            //    new ApplicationMenuItem(
            //        SQLServerMenus.Xamples,
            //        l["Menu:Xamples"],
            //        url: "/Xamples",
            //        icon: "fa fa-file-alt",
            //        requiredPermissionName: SQLServerPermissions.Xamples.Default)
            //);
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