using BlazorApp.UI.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.UI.Features.Authenticate
{
    public partial class Logout
    {

        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}
