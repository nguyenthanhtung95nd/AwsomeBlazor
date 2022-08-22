using BlazorAppEditForm.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppEditForm.Server.Data
{
    public class ExpenseTrackerServerContext : DbContext
    {
        public ExpenseTrackerServerContext(DbContextOptions<ExpenseTrackerServerContext> options)
            : base(options)
        {
        }

        public DbSet<BlazorAppEditForm.Shared.ExpenseType> ExpenseType { get; set; }

        public DbSet<BlazorAppEditForm.Shared.Expense> Expense { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExpenseType>().HasData(
            new ExpenseType { Type = "Airfare", Id = 1 },
            new ExpenseType { Type = "Lodging", Id = 2 },
            new ExpenseType { Type = "Meal", Id = 3 },
            new ExpenseType { Type = "Other", Id = 4 }
            );
        }
    }
}
