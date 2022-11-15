using Volo.Abp.Settings;

namespace CORE.MVC.SQLServer.Settings
{
    public class SQLServerSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(SQLServerSettings.MySetting1));
        }
    }
}
