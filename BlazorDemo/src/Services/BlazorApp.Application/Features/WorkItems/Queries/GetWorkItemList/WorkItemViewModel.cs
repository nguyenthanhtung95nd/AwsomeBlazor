using BlazorApp.Shared.Enums;

namespace BlazorApp.Application.Features.WorkItems.Queries.GetWorkItemList
{
    public class WorkItemViewModel
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public ItemType ItemType { get; set; }

        public double Rate { get; set; }

        public UnitPrice UnitPrice { get; set; }

        public double Hours { get; set; }

        public int Quantity { get; set; }
    }
}