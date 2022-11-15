using Volo.Abp.Settings;

namespace Volo.CmsKit
{
    public class CmsKitProSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(new SettingDefinition(CmsKitProSettingNames.Contact.ReceiverEmailAddress, "info@mycompanyname.com", null, null, true));
        }
    }
}