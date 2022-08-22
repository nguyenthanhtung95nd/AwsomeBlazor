using BlazorApp.Models.Enums;

namespace BlazorApp.Models
{
    public class TodoDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid? AssigneeId { get; set; }

        public string AssigneeName { set; get; }

        public DateTime CreatedDate { get; set; }

        public Priority Priority { get; set; }

        public Status Status { get; set; }
    }
}