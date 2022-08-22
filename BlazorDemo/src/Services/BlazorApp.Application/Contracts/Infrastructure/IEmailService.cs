using BlazorApp.Application.Models;

namespace BlazorApp.Application.Contracts.Infrastructure
{
    public interface IEmailService
    {
        Task<bool> SendEmail(Email email);
    }
}