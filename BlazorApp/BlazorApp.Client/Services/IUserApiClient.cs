using BlazorApp.Models;

namespace BlazorApp.Client.Services
{
    public interface IUserApiClient
    {
        Task<List<AssigneeDto>> GetAssignees();
    }
}
