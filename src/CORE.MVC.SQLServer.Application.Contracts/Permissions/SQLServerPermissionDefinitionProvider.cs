using CORE.MVC.SQLServer.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace CORE.MVC.SQLServer.Permissions
{
    public class SQLServerPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(SQLServerPermissions.GroupName);

            myGroup.AddPermission(SQLServerPermissions.Dashboard.Host, L("Permission:Dashboard"), MultiTenancySides.Host);
            myGroup.AddPermission(SQLServerPermissions.Dashboard.Tenant, L("Permission:Dashboard"), MultiTenancySides.Tenant);
            myGroup.AddPermission(SQLServerPermissions.Dashboard.Index, L("Permission:Index"), MultiTenancySides.Both);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(SQLServerPermissions.MyPermission1, L("Permission:MyPermission1"));

            var samplePermission = myGroup.AddPermission(SQLServerPermissions.Samples.Default, L("Permission:Samples"));
            samplePermission.AddChild(SQLServerPermissions.Samples.Create, L("Permission:Create"));
            samplePermission.AddChild(SQLServerPermissions.Samples.Edit, L("Permission:Edit"));
            samplePermission.AddChild(SQLServerPermissions.Samples.Delete, L("Permission:Delete"));

            var xamplePermission = myGroup.AddPermission(SQLServerPermissions.Xamples.Default, L("Permission:Xamples"));
            xamplePermission.AddChild(SQLServerPermissions.Xamples.Create, L("Permission:Create"));
            xamplePermission.AddChild(SQLServerPermissions.Xamples.Edit, L("Permission:Edit"));
            xamplePermission.AddChild(SQLServerPermissions.Xamples.Delete, L("Permission:Delete"));
            var authorPermission = myGroup.AddPermission(SQLServerPermissions.Authors.Default, L("Permission:Authors"));
            authorPermission.AddChild(SQLServerPermissions.Authors.Create, L("Permission:Create"));
            authorPermission.AddChild(SQLServerPermissions.Authors.Edit, L("Permission:Edit"));
            authorPermission.AddChild(SQLServerPermissions.Authors.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SQLServerResource>(name);
        }
    }
}