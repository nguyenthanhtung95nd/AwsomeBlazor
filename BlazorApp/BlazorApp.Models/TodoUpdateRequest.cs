using BlazorApp.Models.Enums;

namespace BlazorApp.Models
{
    public class TodoUpdateRequest
    {
        public string? Name { get; set; }

        public Priority Priority { get; set; }
    }
}