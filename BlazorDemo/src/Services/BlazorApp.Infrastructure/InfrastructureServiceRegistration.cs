using BlazorApp.Application.Contracts.Infrastructure;
using BlazorApp.Application.Contracts.Persistence;
using BlazorApp.Application.Models;
using BlazorApp.Infrastructure.Mail;
using BlazorApp.Infrastructure.Persistence;
using BlazorApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorApp.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DBConnectionString")));

            services.AddScoped<ApplicationDbContextInitialiser>();

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IWorkItemRepository, WorkItemRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();

            services.Configure<EmailSettings>(c => configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailService, EmailService>();

            return services;
        }
    }
}