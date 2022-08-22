using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Shared.Ticket;
using MediatR;

namespace BlazorApp.Application.Features.Tickets.Queries.GetTickketById
{
    public class GetTicketByIdQueryHandler : IRequestHandler<GetTicketByIdQuery, TicketViewModel>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketByIdQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        public async Task<TicketViewModel> Handle(GetTicketByIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.GetAsync(x => x.Id == request.Id, i => i.WorkItems);
            return new TicketViewModel
            {
                Id = ticket.Id,
                Description = ticket.Description,
                Title = ticket.Title,
                Status = ticket.Status,
                WorkItems = ticket.WorkItems.Select(w => new WorkItemViewModel
                {
                    Id = w.Id,
                    Hours = w.Hours,
                    ItemType = w.ItemType,
                    Quantity = w.Quantity,
                    Rate = w.Rate,
                    UnitPrice = w.UnitPrice
                }).ToList()
            };
        }
    }
}