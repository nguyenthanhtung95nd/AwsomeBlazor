using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Domain.Entities;
using BlazorApp.Infrastructure.Persistence;

namespace BlazorApp.Infrastructure.Repositories
{
    public class WorkItemRepository : RepositoryBase<WorkItem>, IWorkItemRepository
    {
        public WorkItemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}