using BlazorApp.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Models
{
    public class TodoCreateRequest
    {
        public int Id { get; set; }

        [MaxLength(20, ErrorMessage = "You cannot fill task name over than 20 characters")]
        [Required(ErrorMessage = "Please enter your task name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please select your task priority")]
        public Priority? Priority { get; set; }
    }
}