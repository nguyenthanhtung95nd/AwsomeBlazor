using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Application.Exceptions;
using BlazorApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Application.Features.WorkItems.Commands.DeleteWorkItem
{
    public class DeleteWorkItemCommandHandler : IRequestHandler<DeleteWorkItemCommand>
    {
        private readonly IWorkItemRepository _workItemRepository;
        private readonly ILogger<DeleteWorkItemCommandHandler> _logger;

        public DeleteWorkItemCommandHandler(IWorkItemRepository workItemRepository, ILogger<DeleteWorkItemCommandHandler> logger)
        {
            _workItemRepository = workItemRepository ?? throw new ArgumentNullException(nameof(workItemRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(DeleteWorkItemCommand request, CancellationToken cancellationToken)
        {
            var workItemToDelete = await _workItemRepository.GetByIdAsync(request.Id);
            if (workItemToDelete == null)
            {
                throw new NotFoundException(nameof(WorkItem), request.Id);
            }

            await _workItemRepository.DeleteAsync(workItemToDelete);
            _logger.LogInformation($"Work item {workItemToDelete.Id} is successfully deleted.");
            return Unit.Value;
        }
    }
}