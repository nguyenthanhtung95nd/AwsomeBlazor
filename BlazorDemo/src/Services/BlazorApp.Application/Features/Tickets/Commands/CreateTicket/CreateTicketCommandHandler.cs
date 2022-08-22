using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Domain.Entities;
using BlazorApp.Shared.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandHandler : IRequestHandler<CreateTicketCommand, int>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly ILogger<CreateTicketCommandHandler> _logger;

        public CreateTicketCommandHandler(ITicketRepository ticketRepository,
            ILogger<CreateTicketCommandHandler> logger)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CreateTicketCommand request, CancellationToken cancellationToken)
        {
            var ticketEntity = new Ticket
            {
                Title = request.Model.Title,
                Description = request.Model.Description,
                Status = Status.TODO,
            };

            if (request.Model.WorkItems != null)
            {
                ticketEntity.WorkItems = request.Model.WorkItems.Select(w => new WorkItem
                {
                    ItemType = w.ItemType,
                    Rate = w.Rate,
                    Quantity = w.Quantity,
                    UnitPrice = w.UnitPrice,
                    Hours = w.Hours
                }).ToList();
            }

            var newTicket = await _ticketRepository.AddAsync(ticketEntity);
            _logger.LogInformation($"New Ticket {newTicket.Id} is successfully created.");

            return newTicket.Id;
        }
    }
}