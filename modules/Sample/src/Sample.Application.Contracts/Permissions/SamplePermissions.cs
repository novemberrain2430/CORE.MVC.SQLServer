using Volo.Abp.Reflection;

namespace Sample.Permissions
{
    public class SamplePermissions
    {
        public const string GroupName = "Sample";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(SamplePermissions));
        }

        public class Books
        {
            public const string Default = GroupName + ".Books";
            public const string Edit = Default + ".Edit";
            public const string Create = Default + ".Create";
            public const string Delete = Default + ".Delete";
        }
    }
}