using BlazorApp.Shared.Enums;
using MediatR;

namespace BlazorApp.Application.Features.WorkItems.Commands.CreateWorkItem
{
    public class CreateWorkItemCommand : IRequest<int>
    {
        public int TicketId { get; set; }

        public ItemType ItemType { get; set; }

        public double Rate { get; set; }

        public UnitPrice UnitPrice { get; set; }

        public double Hours { get; set; }

        public int Quantity { get; set; }
    }
}