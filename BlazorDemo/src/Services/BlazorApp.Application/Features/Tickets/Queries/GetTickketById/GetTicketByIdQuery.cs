using BlazorApp.Application.Models;
using BlazorApp.Shared.Ticket;
using MediatR;

namespace BlazorApp.Application.Features.Tickets.Queries.GetTickketById
{
    public record GetTicketByIdQuery(int Id) : IRequest<TicketViewModel>;
}