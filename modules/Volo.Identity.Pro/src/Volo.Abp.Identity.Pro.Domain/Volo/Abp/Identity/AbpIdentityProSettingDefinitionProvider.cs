using Volo.Abp.Features;
using Volo.Abp.Identity.Features;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Identity.Settings;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace Volo.Abp.Identity
{
    public class AbpIdentityProSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
               new SettingDefinition(IdentityProSettingNames.TwoFactor.Behaviour,
                    IdentityProTwoFactorBehaviour.Optional.ToString(),
                    L("DisplayName:Abp.Identity.TwoFactorBehaviour"),
                    L("Description:Abp.Identity.TwoFactorBehaviour"),
                    isVisibleToClients: true),

                new SettingDefinition(IdentityProSettingNames.TwoFactor.UsersCanChange,
                    true.ToString(),
                    L("DisplayName:Abp.Identity.UsersCanChange"),
                    L("Description:Abp.Identity.UsersCanChange"),
                    isVisibleToClients: true)
            );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<IdentityResource>(name);
        }
    }
}
