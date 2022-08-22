using BlazorApp.Domain.Common;
using BlazorApp.Shared.Enums;

namespace BlazorApp.Domain.Entities
{
    public class Ticket : EntityBase
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public ICollection<WorkItem> WorkItems { get; set; }
    }
}