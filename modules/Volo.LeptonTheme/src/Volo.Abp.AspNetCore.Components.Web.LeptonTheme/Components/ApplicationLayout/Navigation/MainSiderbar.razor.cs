using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace Volo.Abp.AspNetCore.Components.Web.LeptonTheme.Components.ApplicationLayout.Navigation
{
    public partial class MainSiderbar : IDisposable
    {
        [Inject]
        protected MainMenuProvider MainMenuProvider { get; set; }

        protected MenuViewModel Menu { get; set; }

        [Parameter]
        public EventCallback OnClickCallback { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Menu = await MainMenuProvider.GetMenuAsync();
            Menu.StateChanged += Menu_StateChanged;
        }

        private void Menu_StateChanged(object sender, EventArgs e)
        {
            InvokeAsync(StateHasChanged);
        }

        public void Dispose()
        {
            Menu.StateChanged -= Menu_StateChanged;
        }
    }
}
