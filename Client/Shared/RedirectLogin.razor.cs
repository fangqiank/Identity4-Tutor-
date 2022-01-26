using Microsoft.AspNetCore.Components;

namespace Client.Shared
{
    public partial class RedirectLogin
    {
        [Inject]
        private NavigationManager _navigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _navigationManager.NavigateTo($"/login?redirectUri={Uri.EscapeDataString(_navigationManager.Uri)}", true);
        }
    }
}
