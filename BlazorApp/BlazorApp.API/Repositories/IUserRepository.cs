using BlazorApp.API.Entities;

namespace BlazorApp.API.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserList();
    }
}
