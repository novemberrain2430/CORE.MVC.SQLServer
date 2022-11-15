namespace CORE.MVC.SQLServer.Xamples
{
    public static class XampleConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Xample." : string.Empty);
        }

        public const int CodeMaxLength = 200;
    }
}