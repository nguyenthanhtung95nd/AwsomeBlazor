using AutoMapper;
using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Application.Exceptions;
using BlazorApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Application.Features.WorkItems.Commands.UpdateWorkItem
{
    public class UpdateWorkItemCommandHandler : IRequestHandler<UpdateWorkItemCommand>
    {
        private readonly IWorkItemRepository _workItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateWorkItemCommandHandler> _logger;

        public UpdateWorkItemCommandHandler(IWorkItemRepository workItemRepository, IMapper mapper, ILogger<UpdateWorkItemCommandHandler> logger)
        {
            _workItemRepository = workItemRepository ?? throw new ArgumentNullException(nameof(workItemRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Unit> Handle(UpdateWorkItemCommand request, CancellationToken cancellationToken)
        {
            var workItemToUpdate = await _workItemRepository.GetByIdAsync(request.Id);
            if (workItemToUpdate == null)
            {
                throw new NotFoundException(nameof(WorkItem), request.Id);
            }

            _mapper.Map(request, workItemToUpdate, typeof(UpdateWorkItemCommand), typeof(WorkItem));

            await _workItemRepository.UpdateAsync(workItemToUpdate);

            _logger.LogInformation($"Work item {workItemToUpdate.Id} is successfully updated.");

            return Unit.Value;
        }
    }
}