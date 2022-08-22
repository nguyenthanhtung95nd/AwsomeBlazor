using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Application.Exceptions;
using BlazorApp.Domain.Entities;
using MediatR;

namespace BlazorApp.Application.Features.Tickets.Commands.UpdateTicketStatus
{
    public class UpdateTicketStatusCommandHandler : IRequestHandler<UpdateTicketStatusCommand, bool>
    {
        private readonly ITicketRepository _ticketRepository;

        public UpdateTicketStatusCommandHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        public async Task<bool> Handle(UpdateTicketStatusCommand request, CancellationToken cancellationToken)
        {
            var ticketToUpdate = await _ticketRepository.GetByIdAsync(request.Id);
            if (ticketToUpdate == null)
            {
                throw new NotFoundException(nameof(Ticket), request.Id);
            }

            ticketToUpdate.Status = request.Status;
            await _ticketRepository.UpdateAsync(ticketToUpdate);
            return true;
        }
    }
}