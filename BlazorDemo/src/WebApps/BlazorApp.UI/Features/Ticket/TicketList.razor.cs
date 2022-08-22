using BlazorApp.ComponentLib;
using BlazorApp.Shared.SeedWork;
using BlazorApp.Shared.Ticket;
using BlazorApp.UI.Features.Shared;
using BlazorApp.UI.Services.Interfaces;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.UI.Features.Ticket
{
    public partial class TicketList
    {
        #region DI
        [Inject]
        public ITicketSevice TicketSevice { get; set; } = default!;

        #endregion

        #region Properties
        public List<TicketViewModel> Tickets { get; set; } = new List<TicketViewModel>();
        public SearchTicketViewModel SearchTicketViewModel { get; set; } = new SearchTicketViewModel();
        public MetaData MetaData { get; set; } = new MetaData();
        private int DeleteId { set; get; }
        protected Confirmation DeleteConfirmation { set; get; } = default!;

        [CascadingParameter]
        private Error Error { set; get; }
        #endregion

        #region Component Life Cycle
        protected override async Task OnInitializedAsync()
        {
            await GetTickets();
        }
        #endregion

        #region Methods
        private async Task GetTickets()
        {
            try
            {
                var response = await TicketSevice.GetTickets(SearchTicketViewModel);
                Tickets = response.Items;
                MetaData = response.MetaData;
            }
            catch (Exception ex)
            {
                Error.ProcessError(ex);
            }
        }

        private async Task SelectedPage(int page)
        {
            SearchTicketViewModel.PageNumber = page;
            await GetTickets();
        }

        public void OnDeleteTicket(int deleteId)
        {
            DeleteId = deleteId;
            DeleteConfirmation.Show();
        }

        public async Task OnConfirmDeleteTicket(bool deleteConfirmed)
        {
            if (deleteConfirmed)
            {
                await TicketSevice.DeleteTicket(DeleteId);
                await GetTickets();
            }
        }

        public async Task HandleSearchForm(SearchTicketViewModel searchTicketViewModel)
        {
            SearchTicketViewModel = searchTicketViewModel;
            await GetTickets();
        }

        #endregion
    }
}
