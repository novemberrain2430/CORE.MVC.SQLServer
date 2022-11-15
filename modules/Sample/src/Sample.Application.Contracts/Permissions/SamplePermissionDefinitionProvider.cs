using Sample.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Sample.Permissions
{
    public class SamplePermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(SamplePermissions.GroupName, L("Permission:Sample"));

            var bookPermission = myGroup.AddPermission(SamplePermissions.Books.Default, L("Permission:Books"));
            bookPermission.AddChild(SamplePermissions.Books.Create, L("Permission:Create"));
            bookPermission.AddChild(SamplePermissions.Books.Edit, L("Permission:Edit"));
            bookPermission.AddChild(SamplePermissions.Books.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SampleResource>(name);
        }
    }
}