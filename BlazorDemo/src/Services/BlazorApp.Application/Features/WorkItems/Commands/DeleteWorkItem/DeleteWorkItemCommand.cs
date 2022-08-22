using MediatR;

namespace BlazorApp.Application.Features.WorkItems.Commands.DeleteWorkItem
{
    public class DeleteWorkItemCommand : IRequest
    {
        public int Id { get; set; }
    }
}