using BlazorApp.Shared.Enums;
using MediatR;

namespace BlazorApp.Application.Features.Tickets.Commands.UpdateTicketStatus
{
    public record UpdateTicketStatusCommand(int Id, Status Status) : IRequest<bool>;
}