using Volo.Abp.Reflection;

namespace Volo.CmsKit.Permissions
{
    public class CmsKitProAdminPermissions
    {
        public const string GroupName = CmsKitAdminPermissions.GroupName;

        public static class Newsletters
        {
            public const string Default = GroupName + ".Newsletter";
        }
        
        public static class Contact
        {
            private const string Default = GroupName + ".Contact";
            
            public const string SettingManagement = GroupName + ".SettingManagement";
        }

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(CmsKitProAdminPermissions));
        }
    }
}