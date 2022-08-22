using BlazorApp.Domain.Entities;

namespace BlazorApp.Application.Contracts.Persistence
{
    public interface ITicketRepository : IAsyncRepository<Ticket>
    {
        Task<bool> IsExistTicketTitle(int id, string ticketTitle);
    }
}