using BlazorApp.Shared.Ticket;
using MediatR;

namespace BlazorApp.Application.Features.Tickets.Commands.UpdateTicket
{
    public record UpdateStatusTicketCommand(int Id, UpdateTicketViewModel Model) : IRequest;
}