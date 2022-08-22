using AutoMapper;
using BlazorApp.Application.Contracts.Infrastructure;
using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Application.Models;
using BlazorApp.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Application.Features.WorkItems.Commands.CreateWorkItem
{
    public class CreateWorkItemCommandHandler : IRequestHandler<CreateWorkItemCommand, int>
    {
        private readonly IWorkItemRepository _workItemRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateWorkItemCommandHandler> _logger;

        public CreateWorkItemCommandHandler(IWorkItemRepository orderRepository, IMapper mapper, IEmailService emailService, ILogger<CreateWorkItemCommandHandler> logger)
        {
            _workItemRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<int> Handle(CreateWorkItemCommand request, CancellationToken cancellationToken)
        {
            var workItemEntity = _mapper.Map<WorkItem>(request);
            var newWorkItem = await _workItemRepository.AddAsync(workItemEntity);

            _logger.LogInformation($"New workItem {newWorkItem.Id} is successfully created.");
            await SendMail(newWorkItem);
            return newWorkItem.Id;
        }

        private async Task SendMail(WorkItem order)
        {
            var email = new Email
            {
                To = "tung112233aabbcc@gmail.com",
                Body = $"Work item was created.",
                Subject = "Work item was created"
            };

            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Work item {order.Id} failed due to an error with the mail service: {ex.Message}");
            }
        }
    }
}