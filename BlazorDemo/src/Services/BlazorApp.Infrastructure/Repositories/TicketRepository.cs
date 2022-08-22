using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Domain.Entities;
using BlazorApp.Infrastructure.Persistence;

namespace BlazorApp.Infrastructure.Repositories
{
    public class TicketRepository : RepositoryBase<Ticket>, ITicketRepository
    {
        public TicketRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsExistTicketTitle(int id, string ticketTitle)
        {
            var ticket = await base.FetchAsync(x => x.Id != id);
            return ticket.All(x => x.Title != ticketTitle);
        }
    }
}