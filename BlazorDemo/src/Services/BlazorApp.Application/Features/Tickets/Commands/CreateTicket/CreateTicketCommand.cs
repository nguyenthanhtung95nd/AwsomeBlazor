using BlazorApp.Shared.Ticket;
using MediatR;

namespace BlazorApp.Application.Features.Tickets.Commands.CreateTicket
{
    public record CreateTicketCommand(CreateTicketViewModel Model) : IRequest<int>;
}