using BlazorApp.Shared.Enums;
using FluentValidation;

namespace BlazorApp.Shared.Ticket
{
    public class CreateTicketViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public List<WorkItemViewModel> WorkItems { get; set; } = new List<WorkItemViewModel>();
    }

    public class CreateTicketViewModelValidator : AbstractValidator<CreateTicketViewModel>
    {
        public CreateTicketViewModelValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please enter a title");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Please enter a description");
        }
    }

}