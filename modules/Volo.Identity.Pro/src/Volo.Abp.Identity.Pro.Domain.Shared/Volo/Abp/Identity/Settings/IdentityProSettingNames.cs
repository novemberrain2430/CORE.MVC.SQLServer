namespace Volo.Abp.Identity.Settings
{
    public static class IdentityProSettingNames
    {
        private const string Prefix = "Abp.Identity";

        public static class TwoFactor
        {
            private const string TwoFactorPrefix = Prefix + ".TwoFactor";

            public const string Behaviour = TwoFactorPrefix + ".Behaviour";

            public const string UsersCanChange = TwoFactorPrefix + ".UsersCanChange";
        }
    }
}
