namespace Sample
{
    public static class SampleDbProperties
    {
        public static string DbTablePrefix { get; set; } = "Sample";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "Sample";
    }
}
