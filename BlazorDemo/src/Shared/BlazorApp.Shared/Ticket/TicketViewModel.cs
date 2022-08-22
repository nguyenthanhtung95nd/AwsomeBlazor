using BlazorApp.Shared.Enums;

namespace BlazorApp.Shared.Ticket
{
    public class TicketViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedDate { get; set; }
        public List<WorkItemViewModel> WorkItems { get; set; } = new List<WorkItemViewModel>();
    }
}