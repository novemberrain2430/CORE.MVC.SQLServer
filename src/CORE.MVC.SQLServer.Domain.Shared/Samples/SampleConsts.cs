namespace CORE.MVC.SQLServer.Samples
{
    public static class SampleConsts
    {
        private const string DefaultSorting = "{0}Name asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Sample." : string.Empty);
        }

        public const int CodeMaxLength = 200;
    }
}