using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Application.Features.WorkItems.Queries.GetWorkItemList
{
    public class GetWorkItemListQuery : IRequest<List<WorkItemViewModel>>
    {
    }
}
