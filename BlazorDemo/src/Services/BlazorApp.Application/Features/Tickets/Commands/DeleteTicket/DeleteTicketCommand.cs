using MediatR;

namespace BlazorApp.Application.Features.Tickets.Commands.DeleteTicket
{
    public record DeleteTicketCommand(int Id) : IRequest;
}