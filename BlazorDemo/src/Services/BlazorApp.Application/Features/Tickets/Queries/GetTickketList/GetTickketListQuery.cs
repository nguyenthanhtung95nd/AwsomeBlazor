using BlazorApp.Shared.SeedWork;
using BlazorApp.Shared.Ticket;
using MediatR;

namespace BlazorApp.Application.Features.Tickets.Queries.GetTickketList
{
    public record GetTickketListQuery(SearchTicketViewModel Model) : IRequest<PaginatedList<TicketViewModel>>;
}