using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Application.Exceptions;
using BlazorApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Application.Features.Tickets.Commands.UpdateTicket
{
    public class UpdateTicketCommandHandler : IRequestHandler<UpdateStatusTicketCommand>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IWorkItemRepository _workItemRepository;
        private readonly ILogger<UpdateTicketCommandHandler> _logger;

        public UpdateTicketCommandHandler(ITicketRepository ticketRepository, ILogger<UpdateTicketCommandHandler> logger, IWorkItemRepository workItemRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _workItemRepository = workItemRepository ?? throw new ArgumentNullException(nameof(workItemRepository));
        }

        public async Task<Unit> Handle(UpdateStatusTicketCommand request, CancellationToken cancellationToken)
        {
            var ticketToUpdate = await _ticketRepository.GetByIdAsync(request.Id);
            if (ticketToUpdate == null)
            {
                throw new NotFoundException(nameof(Ticket), request.Model.Id);
            }

            ticketToUpdate.Description = request.Model.Description;
            ticketToUpdate.Title = request.Model.Title;
            ticketToUpdate.Status = request.Model.Status;

            var workItemByTicketId = await _workItemRepository.FetchAsync(x => x.TicketId == request.Model.Id);
            if (workItemByTicketId != null)
            {
                foreach (var workItem in workItemByTicketId)
                {
                    await _workItemRepository.DeleteAsync(workItem);
                }
            }

            if (request.Model.WorkItems != null)
            {
                ticketToUpdate.WorkItems = request.Model.WorkItems.Select(w => new WorkItem
                {
                    ItemType = w.ItemType,
                    Rate = w.Rate,
                    Quantity = w.Quantity,
                    UnitPrice = w.UnitPrice,
                    Hours = w.Hours
                }).ToList();
            }

            await _ticketRepository.UpdateAsync(ticketToUpdate);

            _logger.LogInformation($"Ticket item {ticketToUpdate.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}