using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.Features;
using Volo.Abp.Authorization.Permissions;
using Volo.Chat.Authorization;
using Volo.Chat.Web.Pages.Chat.Components.MessagesToolbarItem;

namespace Volo.Chat.Web
{
    public class ChatToolbarContributor : IToolbarContributor
    {
        public async Task ConfigureToolbarAsync(IToolbarConfigurationContext context)
        {
            var featureChecker = context.ServiceProvider.GetService<IFeatureChecker>();

            await featureChecker.IsEnabledAsync(ChatFeatures.Enable);

            if (context.Toolbar.Name == StandardToolbars.Main)
            {
                if (await featureChecker.IsEnabledAsync(ChatFeatures.Enable))
                {
                    context.Toolbar.Items
                        .Insert(0, new ToolbarItem(typeof(MessagesToolbarItemViewComponent)).RequirePermissions(ChatPermissions.Messaging));
                }
            }
        }
    }
}
