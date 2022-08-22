using BlazorApp.Models.Enums;
using BlazorApp.Models.SeedWork;

namespace BlazorApp.Models
{
    public class TodoListSearch : PagingParameters
    {
        public string? Name { get; set; }

        public Guid? AssigneeId { get; set; }

        public Priority? Priority { get; set; }
    }
}