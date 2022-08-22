using FluentValidation;

namespace BlazorApp.Application.Features.WorkItems.Commands.CreateWorkItem
{
    public class CreateWorkItemCommandValidator : AbstractValidator<CreateWorkItemCommand>
    {
        public CreateWorkItemCommandValidator()
        {
            //RuleFor(p => p.ItemType)
            //    .NotEmpty().WithMessage("{ItemType} is required.")
            //    .NotNull();

            //RuleFor(p => p.UnitPrice)
            //   .NotEmpty().WithMessage("{UnitPrice} is required.")
            //   .NotNull();

            RuleFor(p => p.Rate)
                .NotEmpty().WithMessage("{Rate} is required.")
                .GreaterThan(0).WithMessage("{Rate} should be greater than zero.");

            RuleFor(p => p.Hours)
                .NotEmpty().WithMessage("{Hours} is required.")
                .GreaterThan(0).WithMessage("{Hours} should be greater than zero.");

            RuleFor(p => p.Quantity)
                .NotEmpty().WithMessage("{Quantity} is required.")
                .GreaterThan(0).WithMessage("{Quantity} should be greater than zero.");
        }
    }
}