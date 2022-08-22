using BlazorApp.Domain.Entities;
using BlazorApp.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BlazorApp.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                //await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            if (!_context.WorkItems.Any())
            {
                _context.WorkItems.AddRange(GetPreconfiguredWorkItems());
                await _context.SaveChangesAsync();
                _logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }

            if (!_context.Tickets.Any())
            {
                _context.Tickets.AddRange(GetPreconfiguredTickets());
                await _context.SaveChangesAsync();
                _logger.LogInformation("Seed database associated with context {DbContextName}", typeof(ApplicationDbContext).Name);
            }
        }

        private static IEnumerable<WorkItem> GetPreconfiguredWorkItems()
        {
            return new List<WorkItem>
            {
                new WorkItem
                {
                    ItemType = ItemType.LABOR,
                    Rate = 0.5,
                    UnitPrice = UnitPrice.Dollar,
                    Hours = 1,
                    Quantity = 5
                }
            };
        }

        private static IEnumerable<Ticket> GetPreconfiguredTickets()
        {
            return new List<Ticket>
            {
                new Ticket
                {
                   Title = "New Ticket 1",
                   Description = "Ticket for testing",
                   Status = Status.TODO,
                }
            };
        }
    }
}