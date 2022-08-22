using Microsoft.EntityFrameworkCore;

namespace BlazorAppTaskManager.Server.Data
{
    public class TaskManagerServerContext : DbContext
    {
        public TaskManagerServerContext(DbContextOptions<TaskManagerServerContext> options)
            : base(options)
        {
        }

        public DbSet<BlazorAppTaskManager.Shared.TaskItem> TaskItem { get; set; }
    }
}
