using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Application.Exceptions;
using BlazorApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Application.Features.Tickets.Commands.DeleteTicket
{
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<DeleteTicketCommandHandler> _logger;

        public DeleteTicketCommandHandler(ITicketRepository ticketRepository, ILogger<DeleteTicketCommandHandler> logger)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            var ticketToDelete = await _ticketRepository.GetByIdAsync(request.Id);
            if (ticketToDelete == null)
            {
                throw new NotFoundException(nameof(Ticket), request.Id);
            }

            await _ticketRepository.DeleteAsync(ticketToDelete);
            _logger.LogInformation($"Ticket {ticketToDelete.Id} is successfully deleted.");
            return Unit.Value;
        }
    }
}