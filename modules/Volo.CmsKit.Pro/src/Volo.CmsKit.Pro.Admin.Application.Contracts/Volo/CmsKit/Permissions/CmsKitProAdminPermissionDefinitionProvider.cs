using Volo.Abp.Authorization.Permissions;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Localization;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Localization;

namespace Volo.CmsKit.Permissions
{
    public class CmsKitProAdminPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var cmsGroup = context.GetGroupOrNull(CmsKitProAdminPermissions.GroupName)
                           ?? context.AddGroup(CmsKitProAdminPermissions.GroupName, L("Permission:CmsKit"));

            cmsGroup.AddPermission(CmsKitProAdminPermissions.Newsletters.Default, L("Permission:NewsletterManagement"))
                .RequireGlobalFeatures(typeof(NewslettersFeature));

            cmsGroup.AddPermission(CmsKitProAdminPermissions.Contact.SettingManagement, L("Permission:Contact:SettingManagement"))
                .RequireGlobalFeatures(typeof(ContactFeature));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<CmsKitResource>(name);
        }
    }
}
