using BlazorApp.Shared.Enums;
using BlazorApp.Shared.SeedWork;
using BlazorApp.Shared.Ticket;

namespace BlazorApp.UI.Services.Interfaces
{
    public interface ITicketSevice
    {
        Task<PaginatedList<TicketViewModel>> GetTickets(SearchTicketViewModel viewModel);

        Task<TicketViewModel> GetTicketById(int id);

        Task<bool> DeleteTicket(int id);

        Task<bool> UpdateTicketStatus(int id, Status status);

        Task<int> CreateTicket(CreateTicketViewModel model);

        Task<bool> UpdateTicket(int id, UpdateTicketViewModel model);
    }
}