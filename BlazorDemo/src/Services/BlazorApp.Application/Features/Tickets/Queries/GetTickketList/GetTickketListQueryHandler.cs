using AutoMapper;
using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Application.Models;
using BlazorApp.Shared.SeedWork;
using BlazorApp.Shared.Ticket;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Application.Features.Tickets.Queries.GetTickketList
{
    public class GetTickketListQueryHandler : IRequestHandler<GetTickketListQuery, PaginatedList<TicketViewModel>>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTickketListQueryHandler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository ?? throw new ArgumentNullException(nameof(ticketRepository));
        }

        public async Task<PaginatedList<TicketViewModel>> Handle(GetTickketListQuery request, CancellationToken cancellationToken)
        {
            var query = await _ticketRepository.GetAllIncludingAsync(i => i.WorkItems);
            var data = query.Select(x => new TicketViewModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Status = x.Status,
                CreatedBy = x.CreatedBy,
                CreatedDate = x.CreatedDate,
                LastModifiedBy = x.LastModifiedBy,
                LastModifiedDate = x.LastModifiedDate,
                WorkItems = x.WorkItems.Select(w => new WorkItemViewModel
                {
                    Id = w.Id,
                    Hours = w.Hours,
                    ItemType =w.ItemType,
                    Quantity = w.Quantity,
                    Rate = w.Rate,
                    UnitPrice = w.UnitPrice
                }).ToList()
            });

            if (!string.IsNullOrEmpty(request.Model.Title)) {
                data = data.Where(x => x.Title.Contains(request.Model.Title));
            }

            if (request.Model.Status.HasValue)
            {
                data = data.Where(x => x.Status == request.Model.Status);
            }

            var count = data.Count();
            var skip = request.Model.PageSize * (request.Model.PageNumber - 1);
            var items = data.Skip(skip).Take(request.Model.PageSize).ToList();

            return new PaginatedList<TicketViewModel>(items, count, request.Model.PageNumber, request.Model.PageSize);
        }
    }
}