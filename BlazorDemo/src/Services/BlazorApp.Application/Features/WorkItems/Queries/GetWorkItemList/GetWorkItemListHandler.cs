using AutoMapper;
using BlazorApp.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Application.Features.WorkItems.Queries.GetWorkItemList
{
    public class GetWorkItemListHandler : IRequestHandler<GetWorkItemListQuery, List<WorkItemViewModel>>
    {
        private readonly IWorkItemRepository _workItemsRepository;
        private readonly IMapper _mapper;

        public GetWorkItemListHandler(IWorkItemRepository workItemsRepository, IMapper mapper)
        {
            _workItemsRepository = workItemsRepository ?? throw new ArgumentNullException(nameof(workItemsRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<WorkItemViewModel>> Handle(GetWorkItemListQuery request, CancellationToken cancellationToken)
        {
            var workItemList = await _workItemsRepository.GetAllAsync();
            return _mapper.Map<List<WorkItemViewModel>>(workItemList);
        }
    }
}
