using BlazorApp.Shared.Enums;
using BlazorApp.Shared.SeedWork;
using FluentValidation;

namespace BlazorApp.Shared.Ticket
{
    public class SearchTicketViewModel : PagingParameters
    {
        public string Title { get; set; }
        public Status? Status { get; set; }
    }
    public class SearchTicketViewModelValidator : AbstractValidator<SearchTicketViewModel>
    {
        public SearchTicketViewModelValidator()
        {
            RuleFor(x => x.PageNumber)
                .GreaterThanOrEqualTo(1).WithMessage("PageNumber at least greater than or equal to 1.");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("PageSize at least greater than or equal to 1.");
        }
    }
}