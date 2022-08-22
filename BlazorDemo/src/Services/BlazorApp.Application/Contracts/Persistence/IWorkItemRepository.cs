using BlazorApp.Domain.Entities;

namespace BlazorApp.Application.Contracts.Persistence
{
    public interface IWorkItemRepository : IAsyncRepository<WorkItem>
    {
    }
}