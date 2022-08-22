using BlazorApp.Shared.Enums;
using BlazorApp.Shared.Ticket;
using BlazorApp.UI.Services.Interfaces;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.UI.Features.Home
{
    public partial class HomePage
    {
        [Inject]
        public ITicketSevice TicketSevice { get; set; } = default!;

        [Inject]
        NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private IToastService ToastService { set; get; } = default!;

        public TicketViewModel CurrentItem;
        List<TicketViewModel> TicketItems = new List<TicketViewModel>();

        public bool IsLoading { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            await GetTickets();
        }

        private async Task GetTickets()
        {
            try
            {
                var response = await TicketSevice.GetTickets(new SearchTicketViewModel());
                TicketItems = response.Items;
            }
            catch (Exception ex)
            {

            }
        }

        private void OnStartDrag(TicketViewModel item)
        {
            CurrentItem = item;
        }

        private async void OnDrop(Status status)
        {
            IsLoading = true;
            var result = await TicketSevice.UpdateTicketStatus(CurrentItem.Id, status);
            IsLoading = false;
            if (result)
            {
                CurrentItem.Status = status;
                ToastService.ShowSuccess($"Ticket {CurrentItem.Title} has been changed status: {status.ToString()} successfully.", "Success");
                StateHasChanged();
            }
            else
            {
                ToastService.ShowError($"Can not change Staus of {CurrentItem.Title} Ticket to {status.ToString()}", "Error");
            }
        }

        private void NavigateToHome()
        {
            NavigationManager.NavigateTo("/ticket");
        }
    }
}
