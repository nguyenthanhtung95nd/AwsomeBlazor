using BlazorApp.Shared.Enums;

namespace BlazorApp.Shared.Ticket
{
    public class UpdateTicketViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public IList<WorkItemViewModel> WorkItems { get; set; }
    }
}