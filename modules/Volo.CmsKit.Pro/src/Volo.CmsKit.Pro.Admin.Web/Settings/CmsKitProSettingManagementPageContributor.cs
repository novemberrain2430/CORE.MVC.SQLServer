using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Volo.Abp.SettingManagement.Web.Pages.SettingManagement;
using Volo.CmsKit.Localization;
using Volo.CmsKit.Permissions;
using Volo.CmsKit.Pro.Admin.Web.Pages.CmsKit.Components.CmsKitProSettingGroup;

namespace Volo.CmsKit.Pro.Admin.Web.Settings
{
    public class CmsKitProSettingManagementPageContributor : ISettingPageContributor
    {
        public virtual async Task ConfigureAsync(SettingPageCreationContext context)
        {
            if (!await CheckPermissionsAsync(context))
            {
                return;
            }

            var l = context.ServiceProvider.GetRequiredService<IStringLocalizer<CmsKitResource>>();
            context.Groups.Add(
                new SettingPageGroup(
                    "Volo.Abp.CmsKitPro",
                    l["Settings:Menu:CmsKit"],
                    typeof(CmsKitProSettingGroupViewComponent)
                )
            );
        }

        public virtual async Task<bool> CheckPermissionsAsync(SettingPageCreationContext context)
        {
            var authorizationService = context.ServiceProvider.GetRequiredService<IAuthorizationService>();

            return await authorizationService.IsGrantedAsync(CmsKitProAdminPermissions.Contact.SettingManagement);
        }
    }
}